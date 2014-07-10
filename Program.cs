using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace JoyStick
{
    public partial class Program
    {
        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            // Use Debug.Print to show messages in Visual Studio's "Output" window during debugging.
            Debug.Print("Program Started");
            
            // キャリブレーション
            PrintPosition("before calibrate");
            joystick.Calibrate();       // calibrate (current position will be 0)
            PrintPosition("after calibrate");

            // イベント登録
            joystick.JoystickPressed += joystick_JoystickPressed;
            joystick.JoystickReleased += joystick_JoystickReleased;

            // タイマーイベント登録
            GT.Timer timer = new GT.Timer(500);
            timer.Tick += timer_Tick;
            timer.Start();  
        }

        // 位置情報の表示
        //------------------------------------------------
        void PrintPosition(string info) 
        {
            Joystick.Position pos = new Joystick.Position();
            pos = joystick.GetPosition();
            Debug.Print("Position[" + info + "]" + "x:" + pos.X.ToString() + ", " + "y:" + pos.Y.ToString());
        }

        // Timerイベント処理：周期的に位置情報を出力
        //------------------------------------------------
        void timer_Tick(GT.Timer timer)
        {
            PrintPosition("timer");
        }

        // プレスイベント処理：現在位置情報を出力
        //------------------------------------------------
        void joystick_JoystickPressed(Joystick sender, Joystick.JoystickState state)
        {
            PrintPosition("Pressed");
        }

        // リリースイベント処理：現在位置情報を出力
        //------------------------------------------------
        void joystick_JoystickReleased(Joystick sender, Joystick.JoystickState state)
        {
            PrintPosition("Released");
        }

    }
}
