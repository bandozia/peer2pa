using Microsoft.AspNetCore.SignalR;

namespace Peer2Pa.Ws; 

public class MainHub : Hub {
	public async Task Ping(string msg) {
		await Clients.All.SendAsync("Pong", msg);
	}
}
