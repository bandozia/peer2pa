using System.Net;
using System.Net.Sockets;

namespace Peer2Pa; 

public record ClientInfo(string Ip, string Name);

public class Knowledge {

    public ClientInfo ClientInfo { get; init; }

    public Knowledge() {
       ClientInfo = GetClientInfo();                
    }

    public static ClientInfo GetClientInfo() {
		using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0);
		socket.Connect("8.8.8.8", 65530);

        var ep = socket.LocalEndPoint as IPEndPoint;
        
        socket.Close();
        return new ClientInfo(ep?.Address.ToString() ?? "undefined", Environment.MachineName);
	}
}
