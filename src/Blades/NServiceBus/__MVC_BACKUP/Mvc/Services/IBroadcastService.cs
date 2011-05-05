namespace Mvc.Services {
    using System;

    public interface IBroadcastService {
        Guid Broadcast(string message);
    }
}