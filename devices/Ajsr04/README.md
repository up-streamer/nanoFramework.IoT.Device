# JSN-SR04T - Ultrasonic Ranging Module
Drive for JSN-SR04T, AJ-SR04 and HC-SR04 ultrasonic ranging modules

This drive will work on pulse mode only on ESP32 MCUs due the use of RMT, embedded hardware particular to this chip family.

The JSN-SR04T and AJ-SR04 works on 3.3V as well on 5V.  
The HC-SR04 works on 5V generally, but it's possible to find versions capable to work on 3.3V too.

# Available modes 
Mode |    AJ-SR04    |    R19  | JSN-SR04T |  R27
---- | :-------------: | :--------:| :---------: | :------:
mode1|     Pulse     |   open  |   Pulse   | open
mode2|   Pulse_LP    |   300K  |     NA    |  NA
mode3|  Serial_Auto  |   120K  | Serial_Auto | 47K
mode3|  Serial_LP_Bin |   47K  | Serial_LP_Bin | 120K
mode4| Serial_LP_ASCII |   0K  |  NA          | NA

## Documentation

* [JSN-SR04T data sheet](https://www.makerguides.com/wp-content/uploads/2019/02/JSN-SR04T-Datasheet.pdf)

## Usage
```csharp
// Serial configuration and target selection done in Constants/ConfigSerial

sensor = new Serial_HCSR04(SensorType.AJ_SR04M, Mode.Serial_LP_Bin);

for (int i = 0; i < 500; i++)
{
    Length distance = sensor.GetDistance();
    Debug.WriteLine($"distance = {distance.Millimeters} mm" + $"--> count = {i}");
    Thread.Sleep(1000);
}
```
Notes:  
LP - Low Power mode. (AJ-SR04 can go down to 20uA in stand-by).  
BIN - Binary mode, 4 bytes sent from sensor.  
ASCII - ASCII mode, 12 bytes sent from sensor. 
Serial_Auto AJ-SR04 will produce a fix 100ms sample rate in binary mode, 
this mode was implemented using Timer instead to allow modify sample rate. Set resitors to Serial_LP_Bin for both sensors.  
Due this, the minimum time between samples will be 150ms.  
The sensor does not sends CRC in ASCII mode. The drive just check if the data frame is correct and probably the data will be too.  
For generic HC-SR04; select Pulse mode.  
Trigger and Echo pins are defaulted to serial port pins for ESP32, to allow tests without changing wires.  
Remember: If working with 5V sensors; use voltage level shift circuits for MCU that aren't 5V tolerant, as ESP32.
# Diagram

<p align="center">
  <img src="https://github.com/up-streamer/nanoFramework.IoT.Device/blob/develop/devices/Ajsr04/nf_SerialHC-SR04_Diag.png" width="300" title="ESP32 Dev.kit v1 nf Com2">
</p>

