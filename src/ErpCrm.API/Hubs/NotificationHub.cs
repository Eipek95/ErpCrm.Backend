using ErpCrm.API.Realtime;
using Microsoft.AspNetCore.SignalR;

namespace ErpCrm.API.Hubs;

public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var user =
            Context.UserIdentifier
            ?? Context.ConnectionId;

        OnlineUserTracker.AddUser(
            Context.ConnectionId,
            user);

        await Clients.All.SendAsync(
            "OnlineUserCountChanged",
            OnlineUserTracker.GetOnlineUserCount());

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(
        Exception? exception)
    {
        OnlineUserTracker.RemoveUser(
            Context.ConnectionId);

        await Clients.All.SendAsync(
            "OnlineUserCountChanged",
            OnlineUserTracker.GetOnlineUserCount());

        await base.OnDisconnectedAsync(exception);
    }
}