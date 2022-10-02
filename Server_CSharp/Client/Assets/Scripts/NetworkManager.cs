using DummyClient;
using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    ServerSession _session = new ServerSession();
    // Start is called before the first frame update
    void Start()
    {
        // DNS (Domain Name System)
        string host = Dns.GetHostName();
        IPHostEntry iPHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = iPHost.AddressList[0];
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

        Connector connector = new Connector();
        connector.Connect(endPoint, () => { return _session; }, 1);

        StartCoroutine("CoSendPacket");
    }

    // Update is called once per frame
    void Update()
    {
        IPacket packet = PacketQueue.Instance.Pop();
        if(packet != null)
        {
            PacketManager.Instance.HandlePacket(_session, packet);
        }
    }

    IEnumerator CoSendPacket()
    {
        while(true)
        {
            yield return new WaitForSeconds(3.0f);

            C2S_Chat_Req chatPacket = new C2S_Chat_Req();
            chatPacket.chat = "Hello Unity !";
            ArraySegment<byte> segment = chatPacket.Write();

            _session.Send(segment);
        }
    }
}
