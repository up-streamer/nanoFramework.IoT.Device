// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

// NOTE: when working with ESP32 this define needs to be uncommented
#define BUIID_FOR_ESP32

using System;
using System.Threading;
using System.Diagnostics;
using System.IO.Ports;
using System.Device.Model;
using Iot.Device.Ajsr04.Constants;
using Iot.Device.Ajsr04.Config;
using UnitsNet;


namespace Iot.Device.Ajsr04
{
    public delegate void _NewValuesCallback(int distance, Status sta);
    /// <summary>
    /// AJ-SR04 - Ultrasonic Ranging Module
    /// </summary>
    [Interface("AJ-SR04 - Ultrasonic Ranging Module")]
    public class Ajsr04 : IDisposable
    {
        private byte[] _data;
        private uint _sum;
        private uint _dataCheck;
        private readonly byte[] _ping = new byte[1];
        private readonly string port;
        public readonly SensorType sensorType;
        public readonly Mode sensorMode;

        readonly ConfigSerial _serial = new();
        private delegate int GetDistanceDelegate();
        GetDistanceDelegate _getDistance;
        private static Timer _getDistanceTimer;

        private readonly int _triggerPin = 0;

        public Status status;

        private int distance;
        public Length Distance
        {
            get { return Length.FromMillimeters(distance); }
        }

        private int _readInterval;
        public int ReadInterval
        {
            set
            {
                _readInterval -= 100;
                _readInterval = _readInterval < 100 ? 100 : value;
                _getDistanceTimer.Change(0, _readInterval);
            }
            get
            {
                return _readInterval;
            }
        }

        // * For debuging raw data from sensor *
        readonly PrintRaw PR = new();

        /// <summary>
        /// Creates a new instance of the AJ-SCR04 sonar.
        /// </summary>
        /// <param name="SensorType">Define Tx ping byte</param>
        /// <param name="Mode">Define Sensor operating mode, match to mode resistor on PCB</param>
        public Ajsr04(SensorType type, Mode mode)
        {
            status = Status.Ok;
            // Define Tx ping byte
            sensorType = type;
            // Define Sensor mode
            sensorMode = mode;
            // Define ping byte according to sensorType
            _ping[0] = (byte)sensorType;
            // Default ReadInterval to GetDistanceAUTO;
            _readInterval = 1000;

            // List available ports
            var serialPorts = SerialPort.GetPortNames();

            foreach (string port in serialPorts)
            {
                Debug.WriteLine("Avail. Port - " + port);
            }
#if BUIID_FOR_ESP32
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // COM2 in ESP32-WROVER-KIT mapped to free GPIO pins
            // mind to NOT USE pins shared with other devices, like serial flash and PSRAM
            // also it's MANDATORY to set pin funcion to the appropriate COM before instanciating it

            // set GPIO functions for COM2 (this is UART2 on ESP32 WROOM32)
            //Configuration.SetPinFunction(17, DeviceFunction.COM2_TX);
            //Configuration.SetPinFunction(16, DeviceFunction.COM2_RX);

            // open COM2
            port = "COM2";
            ConfigPins(sensorMode);
            ConfigMode(sensorMode);
#else
			///////////////////////////////////////////////////////////////////////////////////////////////////
			// COM6 in STM32F769IDiscovery board (Tx, Rx pins exposed in Arduino header CN13: TX->D1, RX->D0)
			
			// open COM6
			port = "COM6";
			ConfigPins(sensorMode);
			ConfigMode(sensorMode);
#endif
        }

        private void ConfigPins(Mode sensorMode)
        {
                if (sensorMode != Mode.Serial_Auto)
                {
                    _serial.Config(port);
                }
        }

        private void ConfigMode(Mode sensorMode)
        {
            switch (sensorMode)
            {
                case Mode.Serial_Auto:
                    GetDistanceAUTO gda = new(port, sensorType, NewValues);
                    _getDistanceTimer = new Timer(gda.ThreadProcess, null, 0, _readInterval);
                    return;

                case Mode.Serial_LP_Bin:
                    _getDistance = GetDistanceBIN;
                    return;

                case Mode.Serial_LP_ASCII:
                    if (sensorType == SensorType.AJ_SR04M)
                    {
                        _getDistance = GetDistanceASCII;
                    }
                    else
                    {
                        _getDistance = GetDistanceBIN;
                    }
                    return;
            }
        }

        private void NewValues(int dist, Status sta)
        {
            distance = dist;
            status = sta;
        }

