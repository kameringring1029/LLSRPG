using System;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class WebsocketAccessor : MonoBehaviour
{
    private void Start()
    {
        // サーバー側で
        using (WebSocket ws = new WebSocket("ws://localhost:8080/ws/app/"))
        {
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
            // 接続.
            ws.Connect();
            // メッセージ送信.
            ws.Send("Hello, World");
        }
    }
}

