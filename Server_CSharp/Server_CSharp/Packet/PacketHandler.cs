﻿using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class PacketHandler
{
    public static void C2S_PlayerInfoReqHandler(PacketSession session, IPacket packet)
    {
        C2S_PlayerInfoReqHandler p = packet as C2S_PlayerInfoReqHandler;

        Console.WriteLine($"PlayerInfoReq: {p.playerId} {p.name}");

        foreach (C2S_PlayerInfoReqHandler.Skill skill in p.skills)
        {
            Console.WriteLine($"Skill({skill.id})({skill.level})({skill.duration})");
        }
    }

    public static void TestHandler(PacketSession session, IPacket packet)
    {

    }
}