using System;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class WebsocketAccessor : MonoBehaviour
{
    private WebSocket ws;
    private EnemyMgr EM;

    private void Start()
    {
        EM = gameObject.GetComponent<EnemyMgr>();

//        ws = new WebSocket("ws://localhost:8080/ws");
        ws = new WebSocket("ws://koke.link:8080/ws");
        // 接続開始時のイベント.
        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("Opended");
        };
        // メッセージ受信時のイベント.
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Received " + e.Data);

            EM.enqRecvMsg(e.Data);

            /*
            switch (e.Data)
            {
                case "A":
                    EM.
                    break;
                case "B":
                    GM.pushB();
                    break;
                case "U":
                    GM.pushArrow(0,1);
                    break;
                case "D":
                    GM.pushArrow(0,-1);
                    break;
                case "R":
                    GM.pushArrow(1,0);
                    break;
                case "L":
                    GM.pushArrow(-1,0);
                    // gameObject.GetComponent<ControllerButtons>().onClickLeft();
                    break;

            }
            */
            
        };
        ws.OnError += (sender, e) =>
        {
            Debug.Log("ERROR");
        };

        // 接続.
        ws.Connect();

        // メッセージ送信.
        if(ws.IsAlive)
            ws.Send("Hello, World");
    }

    private void OnDestroy()
    {
        if (ws.IsAlive)
            ws.Close();
    }



    public void sendws(string message)
    {
        if (ws.IsAlive)
            ws.Send(message);

    }
}

