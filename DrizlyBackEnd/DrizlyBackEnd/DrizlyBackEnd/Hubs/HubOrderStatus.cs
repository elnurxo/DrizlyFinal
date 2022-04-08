using DrizlyBackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Hubs
{
    public class HubOrderStatus : Hub
    {
        private readonly UserManager<AppUser> _userManager;
        public HubOrderStatus(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task OrderStatusChange(string userid,string result,int orderid)
        {
            AppUser user = await _userManager.FindByIdAsync(userid);
            await Clients.Client(user.ConnectionId).SendAsync("orderstatus", result,orderid);
        }
        public override Task OnConnectedAsync()
        {
            AppUser member =  _userManager.FindByNameAsync(Context.User.Identity.Name).Result;

            member.ConnectionId = Context.ConnectionId;
            var Result = _userManager.UpdateAsync(member).Result;


            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            AppUser member = _userManager.FindByNameAsync(Context.User.Identity.Name).Result;

            member.ConnectionId = null;
            var Result = _userManager.UpdateAsync(member).Result;
            return base.OnDisconnectedAsync(exception);
        }
    }
}
