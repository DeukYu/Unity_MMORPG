﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ServerCore;

namespace Server.Session
{
    abstract class Packet
    {
        public ushort size;
        public ushort packetId;

        public abstract ArraySegment<byte> Write();
        public abstract void Read(ArraySegment<byte> s);
    }

    public enum PacketID
    {
        PlayerInfoReq = 1,
        Test = 2,

    }

    interface IPacket
    {
        ushort Protocol { get; }
        void Read(ArraySegment<byte> segment);
        ArraySegment<byte> Write();
    }


    class PlayerInfoReq : IPacket
    {
        public byte testByte;
        public long playerId;
        public string name;

        public class Skill
        {
            public int id;
            public short level;
            public float duration;

            public class Attribute
            {
                public int att;
                public void Read(ReadOnlySpan<byte> s, ref ushort count)
                {
                    att = BitConverter.ToInt32(s.Slice(count, s.Length - count));
                    count += sizeof(int);
                }
                public bool Write(Span<byte> s, ref ushort count)
                {
                    bool bSuccess = true;
                    bSuccess &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), att);
                    count += sizeof(int);
                    return bSuccess;
                }
            }
            public List<Attribute> attributes = new List<Attribute>();
            public void Read(ReadOnlySpan<byte> s, ref ushort count)
            {
                id = BitConverter.ToInt32(s.Slice(count, s.Length - count));
                count += sizeof(int);
                level = BitConverter.ToInt16(s.Slice(count, s.Length - count));
                count += sizeof(short);
                duration = BitConverter.ToSingle(s.Slice(count, s.Length - count));
                count += sizeof(float);
                attributes.Clear();
                ushort attributeLen = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
                count += sizeof(ushort);

                for (int i = 0; i < attributeLen; ++i)
                {
                    Attribute attribute = new Attribute();
                    attribute.Read(s, ref count);
                    attributes.Add(attribute);
                }
            }
            public bool Write(Span<byte> s, ref ushort count)
            {
                bool bSuccess = true;
                bSuccess &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), id);
                count += sizeof(int);
                bSuccess &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), level);
                count += sizeof(short);
                bSuccess &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), duration);
                count += sizeof(float);
                attributes.Clear();
                ushort attributeLen = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
                count += sizeof(ushort);

                for (int i = 0; i < attributeLen; ++i)
                {
                    Attribute attribute = new Attribute();
                    attribute.Read(s, ref count);
                    attributes.Add(attribute);
                }
                return bSuccess;
            }
        }
        public List<Skill> skills = new List<Skill>();

        public ushort Protocol { get { return (ushort)PacketID.PlayerInfoReq; } }

        public void Read(ArraySegment<byte> segment)
        {
            ushort count = 0;

            ReadOnlySpan<byte> s = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
            count += sizeof(ushort);
            count += sizeof(ushort);
            testByte = segment.Array[segment.Offset + count];
            count += sizeof(byte);
            playerId = BitConverter.ToInt64(s.Slice(count, s.Length - count));
            count += sizeof(long);
            ushort nameLen = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
            count += sizeof(ushort);
            name = Encoding.Unicode.GetString(s.Slice(count, nameLen));
            count += nameLen;
            skills.Clear();
            ushort skillLen = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
            count += sizeof(ushort);

            for (int i = 0; i < skillLen; ++i)
            {
                Skill skill = new Skill();
                skill.Read(s, ref count);
                skills.Add(skill);
            }
        }
        public ArraySegment<byte> Write()
        {
            ArraySegment<byte> segment = SendBufferHelper.Open(4096);
            ushort count = 0;
            bool bSuccess = true;

            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);

            count += sizeof(ushort);
            bSuccess &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)PacketID.PlayerInfoReq);
            count += sizeof(ushort);
            segment.Array[segment.Offset + count] = testByte;
            count += sizeof(byte);
            bSuccess &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), playerId);
            count += sizeof(long);
            ushort nameLen = (ushort)Encoding.Unicode.GetBytes(name, 0, name.Length, segment.Array, segment.Offset + count + sizeof(ushort));
            bSuccess &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), nameLen);
            count += sizeof(ushort);
            count += nameLen;
            skills.Clear();
            ushort skillLen = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
            count += sizeof(ushort);

            for (int i = 0; i < skillLen; ++i)
            {
                Skill skill = new Skill();
                skill.Read(s, ref count);
                skills.Add(skill);
            }
            bSuccess &= BitConverter.TryWriteBytes(s, count);
            if (bSuccess == false)
                return null;
            return SendBufferHelper.Close(count);
        }
    }
    class ClientSession : PacketSession
    {
        public int SessionId { get; set; }
        public GameRoom Room { get; set; }
        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected : {endPoint}");

            Program.Room.Push(() => Program.Room.Enter(this));
        }
        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            PacketManager.Instance.OnRecvPacket(this, buffer);
        }
        public override void OnDisconnected(EndPoint endPoint)
        {
            SessionManager.Instance.Remove(this);
            if(Room != null)
            {
                GameRoom room = Room;
                room.Push(() => room.Leave(this));
                Room = null;
            }
            Console.WriteLine($"OnDisconnected : {endPoint}");
        }
        public override void OnSend(int numOfBytes)
        {
            //Console.WriteLine($"Transferred byte : {numOfBytes}");
        }
    }
}