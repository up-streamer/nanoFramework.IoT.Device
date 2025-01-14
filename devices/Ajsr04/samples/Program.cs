using System;
using System.Diagnostics;
using System.Threading;
using Iot.Device.Ajsr04;
using Iot.Device.Ajsr04.Constants;
using UnitsNet;

namespace Sample_Ajsr04
{
    public class Program
    {
        private static Ajsr04 sensor;
        public static void Main()
        {
            // * To test receive data on demand, by pinging the device. *
            //sensor = new Ajsr04(SensorType.AJ_SR04M, Mode.Serial_LP_Bin);

            //for (int i = 0; i < 500; i++)
            //{
            //    Length distance = sensor.GetDistance();
            //    Debug.WriteLine($"distance = {distance.Millimeters} mm" + $"--> count = {i}");
            //    Thread.Sleep(1000);
            //}

            // * To test receive data when in auto mode, new data got from Timer thread.* 
            sensor = new Ajsr04(SensorType.AJ_SR04M, Mode.Serial_Auto);
            sensor.ReadInterval = 1000;

            for (int i = 0; i < 500; i++)
            {
                Debug.WriteLine($"distance = {sensor.Distance.Millimeters} mm " + $"Status. {sensor.status}" + $" --> count = {i}");
                Thread.Sleep(2000);
            }
        }
    }
}
