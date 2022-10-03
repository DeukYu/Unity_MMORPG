using Server;
using Server.Session;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;

class PacketHandler
{
	public static void C2S_LeaveGameHandler(PacketSession session, IPacket packet)
	{
		ClientSession? clientSession = session as ClientSession;

		if (clientSession.Room == null)
			return;

		GameRoom room = clientSession.Room;
		room.Push(() => room.Leave(clientSession));
	}
	public static void C2S_MoveHandler(PacketSession session, IPacket packet)
    {
		C2S_Move pkt = packet as C2S_Move;
		ClientSession? clientSession = session as ClientSession;

		if (clientSession.Room == null)
			return;

        //Console.WriteLine($"{pkt.posX}, {pkt.posY}, {pkt.posZ}");

		GameRoom room = clientSession.Room;
		room.Push(() => room.Move(clientSession, pkt));
	}
}
