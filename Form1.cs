using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Management;
using MyExplorer;

namespace MyExplorer
{
    public partial class Form1 : Form
    {

        List<Window> windows = new List<Window>();

        WindowManager windowManager = new WindowManager();
        
        public static Color backColor = Color.Lime;
        Timer timer1;

        public static bool isMouseOnIcon = false;
        public Form1()
        {
            InitializeComponent();
            registerKey();

            

            timer1 = new Timer();
            timer1.Interval = 2000;
            timer1.Tick += new EventHandler(timer_Tick);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            WindowIcon.margin = 15;
            onActivated();
            BackColor = backColor;

            this.MouseEnter += new EventHandler(form_MouseEnter);
            this.MouseLeave += new EventHandler(form_MouseExit);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (Visible)
            {
                hide();
                
            }
        }

        public void onActivated()
        {
            if (Visible)
            {
                hide();
            }
            else
            {
                show();
            }
        }

        public void hide()
        {
            ActionDialogManager.hideAll();
            this.Hide();
            timer1.Stop();
        }

        public void show()
        {
            list();
            this.Show();
            //timer1.Start();

            Point p = new Point();
            WinApi.GetCursorPos(ref p);
            p.Y -= Size.Height / 2;
            p.X -= Size.Width / 2;
            this.Location = p;

            WinApi.SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle.ToInt32());
        }
        
        private void list()
        {
            windows = new List<Window>();
            this.Controls.Clear();

            int i = 0;
            windows = windowManager.findWindows();

            foreach(Window window in windows)
            {
                this.Controls.Add(window.WindowIcon);
                i++;
            }

            Size = new Size(WindowIcon.size * i + (i+1) * WindowIcon.margin,  WindowIcon.size + WindowIcon.margin * 2);
        }

        private void form_MouseEnter(object sender, System.EventArgs e)
        {
            timer1.Stop();
        }

        private void form_MouseExit(object sender, System.EventArgs e)
        {
            Console.WriteLine("exit");
            Point pos = new Point();
            WinApi.GetCursorPos(ref pos);
            Rectangle rec = new Rectangle(this.Location,
                new Size(this.Location.X + this.Size.Width, this.Location.Y + this.Size.Height));
            if (rec.Contains(pos))
            {
                timer1.Start();
                Console.WriteLine("timer start");
            }
            
        }
        

        /********************************************/
        //global hot key
        //http://frasergreenroyd.com/c-global-keyboard-listeners-implementation-of-key-hooks/
        public void registerKey()
        {
            WinApi.RegisterHotKey(this.Handle, WinApi.mActionHotKeyID, 1, (int)Keys.Z);
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == WinApi.mActionHotKeyID)
            {
                onActivated();
            }
            base.WndProc(ref m);
        }

        
    }
}
