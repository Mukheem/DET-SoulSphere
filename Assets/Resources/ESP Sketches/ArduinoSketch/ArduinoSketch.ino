const int TOUCH_BUTTON_PIN = 8;
const int LED_PIN = 13;
const int THERMALPAD_BUTTON_PIN = 7;

int touchReadingState = LOW;
void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);

  pinMode(TOUCH_BUTTON_PIN, INPUT_PULLUP);
  pinMode(THERMALPAD_BUTTON_PIN, OUTPUT);
  pinMode(LED_PIN, OUTPUT);
}

void loop() {
  // put your main code here, to run repeatedly:
  Serial.flush();
  touchReadingState = digitalRead(TOUCH_BUTTON_PIN);
  if (touchReadingState == HIGH) {
    digitalWrite(LED_PIN, HIGH);
    digitalWrite(THERMALPAD_BUTTON_PIN, HIGH);
    Serial.println("Touched");
  } else {
    digitalWrite(LED_PIN, LOW);
    digitalWrite(THERMALPAD_BUTTON_PIN, LOW);
    //Serial.println("Touch released");
  }
}
