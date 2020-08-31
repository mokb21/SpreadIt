using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpreadIt.API.Hubs
{
    public class SpreadItHub : Hub
    {
        public async Task SendMessage()
        {
            if (Clients != null)
                await Clients.All.SendAsync("ReceiveMessage", "New Post Added !!");
        }
    }
}