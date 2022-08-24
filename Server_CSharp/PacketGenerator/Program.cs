using System;
using System.Xml;

namespace PacketGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlReaderSettings settings = new XmlReaderSettings()
            {
                IgnoreComments = true,  // 주석 무시
                IgnoreWhitespace = true // 스페이스바 무시
            };

            using (XmlReader reader = XmlReader.Create("PDL.xml", settings))
            {
                reader.MoveToContent();

                while(reader.Read())
                {
                    if (reader.Depth == 1 && reader.NodeType == XmlNodeType.Element)
                        ParsePacket(reader);
                    //Console.WriteLine(reader.Name + " " + reader["name"]);
                }
            }
        }

        public static void ParsePacket(XmlReader _reader)
        {
            if (_reader.NodeType == XmlNodeType.EndElement)
                return;

            if (_reader.Name.ToLower() != "packet")
            {
                Console.WriteLine("Invalid packet node");
                return;
            }

            string packetName = _reader["name"];
            if(string.IsNullOrEmpty(packetName))
            {
                Console.WriteLine("Packet without name");
            }

            ParseMembers(_reader);
        }
        public static void ParseMembers(XmlReader _reader)
        {
            string packetName = _reader["name"];

            int depth = _reader.Depth + 1;
            while(_reader.Read())
            {
                if (_reader.Depth != depth)
                    break;

                string memberName = _reader["name"];
                if(string.IsNullOrEmpty(memberName))
                {
                    Console.WriteLine("Member without name");
                    return;
                }

                string memberType = _reader.Name.ToLower();
                switch(memberType)
                {
                    case "bool":
                    case "byte":
                    case "short":
                    case "ushort":
                    case "int":
                    case "long":
                    case "float":
                    case "double":
                    case "string":
                    case "list":
                        break;
                    default:
                        break;
                }
            }
        }
    }
}