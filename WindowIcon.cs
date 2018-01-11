using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyExplorer
{
    class WindowIcon : PictureBox
    {
        public int id;
        public Point location;
        public static int size;
        public Bitmap icon;
        public static int margin;
        public WindowIcon(int id, Point location, Bitmap icon)
        {
            this.id = id;
            this.location = location;
            this.icon = icon;
            size = icon.Size.Height;
            this.BackColor = Color.Transparent;
            Initial();
        }

        public void Initial()
        {
            Image = icon;
            Location = location;
            Size = new Size(size, size);

            Cursor = Cursors.Hand;
            BackColor = Form1.backColor;

            MouseEnter += new System.EventHandler(pictureBox_MouseEnter);
            MouseLeave += new System.EventHandler(pictureBox_MouseExit);
            MouseDown += new System.Windows.Forms.MouseEventHandler(pictureBox_MouseDown);
        }


        private void pictureBox_MouseEnter(object sender, System.EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;

            ItemAnimator itemAnimator = new ItemAnimator();
            itemAnimator.animatePosition(pictureBox, new Point(pictureBox.Location.X, pictureBox.Location.Y - margin / 2), 500);
            //Console.WriteLine(WindowManager.getWindow(id).WindowProcess.MainWindowTitle);
            //pictureBox.Location = new Point(pictureBox.Location.X, pictureBox.Location.Y - margin / 2);
            pictureBox.Height += pictureBox.Location.Y + margin / 2;
        }

        private void pictureBox_MouseExit(object sender, System.EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            pictureBox.Location = new Point(pictureBox.Location.X, pictureBox.Location.Y + margin / 2);
            pictureBox.Height -= pictureBox.Location.Y - margin / 2;
        }

        private void pictureBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            Window window = WindowManager.getWindow(id);

            if (e.Button.CompareTo(MouseButtons.Left) == 0)
            {
                Parent.Hide();
                if (window != null)
                {
                    window.show();
                }
            }
            else if (e.Button.CompareTo(MouseButtons.Right) == 0)
            {
                ActionDialog actionDialog = new ActionDialog(Form1.backColor);
                ActionDialogManager.showDialog(actionDialog);
            }
        }

    }
}
