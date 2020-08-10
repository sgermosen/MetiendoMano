// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const loader = $("#loader");
const setCount = (target, val) => target.text(val.length);
const id = (val) => document.getElementById(val);
const log = (text) => console.log(text);
const error = (val) => console.error('Error:', val);

let alertCount = 0,
		fetchTimeOut = 60000; // 60000 ms = 1 minute

function showAlert(message, type, timeout) {
	let alert = $('#alert-container'),
		alertId = `alert-${++alertCount}`,
		alertClass = `class="alert ${type}"`,
		alertMessage = `<span>${message}</span>`,
		alertBody = `<div id="${alertId}" ${alertClass} role="alert" data-dismiss="alert">${alertMessage}</div>`;

	log(message);
	alert.append(alertBody);

	setTimeout(() => $(`#${alertId}`).remove(), timeout);
}

function fetchTimeout(url, ms, { signal, ...options } = {}) {
	const controller = new AbortController();
	const promise = fetch(url, { signal: controller.signal, ...options });

	if (signal)
		signal.addEventListener("abort", () => controller.abort());

	const timeout = setTimeout(() => controller.abort(), ms);

	return promise.finally(() => clearTimeout(timeout));
}