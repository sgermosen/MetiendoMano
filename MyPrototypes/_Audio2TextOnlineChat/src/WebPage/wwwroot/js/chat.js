
const connection = new signalR.HubConnectionBuilder()
	.withUrl("/chatHub")
	.configureLogging(signalR.LogLevel.Error)
	// .withAutomaticReconnect([0, 2000, 10000, 30000]) // yields the default behavior
	.withAutomaticReconnect()
	.build(),
	userNameInput = $("#user-name"),
	connectButton = $("#connect"),
	chatButton = $("#fixed-right-chat-button"),
	messageInput = $("#message"),
	sendButton = $("#send"),
	connectForm = $("#user-name-form"),
	chatContainer = $("#chat-container"),
	chatForm = $("#chat-form"),
	chatPlaceholder = $("#chat-placeholder"),
	userPlaceholder = $("#users-placeholder"),
	userListContainer = $("#user-list"),
	userList = $("#connected-users"),
	messageList = $("#messages-list");

var tryConnectCount = 0,
	isChatVisible = false,
	userId,
	toUserId,
	userName,
	connectedUsers,
	errorMessages,
	isPtpChat;

$(document).ready(async () => {
	connection.on("UpdateConnectedUsers",
		data => {
			userList.empty();
			if (data) {
				connectedUsers = data;
				if (data.length > 0) {
					connectedUsers.forEach(element => {
						var user = createUser({
							userName: element.userName,
							userId: element.userId,
							messageCount: element.messageCount
						});
						userList.append(user);
					});
					userPlaceholder.hide();
				} else
					userPlaceholder.show();
			}
		});

	connection.on("NewUserAvailable",
		data => {
			if (data && !isPtpChat) {
				if (!connectedUsers.includes(data))
					connectedUsers.push(data);

				var user = createUser({
					userName: data.userName,
					userId: data.userId,
					messageCount: data.messageCount
				});

				userList.append(user);
				userPlaceholder.hide();
			}
		});

	connection.on("UserNotAvailable",
		data => {
			if (data && !isPtpChat) {
				const userItem = $(`#${data}`);
				if (userItem)
					userItem.detach();

				connectedUsers = connectedUsers.filter(x => x.userId !== data);

				if (connectedUsers.length === 0)
					userPlaceholder.show();
			}
		});

	connection.on("ConnectWith", data => toUserId = data);

	connection.on("UpdateUnreadMessages",
		data => {
			if (data && !isPtpChat) {
				var user = connectedUsers.filter(x => x.userId === data);
				if (toUserId === data || user.length <= 0)
					return;

				let messageCount = $(`#count-${data}`).text();
				messageCount = messageCount ? ++messageCount : "1";
				$(`#count-${data}`).text(messageCount);

				const audio = id("notification-audio");
				audio.play();
			}
		});

	connection.on("ReceiveConversation",
		data => {
			messageList.empty();
			if (data) {
				data.forEach(element => {
					if (element.fromUserId === userId)
						element.fromUserName = "Me";
					const message = createMessage(element);
					messageList.append(message);
				});
			}
		});

	connection.on("ReceiveMessage",
		data => {
			if (data) {
				if (data.fromUserId !== toUserId)
					return;

				const message = createMessage(data);
				messageList.append(message);
			}
		});

	connection.onclose(() => {
		tryConnectCount++;
		if (tryConnectCount < 10 && userId)
			start();
	});

	connection.onreconnecting(() => {
		showAlert(errorMessages['ConnectionLost'], "alert-danger", 5000);
	});

	connectButton.click(connect);

	sendButton.click(sendMessage);

	userNameInput.bind('input',
		function () {
			setCount($("#user-name-count"), $(this).val());
		});

	messageInput.bind('input', updateMessageBox);

	getErrorMessages();

	chatForm.hide();
	chatContainer.hide();
	userListContainer.hide();
	loader.hide();
	sendButton.prop('disabled', true);
	userNameInput.val("").focus();
});

function getErrorMessages() {
	fetch("chat/getErrorMessages", { method: "GET" })
		.then(response => response.json())
		.then(data => errorMessages = data)
		.catch(e => error(e));
}

function toggleChat() {
	isChatVisible = !isChatVisible;
	if (isChatVisible) {
		chatButton.hide();
		chatContainer.show();
	} else {
		chatButton.show();
		chatContainer.hide();
	}
}

