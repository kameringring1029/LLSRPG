using System;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class WebsocketAccessor : MonoBehaviour
{
    private WebSocket ws;

    private void Start()
    {
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
        ws.Close();
    }
}

