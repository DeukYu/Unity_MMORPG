using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class PacketHandler
{
    public static void S2C_PlayerInfoReqHandler(PacketSession session, IPacket packet)
    {
        S2C_PlayerInfoReqHandler p = packet as S2C_PlayerInfoReqHandler;

        Console.WriteLine($"PlayerInfoReq: {p.playerId} {p.name}");

        foreach (S2C_PlayerInfoReqHandler.Skill skill in p.skills)
        {
            Console.WriteLine($"Skill({skill.id})({skill.level})({skill.duration})");
        }
    }

    public static void TestHandler(PacketSession session, IPacket packet)
    {

    }
}