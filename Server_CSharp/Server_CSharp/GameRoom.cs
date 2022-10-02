﻿using Server.Session;
using ServerCore;

namespace Server
{
    class GameRoom : IJobQueue
    {
        List<ClientSession> _sessions = new List<ClientSession>();
        JobQueue _jobQueue = new JobQueue();
        List<ArraySegment<byte>> _pendingList = new List<ArraySegment<byte>>();
        public void Push(Action job)
        {
            _jobQueue.Push(job);
        }
        public void Flush()
        {
            foreach (ClientSession s in _sessions)
                s.Send(_pendingList);

            Console.WriteLine($"Flushed {_pendingList.Count} items");
            _pendingList.Clear();
        }
        public void Broadcast(ClientSession session, string chat)
        {
            S2C_Chat_Res packet = new S2C_Chat_Res();
            packet.playerId = session.SessionId;
            packet.chat = $"{chat}I am {packet.playerId}";
            ArraySegment<byte> segment = packet.Write();

            _pendingList.Add(segment);
            //foreach (ClientSession s in _sessions)
            //    s.Send(segment);
        }
        public void Enter(ClientSession session)
        {
            _sessions.Add(session);
            session.Room = this;
        }
        public void Leave(ClientSession session)
        {
            _sessions.Remove(session);
        }
    }
}