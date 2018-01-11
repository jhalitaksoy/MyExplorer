using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyExplorer
{
    class ItemAnimator
    {
        Control control;
        Point start, stop;
        int step = 10, stepCounter = 0, directionX = 1, directionY = 1;
        Timer timer = new Timer();
        public void animatePosition(Control control, Point location, int Interval)
        {
            this.control = control;
            start = control.Location;
            stop = location;
            timer.Interval = Interval / step;
            timer.Tick += new EventHandler(tick);
            timer.Start();
            stepCounter = 0;

            if(start.X > stop.X)
            {
                directionX = -1;
            }
            if(start.Y > stop.Y)
            {
                directionY = -1;
            }
        }

       

        private void tick(object sender, EventArgs eventArgs)
        {
            if(stepCounter > 10)
            {
                timer.Stop();
                control.Location = stop;
                stepCounter = 0;
            }
            else
            {
                Point currentPos = control.Location;
                currentPos.X += Math.Abs( stop.X - start.X ) / step * directionX;
                currentPos.Y += Math.Abs(stop.Y - start.Y) / step * directionY;
                control.Location = currentPos;
                stepCounter++;
            }
        }
    }
}
