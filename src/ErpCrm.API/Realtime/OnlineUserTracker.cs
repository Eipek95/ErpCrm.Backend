using System.Collections.Concurrent;

namespace ErpCrm.API.Realtime;

public static class OnlineUserTracker
{
    private static readonly ConcurrentDictionary<string, string>
        ConnectedUsers = new();

    public static void AddUser(
        string connectionId,
        string userIdentifier)
    {
        ConnectedUsers[connectionId] = userIdentifier;
    }

    public static void RemoveUser(string connectionId)
    {
        ConnectedUsers.TryRemove(connectionId, out _);
    }

    public static int GetOnlineUserCount()
    {
        return ConnectedUsers.Count;
    }

    public static List<string> GetOnlineUsers()
    {
        return ConnectedUsers.Values.Distinct().ToList();
    }
}