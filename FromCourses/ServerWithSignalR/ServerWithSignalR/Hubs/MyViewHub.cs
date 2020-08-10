using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWithSignalR.Hubs
{
    public class MyViewHub : Hub
    {
        public async Task MoveViewFromServer(float newX,float newY)
        {
            Console.WriteLine($"Receive position from server app: {newX}/{newY}");
            //clients other means than we will send to all other client but our id
            //if we want to send to all client, just use clients.All
            await Clients.Others.SendAsync($"ReceiveNewPosition", newX, newY);
        }
    }
}
