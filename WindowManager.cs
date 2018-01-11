using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExplorer
{
    class WindowManager
    {
        static List<Window> windows;

        public List<Window> Windows
        {
            get
            {
                return windows;
            }

            set
            {
                windows = value;
            }
        }

        public List<Window> findWindows()
        {
            List<Window> windows = new List<Window>();

            Process[] processlist = Process.GetProcesses();
            int i = 0;

            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    try
                    {
                        
                        //Console.WriteLine("Process: {0} ID: {1} Window title: {2}", process.ProcessName, process.Id, process.MainWindowTitle);
                        
                        bool a = WinApi.IsWindowVisible(process.MainWindowHandle);
                        //Console.WriteLine(process.MainWindowTitle + " " + a);

                        Icon ico = Icon.ExtractAssociatedIcon(process.MainModule.FileName);
                        
                        Bitmap bit = ico.ToBitmap();
                        WindowIcon windowIcon = new WindowIcon(process.Id, 
                            new Point(WindowIcon.size * i + (i + 1) * WindowIcon.margin, WindowIcon.margin), bit);
                        windows.Add(new Window(process.Id, process, windowIcon));
                        i++;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
            Windows = windows;
            return windows;
        }

        public static Window getWindow(int id)
        {
            foreach(Window window in windows)
            {
                if (window.ID == id)
                {
                    return window;
                }
            }
            return null;
        }
    }
}
