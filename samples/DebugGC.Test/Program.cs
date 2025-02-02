﻿//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Runtime.Native;
using Windows.Devices.Gpio;
using System;
using System.Threading;

namespace DebugGC.Test
{
    public class Program
    {
        public static void Main()
        {
            Debug.EnableGCMessages(true);

            Debug.GC(false);

            // mind to set a pin that exists on the board being tested
            // PJ5 is LD2 in STM32F769I_DISCO
            //GpioPin led = GpioController.GetDefault().OpenPin(PinNumber('J', 5));
            // PD15 is LED6 in DISCOVERY4
            //GpioPin led = GpioController.GetDefault().OpenPin(PinNumber('D', 15));
            // PE15 is LED1 in QUAIL
            //GpioPin led = GpioController.GetDefault().OpenPin(PinNumber('E', 15));
            // PG13 is LD3 in F429I-DISCO
            GpioPin led = GpioController.GetDefault().OpenPin(PinNumber('G', 14));
            //GpioPin led = GpioController.GetDefault().OpenPin(4);

            led.SetDriveMode(GpioPinDriveMode.Output);

            int i = 0;

            for(; ; )
            {
                i++;

                int[] array = new int[8192];

                led.Toggle();
                Thread.Sleep(100);
                led.Toggle();
                Thread.Sleep(400);

                Console.WriteLine(">> " + i.ToString() + " free memory: " + Debug.GC(false) + " bytes");

                Thread.Sleep(1000);
            }
        }

        static int PinNumber(char port, byte pin)
        {
            if (port < 'A' || port > 'J')
                throw new ArgumentException();

            return ((port - 'A') * 16) + pin;
        }
    }
}
