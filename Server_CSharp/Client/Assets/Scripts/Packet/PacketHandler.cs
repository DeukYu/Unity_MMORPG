using DummyClient;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class PacketHandler
{
    public static void S2C_BroadcastEnterGameHandler(PacketSession session, IPacket packet)
    {
        S2C_BroadcastEnterGame pkt = new S2C_BroadcastEnterGame();
        ServerSession serverSession = session as ServerSession;

        PlayerManager.Instance.EnterGame(pkt);
    }
    public static void S2C_BroadcastLeaveGameHandler(PacketSession session, IPacket packet)
    {
        S2C_BroadcastLeaveGame pkt = new S2C_BroadcastLeaveGame();
        ServerSession serverSession = session as ServerSession;
        PlayerManager.Instance.LeaveGame(pkt);
    }
    public static void S2C_PlayerListHandler(PacketSession session, IPacket packet)
    {
        S2C_PlayerList pkt = new S2C_PlayerList();
        ServerSession serverSession = session as ServerSession;

        PlayerManager.Instance.Add(pkt);
    }
    public static void S2C_BroadcastMoveHandler(PacketSession session, IPacket packet)
    {
        S2C_BroadcastMove pkt = new S2C_BroadcastMove();
        ServerSession serverSession = session as ServerSession;
        PlayerManager.Instance.Move(pkt);
    }
}
