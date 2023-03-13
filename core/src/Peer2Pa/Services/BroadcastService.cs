using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Peer2Pa.Services;

public class BroadcastService : BackgroundService {

    private readonly ILogger<BroadcastService> _logger;

	public BroadcastService(ILogger<BroadcastService> logger) {
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
		_logger.LogInformation("Starting broadcast...");
		await Broadcast(stoppingToken);
	}

	private async Task Broadcast(CancellationToken ct) {

		var server = new UdpClient(8888);
		var knowledge = Encoding.ASCII.GetBytes("test server");

		while (!ct.IsCancellationRequested) {
			_logger.LogInformation("Broadcasting");

			var result = await server.ReceiveAsync(ct);
			var data = Encoding.ASCII.GetString(result.Buffer);
			_logger.LogInformation("received");						
			await server.SendAsync(knowledge, result.RemoteEndPoint, ct);
		}

		server.Close();
		server.Dispose();		
	}

	public override async Task StopAsync(CancellationToken cancellationToken) {
		_logger.LogInformation("Stopping broadcast...");
		await base.StopAsync(cancellationToken);
	}
}

