using Server;
using Server.Session;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;

class PacketHandler
{
	public static void C2S_Chat_ReqHandler(PacketSession session, IPacket packet)
	{
		C2S_Chat_Req? chatPacket = packet as C2S_Chat_Req;
		ClientSession? clientSession = session as ClientSession;

		if (clientSession.Room == null)
			return;

		GameRoom room = clientSession.Room;
		room.Push(() => room.Broadcast(clientSession, chatPacket.chat));
	}
}
