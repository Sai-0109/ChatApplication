using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
namespace ChatApplication.ChatHubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string GroupName, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", GroupName, message);
        }
        public async Task JoinGroup(string GroupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, GroupName);
        }

        public async Task SendMessageToGroup(string GroupName, string message)
        {
            try
            {
                 await Clients.Group(GroupName).SendAsync("ReceiveMessage", message);
            }
            catch (Exception)
            {

                throw;
            }
            
           // return Clients.All.SendAsync("ReceiveMessage", GroupName, message);
        }

    }
}
