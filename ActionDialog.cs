using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyExplorer
{
    class ActionDialog : Form
    {
        public ActionDialog(Color themeColor)
        {
            InitializeForm(themeColor);
        }

        public void InitializeForm(Color themeColor)
        {
            Size = new System.Drawing.Size(100, 100);
            StartPosition = FormStartPosition.Manual;
            Point mousePoint = new Point();
            WinApi.GetCursorPos(ref mousePoint);
            Location = mousePoint;
            FormBorderStyle = FormBorderStyle.None;
            BackColor = themeColor;
        }
    }
}
