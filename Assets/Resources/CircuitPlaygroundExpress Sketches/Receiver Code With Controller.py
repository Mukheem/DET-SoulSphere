# SPDX-FileCopyrightText: 2018 Kattni Rembor for Adafruit Industries
#
# SPDX-License-Identifier: MIT
import time
import pulseio
import board
import adafruit_irremote
import adafruit_irremote
import digitalio
from adafruit_circuitplayground import cp


# Create a 'pulseio' input, to listen to infrared signals on the IR receiver
pulsein = pulseio.PulseIn(board.IR_RX, maxlen=120, idle_state=True)
# Create a decoder that will take pulses and turn them into numbers
decoder = adafruit_irremote.GenericDecode()

lraMotor = digitalio.DigitalInOut(board.A2)
lraMotor.direction = digitalio.Direction.OUTPUT
print("Hello World")

cp.pixels.brightness = 0.15

while True:
    pulses = decoder.read_pulses(pulsein)
    print(pulses)
    try:
        # Attempt to convert received pulses into numbers
        received_code = decoder.decode_bits(pulses)
    except adafruit_irremote.IRNECRepeatException:
        # We got an unusual short code, probably a 'repeat' signal
        # print("NEC repeat!")
        continue
    except adafruit_irremote.IRDecodeException as e:
        # Something got distorted or maybe its not an NEC-type remote?
        # print("Failed to decode: ", e.args)
        continue

    print("NEC Infrared code received: ", received_code)
    if received_code == (255, 2, 255, 0):
        print("Vibrating now !!")
        cp.pixels.fill((50, 150, 150))
        lraMotor.value = True
        time.sleep(5)
        lraMotor.value = False
        cp.pixels.fill((0, 0, 0))
    if received_code == (239, 55, 51, 204):
        print("Vibrating now !!")
        cp.pixels.fill((50, 150, 150))
        lraMotor.value = True
        time.sleep(5)
        lraMotor.value = False
        cp.pixels.fill((0, 0, 0))
    if received_code == (239, 55, 99, 156):
        print("Vibrating now !!")
        cp.pixels.fill((50, 150, 150))
        lraMotor.value = True
        time.sleep(5)
        lraMotor.value = False
        cp.pixels.fill((0, 0, 0))

    """
    if received_code == (255, 2, 127, 128):
        print("Received NEC Play/Pause")
    if received_code == (255, 2, 191, 64):
        print("Received NEC Vol+")
    """
