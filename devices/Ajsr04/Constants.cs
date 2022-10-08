// NOTE: when working with ESP32 this define needs to be uncommented
#define BUIID_FOR_ESP32

using System;
using System.IO.Ports;
#if BUIID_FOR_ESP32
using nanoFramework.Hardware.Esp32;
#endif

namespace Iot.Device.Ajsr04
{
    namespace Constants
    {
        /// <summary>
        /// Sensor available types
        /// </summary>
        public enum SensorType
        {
            JSN_SR04T = 0x55,
            AJ_SR04M = 0x01,
        }

        public enum Mode
        {
            Serial_Auto,
            Serial_LP_Bin,
            Serial_LP_ASCII,
        }

        public enum Status
        {
            Ok,
            TimeOut,
            DataError,
            ConfigError,
            DataCheckError,
        }

        public static class Constant
        {
            public const float Speed_of_Sound = 340.29F;
        }
    }

    namespace Config
    {
        public class ConfigSerial
        {
            public SerialPort Device;
            public void Config(string port)
            {
                Configuration.SetPinFunction(17, DeviceFunction.COM2_TX);
                Configuration.SetPinFunction(16, DeviceFunction.COM2_RX);

                Device = new SerialPort(port)
                {
                    // set parameters
                    BaudRate = 9600,
                    Parity = Parity.None,
                    StopBits = StopBits.One,
                    Handshake = Handshake.None,
                    DataBits = 8,
                    Mode = SerialMode.Normal,
                    // set Timout,
                    WriteTimeout = 1000,
                    ReadTimeout = 1000,
                    //WatchChar = 'x',     // \x0ff
                };
                Device.Open();

            }
        }
    }
}
