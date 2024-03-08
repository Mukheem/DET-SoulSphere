/*
  Minimal Esp32 Websockets Server&Client/ Reading capacitive touch board

  This sketch:
        1. Connects to a WiFi network
        2. Starts a websocket server on port 81
        3. Once a client is available,it connects with it.
        4. When the client is connected,it reads the state of the capacitive touch board 
        5. If the capacitive touch board is pressed, it sends "Start Narration" message to the client, and turn on Esp32 LED_PIN 
        6. Closes the connection and when client is unavailable.
 
  Base Example By Gil Maimon, Modifications & additional code by Mukheem.
  https://github.com/gilmaimon/ArduinoWebsockets
*/

//Library
#include <WiFi.h>   
#include <ArduinoWebsockets.h>

const char* ssid = "dsv-extrality-lab";            // Replace with your network SSID
const char* password = "expiring-unstuck-slider";  // Replace with your network password

using namespace websockets;                        // Enable access to websockets classes & functions

WebsocketsServer server;                           // Initialize a WebSocket server
WebsocketsClient client;                           // Initialize a WebSocket client

const int TOUCH_BUTTON_PIN = A5;                   // Input pin for touch state
const int LED_PIN = 13;                            // Pin number for LED
const int THERMALPAD_BUTTON_PIN = A3;              // Input pin for capacitive touch board
int buttonState = 0;                               // variable for reading the THERMALPAD_BUTTON_PIN status; capacitive touch board
bool messageSent = false;                          // variable to track a message sent to the WebSocket client
void setup() {
  Serial.begin(115200);                            // Initialize serial communication baud rate
  delay(1000);

  Serial.println("Trying to Connect to WiFi");    // Print a message indicating an attempt to connect to WiFi
  WiFi.begin(ssid, password);                     // Trying to connect to WiFi

  // Trying to connect to WiFi within every 1 second until it get connected
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.println("Trying to Connect to WiFi");
  }
  Serial.println("Connected to WiFi");            // Print a message indicating WiFi connection is successful
  // Print Esp32 local IP Address  
  Serial.print("IP Address: ");                  
  Serial.println(WiFi.localIP());

  pinMode(TOUCH_BUTTON_PIN, INPUT);              // Configure touch board pin as input
  pinMode(THERMALPAD_BUTTON_PIN, OUTPUT);        // Configure capacitive touch board pin as OUTPUT
  // Configure LED pin as output
  //pinMode(LED_PIN, OUTPUT);

  server.listen(81);                           // Initialising websocket server on port 81
  Serial.print("Is server live? ");            // Print a message indicating whether the WebSocket server is active
  Serial.println(server.available());          // Print the number of available WebSocket connections
}

void loop() {
/*
  if (server.poll()) {                         //server.poll() checks if any client is waiting to connect
    Serial.println("Client is available to connect...");
    client = server.accept();                 // Accept() --> what server.accept does, is: "server, please wait until there is a client knocking on the door. when there is a client knocking, let him in and give me it's object".
    Serial.println("Client connected...");

    while (client.available()) {              // Loop while there is data available from the WebSocket client
  */    
      // Read the state of the capacitive touch board
      buttonState = digitalRead(TOUCH_BUTTON_PIN);  
      
      if (buttonState == HIGH) {              // if capacitive touch board is pressed
        digitalWrite(LED_PIN, HIGH);          // Turn on Esp32 LED pin
        Serial.println("Touch detected");     // Print a message indicating touch board is pressed
        digitalWrite(THERMALPAD_BUTTON_PIN,HIGH);
        digitalWrite(13, HIGH);               //Question?

        Serial.flush();
/*
        // send a message to WebSocket client only once 
        if(!messageSent){
           client.send("Start Narration");     
           messageSent = true;
        }
       
      }
    }

    // Cheking if WebSocket client is Disconnected from Esp32
    if(!client.available())
    {
      Serial.println("Client Disconnected.");
       messageSent = false;
    }*/
  }
  ////////////////////////////////////
  /*
  /// Read the state of the capacitive touch board
  buttonState = digitalRead(TOUCH_BUTTON_PIN);

  // If a touch is detected, turn on the LED
  if (buttonState == HIGH) {
    //digitalWrite(LED_PIN, HIGH);
    Serial.println("Touch detected");
    //digitalWrite(THERMALPAD_BUTTON_PIN,HIGH);
  }  //else {
     //digitalWrite(LED_PIN, LOW);
     //}
     */
}
