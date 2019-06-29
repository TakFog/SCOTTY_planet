using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class CommunicationBehaviour : MonoBehaviour {

    private const short LOOT = 1000, REQUEST = 1001, DAMAGE = 1002;

    public NetworkManager networkManager;

    private GolemGenerator golemGenerator;
    private Vector3 golemPosition;

    NetworkClient client;

    // Use this for initialization
    void Start()
    {
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        Log("Connecting to server "+networkManager.networkAddress);
        
        client = new NetworkClient();

        client.RegisterHandler(MsgType.Connect, OnMessageReceive);
        client.RegisterHandler(REQUEST, OnMessageReceive);
        client.Connect(networkManager.networkAddress, networkManager.networkPort);

        golemGenerator = GameObject.FindGameObjectWithTag("Player").GetComponent<GolemGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        Log(networkManager.networkAddress);
        if(!client.isConnected)
        {
            Log("Non connesso");
        }        
    }

    private void SendMsg(short type, MessageBase msg)
    {
        if (client.isConnected)
        {
            client.Send(type, msg);
        }
        else
        {
            Log("Non connesso");
        }
    }

    public void SendLootMsg()
    {
        Log("Sending LOOT message");
        SendMsg(LOOT, new EmptyMessage());
    }

    public void SendGolemRequestMessage(Vector3 golemPosition)
    {
        Log("Sending REQUEST message");
        this.golemPosition = golemPosition;
        SendMsg(REQUEST, new EmptyMessage());
    }

    public void SendDamageMessage()
    {
        Log("Sending DAMAGE message");
        SendMsg(DAMAGE, new EmptyMessage());
    }

    void OnMessageReceive(NetworkMessage netMsg)
    {
        Log("Received message: " + netMsg.msgType);

        switch (netMsg.msgType)
        {
            case MsgType.Connect:
                {
                    Log("Connected to the server");
                    break;
                }
            case REQUEST:
                {
                    Debug.Log("Received Golem");
                    var message = netMsg.ReadMessage<StringMessage>();
                    CardMessage golem = JsonUtility.FromJson<CardMessage>(message.value);
                    if (golem.success == 1)
                        golemGenerator.GenerateGolem(golem, golemPosition);
                    else
                        GameObject.Find("NetworkManager").GetComponent<AudioSource>().Play();
                    break;
                }
        }
    }

    private void Log(string msg)
    {
        Debug.Log("[" + DateTime.UtcNow + "] "+msg);
    }
}