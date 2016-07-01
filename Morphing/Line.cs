using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphing
{
    class Line
    {
        private Point src, dest, mid;
        private bool selected;

        public Line(Point src, Point dest)
        {
            this.src = src;
            this.dest = dest;
            mid = calcMid();
            selected = true;
        }

        public string ToString()
        {
            return "src = " + src.ToString() + ", dest = " + dest.ToString();
        }

        public Point calcMid()
        {
            int x = (dest.X + src.X) / 2;
            int y = (dest.Y + src.Y) / 2;
            return new Point(x, y);
        }

        public void setSrc(Point src)
        {
            this.src = src;
        }

        public void setDest(Point dest)
        {
            this.dest = dest;
        }

        public void setMid(Point mid)
        {
            this.mid = mid;
        }

        public void setSelected(bool selected)
        {
            this.selected = selected;
        }




        public Point getSrc()
        {
            return src;
        }

        public Point getDest()
        {
            return dest;
        }

        public Point getMid()
        {
            return mid;
        }

        public bool getSelected()
        {
            return selected;
        }
    }
}
