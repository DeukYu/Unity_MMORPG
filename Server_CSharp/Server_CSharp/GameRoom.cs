using Server.Session;
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

            //Console.WriteLine($"Flushed {_pendingList.Count} items");
            _pendingList.Clear();
        }
        public void Broadcast(ArraySegment<byte> segment)
        {
            _pendingList.Add(segment);
        }
        public void Enter(ClientSession session)
        {
            // 플레이어 추가
            _sessions.Add(session);
            session.Room = this;

            // 뉴 유저에게 모든 플레이 목록 전송
            S2C_PlayerList players = new S2C_PlayerList();
            foreach(ClientSession s in _sessions)
            {
                players.players.Add(new S2C_PlayerList.Player()
                {
                    isSelf = (s == session),
                    playerId = s.SessionId,
                    posX = s.PosX,
                    posY = s.PosY,
                    posZ = s.PosZ,
                });
                session.Send(players.Write());
            }
            // 모두에게 뉴 유저 입장 알림
            S2C_BroadcastEnterGame enter = new S2C_BroadcastEnterGame();
            enter.playerId = session.SessionId;
            enter.posX = 0;
            enter.posY = 0;
            enter.posZ = 0;
            Broadcast(enter.Write());
        }
        public void Leave(ClientSession session)
        {
            // 플레이어 제거
            _sessions.Remove(session);

            // 모두에게 알린다.
            S2C_BroadcastLeaveGame leave = new S2C_BroadcastLeaveGame();
            leave.playerId = session.SessionId;
            Broadcast(leave.Write());
        }
        public void Move(ClientSession session, C2S_Move packet)
        {
            // 좌표 바꿔주고
            session.PosX = packet.posX;
            session.PosY = packet.posY;
            session.PosZ = packet.posZ;
            // 모두에게 알린다.
            S2C_BroadcastMove pkt = new S2C_BroadcastMove();
            pkt.playerId = session.SessionId;
            pkt.posX = packet.posX;
            pkt.posY = packet.posY;
            pkt.posZ = packet.posZ;
            Broadcast(pkt.Write());
        }
    }
}
