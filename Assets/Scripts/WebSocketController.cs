using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using WebSocketSharp;

public class WebSocketController : MonoBehaviour
{

    String esp32IPAddress = "10.204.0.245";
    String esp32WebsocketPort = "81";
    // Websocket Service
    public WebSocket ws;
    public NarrationController narrationControllerScript;

    // Serial Port to which Arduino is connected
    SerialPort arduinoPort = new SerialPort("COM4", 115200);
    // Start is called before the first frame update
    public void OnEnable()
    {
        //ConnectWithESP32();
        //ConnectWithArduino(true);
    }

    public void Start(){
        ConnectWithESP32();
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
            //ws.Send("Need input");
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

    // Method to connect/disconnect Arduino
    public void ConnectWithArduino(bool makeConnection)
    {
        try
        {

            if (makeConnection)
            {
                Debug.Log("Connecting with Arduino...");
                arduinoPort.Open();

                if (arduinoPort.IsOpen)
                {
                    Debug.LogAssertion("Connection with Arduino established...");
                }
            }
            else
            {
                if (arduinoPort.IsOpen)
                {
                    Debug.Log("Disconnecting Arduino...");
                    arduinoPort.Close();
                    if (!arduinoPort.IsOpen)
                    {
                        Debug.LogAssertion("Connection with Arduino is now broke...");
                    }
                }
            }


        }
        catch (System.Exception)
        {
            throw;
        }
    }


    // Method to read data from Arduino
    public int ReadFromArduino()
    {
        int valueFromArduinoSensor = 0;

        //arduinoPort.ReadTimeout = 50000;
        if (arduinoPort.IsOpen)
        {
            valueFromArduinoSensor = Int32.Parse(arduinoPort.ReadLine());
            Debug.Log("Data From Arduino:" + valueFromArduinoSensor);
        }

        return valueFromArduinoSensor;
    }
    //Closing websocket upon application quit
    void OnApplicationQuit()
    {
        ws.Close();
        Debug.Log("WebSocket closed on application exit");
    }
}
