using DummyClient;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;

class PacketHandler
{
    public static void S2C_Chat_ResHandler(PacketSession session, IPacket packet)
    {
        S2C_Chat_Res chatPacket = packet as S2C_Chat_Res;
        ServerSession serverSession = session as ServerSession;

        //if(chatPacket.playerId == 1)
        //Console.WriteLine(chatPacket.chat);
    }
}
