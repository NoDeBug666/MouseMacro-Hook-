using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace 滑鼠巨集
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int add = 0;
        bool IsWorking = false;
        bool ShutDown = false;
        Thread Loop;
        private void StartClickLoop()
        {
            if (IsWorking)
                return;
            Loop = new Thread(ClickLoop);
            Loop.Name = "點擊Loop";
            IsWorking = true;
            ShutDown = false;
            Loop.Start();
        }
        private void Down()
        {
            ShutDown = true;
        }
        private void ClickLoop()
        {
            int times = 0;
            Thread.Sleep(5000);
            while (!ShutDown)
            {
                while (!ShutDown && times < 50)
                {
                    times++;
                    OneClick();
                }
                Thread.Sleep(3000);
                times = 0;
            }
            IsWorking = false;
        }
        private void OneClick()
        {
            Mouse.LeftDown();
            Thread.Sleep(20);
            Mouse.LeftUp();
            Thread.Sleep(100);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartClickLoop();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Down();
        }
        private void label1_Click(object sender, EventArgs e)
        {
            add++;
            label1.Text = add.ToString();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Loop != null)
                Loop.Abort();
        }
        private void button3_Click(object sender, EventArgs e){
        
            this.Close();
        }
    }

static public class Mouse
{
    [DllImport("user32.dll", SetLastError = true)]
    public static extern Int32 SendInput(Int32 cInputs, ref INPUT pInputs, Int32 cbSize);

    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 28)]
    public struct INPUT
    {
        [FieldOffset(0)]
        public INPUTTYPE dwType;    
        [FieldOffset(4)]
        public MOUSEINPUT mi;
        [FieldOffset(4)]
        public KEYBOARDINPUT ki;
        [FieldOffset(4)]
        public HARDWAREINPUT hi;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MOUSEINPUT
    {
        public Int32 dx;
        public Int32 dy;
        public Int32 mouseData;
        public MOUSEFLAG dwFlags;
        public Int32 time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct KEYBOARDINPUT
    {
        public Int16 wVk;
        public Int16 wScan;
        public KEYBOARDFLAG dwFlags;
        public Int32 time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HARDWAREINPUT
    {
        public Int32 uMsg;
        public Int16 wParamL;
        public Int16 wParamH;
    }

    public enum INPUTTYPE : int
    {
        Mouse = 0,
        Keyboard = 1,
        Hardware = 2
    }

    [Flags()]
    public enum MOUSEFLAG : int
    {
        MOVE = 0x1,
        LEFTDOWN = 0x2,
        LEFTUP = 0x4,
        RIGHTDOWN = 0x8,
        RIGHTUP = 0x10,
        MIDDLEDOWN = 0x20,
        MIDDLEUP = 0x40,
        XDOWN = 0x80,
        XUP = 0x100,
        VIRTUALDESK = 0x400,
        WHEEL = 0x800,
        ABSOLUTE = 0x8000
    }

    [Flags()]
    public enum KEYBOARDFLAG : int
    {
        EXTENDEDKEY = 1,
        KEYUP = 2,
        UNICODE = 4,
        SCANCODE = 8
    }

    static public void LeftDown()
    {
        INPUT leftdown = new INPUT();

        leftdown.dwType = 0;
        leftdown.mi = new MOUSEINPUT();
        leftdown.mi.dwExtraInfo = IntPtr.Zero;
        leftdown.mi.dx = 0;
        leftdown.mi.dy = 0;
        leftdown.mi.time = 0;
        leftdown.mi.mouseData = 0;
        leftdown.mi.dwFlags = MOUSEFLAG.LEFTDOWN;

        SendInput(1, ref leftdown, Marshal.SizeOf(typeof(INPUT)));
    }
    static public void LeftUp()
    {
        INPUT leftup = new INPUT();

        leftup.dwType = 0;
        leftup.mi = new MOUSEINPUT();
        leftup.mi.dwExtraInfo = IntPtr.Zero;
        leftup.mi.dx = 0;
        leftup.mi.dy = 0;
        leftup.mi.time = 0;
        leftup.mi.mouseData = 0;
        leftup.mi.dwFlags = MOUSEFLAG.LEFTUP;

        SendInput(1, ref leftup, Marshal.SizeOf(typeof(INPUT)));
    }

}

}
