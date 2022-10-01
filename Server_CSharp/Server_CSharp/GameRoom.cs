using Server.Session;
using ServerCore;

namespace Server
{
    class GameRoom : IJobQueue
    {
        List<ClientSession> _sessions = new List<ClientSession>();
        JobQueue _jobQueue = new JobQueue();
        public void Push(Action job)
        {
            _jobQueue.Push(job);
        }
        public void Broadcast(ClientSession session, string chat)
        {
            S2C_Chat_Res packet = new S2C_Chat_Res();
            packet.playerId = session.SessionId;
            packet.chat = $"{chat}I am {packet.playerId}";
            ArraySegment<byte> segment = packet.Write();

            foreach (ClientSession s in _sessions)
                s.Send(segment);
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
