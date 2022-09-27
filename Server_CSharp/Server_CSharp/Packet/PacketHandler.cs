using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;

class PacketHandler
{
	public static void C2S_PlayerInfoReqHandler(PacketSession session, IPacket packet)
	{
		C2S_PlayerInfoReq p = packet as C2S_PlayerInfoReq;

		Console.WriteLine($"PlayerInfoReq: {p.playerId} {p.name}");

		foreach (C2S_PlayerInfoReq.Skill skill in p.skills)
		{
			Console.WriteLine($"Skill({skill.id})({skill.level})({skill.duration})");
		}
	}
}