        /// <summary>
        /// Gets the current distance, usual range from 20 cm to 600 cm.
        /// </summary>
        [Telemetry]
        public Length GetDistance()
        {
            status = Status.Ok;
            SensorPing(_ping);
            return Length.FromMillimeters(_getDistance());
        }

        private void SensorPing(byte[] ping)
        {
            if (_triggerPin == 0)
            {
                _serial.Device.Write(ping, 0, ping.Length);
                Thread.Sleep(50);
            }
        }

        private int GetDistanceBIN()
        {
            // Attempt to read 4 bytes from the Serial Device input stream
            // Format: 0XFF + H_DATA + L_DATA + SUM
            if (_serial.Device.BytesToRead == 4)
            {
                _data = new byte[_serial.Device.BytesToRead];
                _serial.Device.Read(_data, 0, _data.Length);

                distance = (_data[1] << 8) | _data[2];
                _sum = _data[3];
                _dataCheck = (uint)(_data[0] + _data[1] + _data[2] + 1) & 0x00ff;

                // * For debuging data received *
                PR.ViewData(_data, _sum, _dataCheck);

                if (_dataCheck == _sum)
                {
                    return distance;
                }
                status = Status.DataCheckError;
                return -1;
            }
            status = Status.DataError;
            return -1;
        }
        private int GetDistanceASCII()
        {
            // Attempt to read 12 bytes from the Serial Device input stream
            // For mode 5, computer printer mode (ASCII) with trigger
            // Data Stream format.										Note: Not in data sheet!!
            //RX = 71 97 112 61 49 56 56 49 109 109 13 10
            //      G  a   p  =  1  8  8  1   m   m CR LF

            if (_serial.Device.BytesToRead == 12)
            {
                _data = new byte[_serial.Device.BytesToRead];
                _serial.Device.Read(_data, 0, _data.Length);

                distance = ((_data[4] - 48) * 1000) + ((_data[5] - 48) * 100) + ((_data[6] - 48) * 10) + ((_data[7] - 48));
                _sum = (uint)_data[0] + _data[1] + _data[2] + _data[3] + _data[8] + _data[9] + _data[10] + _data[11];
                _dataCheck = 582;

                // * For debuging data received *
                PR.ViewData(_data, _sum, _dataCheck);

                if (_dataCheck == _sum)
                {
                    return distance;
                }
                status = Status.DataCheckError;
                return -1;
            }
            status = Status.DataError;
            return -1;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (_serial.Device is { IsOpen: true })
            {
                _serial.Device.Close();
            }

            _serial.Device?.Dispose();
            _serial.Device = null!;
        }
    }
    class GetDistanceAUTO
    {
        private readonly _NewValuesCallback NewValues;
        private readonly ConfigSerial _serial = new();
        private readonly byte[] _pingByte = new byte[1];
        private readonly byte[] _data = new byte[4]; //To save incoming bytes
        uint dataCheck;
        private int distance;
        uint sum;

        // * To debug raw data from sensor *
        readonly PrintRaw PR = new();

        /// <summary>
        /// Constructor delegates automatic ping mode
        /// </summary>
        public GetDistanceAUTO(string port, SensorType sensorType, _NewValuesCallback NV)
        {
            _serial.Config(port);
            _pingByte[0] = (byte)sensorType;

            NewValues = NV;
        }

        public void ThreadProcess(object state)
        {
            _serial.Device.Write(_pingByte, 0, _pingByte.Length);
            Thread.Sleep(50);

            if (_serial.Device.BytesToRead == 4)
            {
                _serial.Device.Read(_data, 0, _data.Length);

                distance = (_data[1] << 8) | _data[2];
                sum = _data[3];
                dataCheck = (uint)(_data[0] + _data[1] + _data[2] + 1) & 0x00ff;

                PR.ViewData(_data, sum, dataCheck);

                if (dataCheck == sum)
                {
                    NewValues?.Invoke(distance, Status.Ok);
                    Debug.WriteLine("distanceByte = " + distance.ToString());
                }
                else
                {
                    distance = -1;
                    NewValues?.Invoke(distance, Status.DataCheckError);
                }
            }
        }
    }

    public class PrintRaw
    {
        public void ViewData(byte[] data, uint sum, uint dataCheck)
        {
            Debug.WriteLine("Datacheck = " + dataCheck.ToString() + " -- Sum = " + sum.ToString());
            Debug.WriteLine("RX = ");
            foreach (byte dt in data)
            {
                Debug.Write(dt + " ");
            }
            Debug.WriteLine("");
        }
    }
}
