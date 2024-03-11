using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using WebSocketSharp;
public class WebSocketController : MonoBehaviour
{

    String esp32IPAddress = "10.204.0.245";
    String esp32WebsocketPort = "81";
    // Websocket Service
    WebSocket ws;
    public NarrationController narrationControllerScript;
    // Start is called before the first frame update
    void OnEnable()
    {
        ConnectWithESP32();
    }

    void Start(){
    //narrationController.GetComponent<NarrationController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method to connect ESP32
    public void ConnectWithESP32()
    {
        Debug.Log("Connecting Unity with ESP32 via Websockets...");
        ws = new WebSocket("ws://" + esp32IPAddress + ":" + esp32WebsocketPort);
        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket connected");
            ws.Send("Hello from Unity!");
        };
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Received message: " + e.Data);
            if(e.Data.Equals("Start Narration", StringComparison.OrdinalIgnoreCase)){
                narrationControllerScript.startNarration = true;
            }
          
        };
        ws.Connect();
        Debug.Log("Websocket state - " + ws.ReadyState);
    }

    //Closing websocket upon application quit
    void OnApplicationQuit()
    {
        ws.Close();
        Debug.Log("WebSocket closed on application exit");
    }
}
