using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExplorer
{

    class Window
    {
        Process process;

        int id;

        public int ID{
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public Process WindowProcess
        {
            get { return process; }
            set { process = value; }
        }
        WindowIcon windowIcon;

        public WindowIcon WindowIcon
        {
            get { return windowIcon; }
            set { windowIcon = value; }
            
        } 
        
           
        public Window(int id, Process process, WindowIcon windowIcon)
        {
            this.process = process;
            this.windowIcon = windowIcon;
            this.id = id;
        }

        public void show()
        {
            int hWnd = process.MainWindowHandle.ToInt32();
            var placement = WinApi.GetPlacement(process.MainWindowHandle);

            string state = placement.showCmd.ToString();
            if (state.ToLower().CompareTo("minimized") == 0)
            {
                WinApi.ShowWindow(hWnd, 3);
            }
            WinApi.SetForegroundWindow(hWnd);
        }
    }
}
