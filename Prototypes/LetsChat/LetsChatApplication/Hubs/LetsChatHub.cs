using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace LetsChatApplication.Hubs
{
    public class LetsChatHub : Hub
    {
        public void Send(string name, string message,string connId)
        {
            Clients.Client(connId).addNewMessageToPage(name, message);
        }

        public void SendToAll(string name, string message)//send to all
        {
            Clients.All.appendNewMessage(name, message);
        }
    }
}