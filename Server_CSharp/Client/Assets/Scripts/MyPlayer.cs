using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : Player
{
    NetworkManager _network;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CoSendPacket");
        _network = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator CoSendPacket()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);

            C2S_Move pkt = new C2S_Move();
            pkt.posX = UnityEngine.Random.RandomRange(-50, 50);
            pkt.posY = 0;
            pkt.posZ = UnityEngine.Random.RandomRange(-50, 50);
            _network.Send(pkt.Write());
        }
    }
}
