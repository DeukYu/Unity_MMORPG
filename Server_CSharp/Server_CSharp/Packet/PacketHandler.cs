using Server.Session;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;

class PacketHandler
{
	public static void S2C_Chat_ResHandler(PacketSession session, IPacket packet)
	{
		S2C_Chat_Res? chatPacket = packet as S2C_Chat_Res;
		ClientSession? clientSession = session as ClientSession;

		if (clientSession.Room == null)
			return;

		clientSession.Room.Broadcast(clientSession, chatPacket.chat);
	}
}