function startChat(isNewRoom) {
	if (!isValidUserName())
		return;

	connect(isNewRoom);
}

function isValidUserName() {
	userName = userNameInput.val();
	let isValid = userName.length > 0;
	if (!isValid)
		showAlert(errorMessages['InvalidUserName'], "alert-danger", 5000);

	return isValid;
}

function connect(isNewRoom = false) {
	loader.show();

	connection.start()
		.then(() => {
			connection.invoke("Connect", userName, isNewRoom)
				.then(data => {
					userId = data.value;
					showRoom(isNewRoom);
				})
				.catch(e => {
					error(e);
					showAlert(errorMessages['UserIsConnected'], "alert-danger", 5000);
				});
		})
		.catch(e => showAlert(e.toString(), "alert-danger", 5000));
}

function showRoom(isNewRoom) {
	isPtpChat = isNewRoom;
	connectForm.hide();
	chatForm.show();

	if (isPtpChat) {
		messageList.append(createMessage({
			fromUserName: "",
			messageDate: "",
			message: "Wait for and admin to assist you."
		}));

		chatPlaceholder.hide();
	} else {
		$("#fixed-bottom-chat").width(800);
		userListContainer.show();

		getConnectedUsers();
	}

	loader.hide();
}

function getConversation(id) {
	if (toUserId === id)
		return;

	toUserId = id;
	connection.invoke("RequestConversation", userId, toUserId)
		.then(() => {
			chatPlaceholder.hide();
			$(`#count-${toUserId}`).text("");
		})
		.catch(e => {
			error(e);
			showAlert(errorMessages['UserIsDisconnected'], "alert-danger", 5000);
		});
}

function sendMessage() {
	const message = messageInput.val(),
		messageDetails = {
			fromUserId: userId,
			toUserId: toUserId,
			fromUserName: userName,
			messageDate: new Date(),
			message: message
		},
		method = toUserId ? "SendPrivateMessage" : "CacheMessage";

	connection.invoke(method, messageDetails)
		.then(() => {
			messageDetails.fromUserName = "Me";
			const messageBody = createMessage(messageDetails);
			messageList.append(messageBody);
		})
		.catch(e => {
			error(e);
			showAlert(errorMessages['UserIsDisconnected'], "alert-danger", 5000);
		});

	messageInput.val("").focus();
	updateMessageBox();
}

function updateMessageBox() {
	const text = messageInput.val(),
		isButtonDisabled = text.length <= 0;

	setCount($("#message-count"), text);
	sendButton.prop('disabled', isButtonDisabled);
}

function getConnectedUsers() {
	connection.invoke("RequestConnectedUsers")
		.catch(e => {
			error(e);
			showAlert(errorMessages['UnableToGetConnectedUsers'], "alert-danger", 5000);
		});
}

function createMessage(data) {
	const message = data.message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;"),
		userName = data.fromUserName,
		messageColor = (userName === "") ? "bg-secondary" : (userName === "Me") ? "bg-success" : "bg-primary",
		messageSender = `<p class="mb-0">${userName}</p>`,
		formattedDate = $.format.date(data.messageDate, "dd/MM/yy hh:mm:ss a"),
		messageDate = `<p class="mb-0">${formattedDate}</p>`,
		messageHeader = `<div class="d-flex justify-content-between">${messageSender}${messageDate}</div>`,
		messageText = `<p class="mb-0">${message}</p>`,
		messageBody = `<div class="${messageColor} text-white p-2 my-2">${messageHeader}${messageText}</div>`;

	return messageBody;
}

function createUser(data) {
	const userName = data.userName,
		userId = data.userId,
		messageCount = data.messageCount !== 0 ? data.messageCount : "",
		buttonClass = 'class="list-group-item list-group-item-action d-flex justify-content-between user-item"',
		messageIndicator = `<span class="badge badge-primary badge-pill" id="count-${userId}">${messageCount}</span>`,
		templateData = `type="button" name="${userName}" id="${userId}" ${buttonClass}`,
		buttonCommand = `onclick="getConversation(this.id)"`,
		userButton = `<button ${templateData} ${buttonCommand}>${userName} ${messageIndicator}</button>`;

	return userButton;
}
