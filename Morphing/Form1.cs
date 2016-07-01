using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Morphing
{
    public partial class Form1 : Form
    {
        private const int PEN_WIDTH = 3;
        private const int RADIUS = 5;

        // Graphics g_srcCanvas;
        Point g_srcPnt, g_destPnt;
        //List<Line> g_Lines;
        List<Tuple<Line, Line>> g_Lines;
        bool g_creating, g_selected, g_srcRotating, g_destRotating;
        Line g_selLine, g_selLine2; // selected line
        //int g_selLineIndex; // Index of selected line
        int g_distX, g_distY;
        int g_numFrames;
        Bitmap[] g_frames;
        //Color[][,] g_toSrcPxls, g_toDestPxls;

        public Form1()
        {
            InitializeComponent();

            g_Lines = new List<Tuple<Line, Line>>();
            g_srcPnt = new Point(-1, -1);
            g_destPnt = new Point(-1, -1);
            g_numFrames = (int)frames_num.Value;
            //g_srcCanvas = source_pb.CreateGraphics();
        }

        // Suppress the WM_UPDATEUISTATE message
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x128) return;
            base.WndProc(ref m);
        }

        // Loads source image
        private void sourceImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image srcImg = Utility.LoadImage();
            if (srcImg != null)
            {
                source_pb.Image = srcImg;
                source_pb.BackColor = Color.Transparent;
            }
        }

        // Loads destination image
        private void destinationImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image srcImg = Utility.LoadImage();
            if (srcImg != null)
            {
                dest_pb.Image = srcImg;
            }
        }

        private void bothImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image srcImg = Utility.LoadImage();
            if (srcImg != null)
            {
                source_pb.Image = srcImg;
                source_pb.BackColor = Color.Transparent;
                dest_pb.Image = srcImg;
            }
        }

        /****************************************************************************************************/
        /****** Draws Line for Source Image *****************************************************************/
        /****************************************************************************************************/

        // Assign a reference to the starting point to create a line
        private void source_pb_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                deselectAll();

                // Checking if any mid point of the line was clicked
                foreach (Tuple<Line, Line> l in g_Lines)
                {
                    Point mid = l.Item1.getMid();
                    g_selected = false;

                    if (Math.Sqrt((e.X - mid.X) * (e.X - mid.X) + (e.Y - mid.Y) * (e.Y - mid.Y)) < RADIUS)
                    {
                        g_selected = true;
                        g_selLine = l.Item1;
                        g_selLine.setSelected(true);
                        g_selLine2 = l.Item2;
                        g_selLine2.setSelected(true);

                        Point src = g_selLine.getSrc();
                        // Calculate the distance between the midpoint of line to source point. 
                        g_distX = Math.Abs(mid.X - src.X);
                        g_distY = Math.Abs(mid.Y - src.Y);

                        source_pb.Invalidate();
                        dest_pb.Invalidate();
                        break;
                    }
                }

                if (g_selLine != null)
                {
                    Point src = g_selLine.getSrc();
                    Point dest = g_selLine.getDest();
                    rotationSelected(src, dest, e);
                }


                if (!g_selected && !g_srcRotating && !g_destRotating)
                {
                    g_creating = true;
                    setPoint(ref g_srcPnt, e);
                }


            }
        }
        // Rubber-banding or dragging
        private void source_pb_MouseMove(object sender, MouseEventArgs e)
        {

            // Displays the rubber banding effect of the current line while holding and moving the mouse, but does not create a line.
            if (g_creating)
            {
                setPoint(ref g_destPnt, e);
                source_pb.Invalidate();
                dest_pb.Invalidate();
            }
            // Drags a line from mid-line
            if (g_selected)
            {
                dragLine(ref g_selLine, e);
                source_pb.Invalidate();
                dest_pb.Invalidate();
            }

            if (g_srcRotating)
            {
                g_destPnt = g_selLine.getDest();
                setPoint(ref g_srcPnt, e);
                g_selLine.setSrc(new Point(e.X, e.Y));
                g_selLine.setMid(g_selLine.calcMid());
                source_pb.Invalidate();
            }
            if (g_destRotating)
            {
                g_srcPnt = g_selLine.getSrc();
                setPoint(ref g_destPnt, e);
                g_selLine.setDest(new Point(e.X, e.Y));
                g_selLine.setMid(g_selLine.calcMid());
                source_pb.Invalidate();
            }


        }
        // Gets the ending point of the line, creates it, and store it in the list
        private void source_pb_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && g_creating)
            {
                setPoint(ref g_destPnt, e);
                g_Lines.Add(new Tuple<Line, Line>(new Line(g_srcPnt, g_destPnt), new Line(g_srcPnt, g_destPnt)));
                g_selLine = g_Lines[g_Lines.Count - 1].Item1;
                g_selLine2 = g_Lines[g_Lines.Count - 1].Item2;
                g_creating = false;

                source_pb.Invalidate();
                dest_pb.Invalidate();
            } else if (e.Button == MouseButtons.Left && g_selected)
            {
                g_selected = false;
            } else if (e.Button == MouseButtons.Left && g_srcRotating)
            {
                g_selLine.setSelected(true);
                g_srcRotating = false;
                source_pb.Invalidate();
            } else if (e.Button == MouseButtons.Left && g_destRotating)
            {
                g_selLine.setSelected(true);
                g_destRotating = false;
                source_pb.Invalidate();
            }
        }
        // Draws the current line and the stored lines in the list
        private void source_pb_Paint(object sender, PaintEventArgs e)
        {
            drawLines(e, true);
            if (g_creating || g_srcRotating || g_destRotating)
            {
                drawRubberLine(e);
            }


        }


        /****************************************************************************************************/
        /****** Drawing Line for Destination Image **********************************************************/
        /****************************************************************************************************/

        // Assign a reference to the starting point to create a line
        private void dest_pb_MouseDown(object sender, MouseEventArgs e)
        {
            // Checking if any mid point of the line was clicked
            if (e.Button == MouseButtons.Left && g_selLine2 != null)
            {
                Point src = g_selLine2.getSrc();
                Point dest = g_selLine2.getDest();
                Point mid = g_selLine2.getMid();
                if (Math.Sqrt((e.X - mid.X) * (e.X - mid.X) + (e.Y - mid.Y) * (e.Y - mid.Y)) < RADIUS)
                {
                    // Calculate the distance between the midpoint of line to source point. 
                    g_distX = Math.Abs(mid.X - src.X);
                    g_distY = Math.Abs(mid.Y - src.Y);
                    g_selected = true;
                }

                rotationSelected(src, dest, e);
            }



        }
        // Rubber-banding or dragging
        private void dest_pb_MouseMove(object sender, MouseEventArgs e)
        {
            // Drags a line from mid-line
            if (g_selected)
            {
                dragLine(ref g_selLine2, e);
                source_pb.Invalidate();
                dest_pb.Invalidate();
            }
            if (g_srcRotating)
            {
                g_destPnt = g_selLine2.getDest();
                setPoint(ref g_srcPnt, e);
                g_selLine2.setSrc(new Point(e.X, e.Y));
                g_selLine2.setMid(g_selLine2.calcMid());
                dest_pb.Invalidate();
            }
            if (g_destRotating)
            {
                g_srcPnt = g_selLine2.getSrc();
                setPoint(ref g_destPnt, e);
                g_selLine2.setDest(new Point(e.X, e.Y));
                g_selLine2.setMid(g_selLine2.calcMid());
                dest_pb.Invalidate();
            }
        }
        // Disables dragging
        private void dest_pb_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && g_selected)
            {
                g_selected = false;
            } else if (e.Button == MouseButtons.Left && g_srcRotating)
            {
                g_selLine2.setSelected(true);
                g_srcRotating = false;
                dest_pb.Invalidate();
            }
            else if (e.Button == MouseButtons.Left && g_destRotating)
            {
                g_selLine2.setSelected(true);
                g_destRotating = false;
                dest_pb.Invalidate();
            }
        }
        // Draws the current line and the stored lines in the list
        private void dest_pb_Paint(object sender, PaintEventArgs e)
        {
            drawLines(e, false);
            if (g_creating || g_srcRotating || g_destRotating)
            {
                drawRubberLine(e);
            }

        }


        /****************************************************************************************************/
        /****************************************************************************************************/
        /****************************************************************************************************/

        private void frames_num_ValueChanged(object sender, EventArgs e)
        {
            g_numFrames = (int)frames_num.Value;
        }

        private void morph_btn_Click(object sender, EventArgs e)
        {
            
            if (g_numFrames > 0) {
                setFramesTrackbar();
            } else
            {
                return;
            }

            g_frames = new Bitmap[g_numFrames];
            Color[][,] toDestPxls = new Color[g_numFrames+2][,]; // +2 for the source image data and dest img which is not used
            Color[][,] toSrcPxls = new Color[g_numFrames + 2][,];
            List<Line>[] frameLines = calcInterLine(); // size = g_numFrames + 2;
            toDestPxls[0] = Utility.convertBitmapToData(new Bitmap(source_pb.Image));
            toSrcPxls[toSrcPxls.Length-1] = Utility.convertBitmapToData(new Bitmap(dest_pb.Image));
            //toSrcPxls[0] = Utility.convertBitmapToData(new Bitmap(dest_pb.Image));

            // Creating inter frames
            for (int i = 0, j = g_numFrames-1; i < g_numFrames; i++, j--)
            {
                // Warp from src to dest
                Color[,] toDestData = warp(frameLines, toDestPxls[i], i +1, true);
                //Color[,] toDestData = warp(frameLines, toDestPxls[0], i + 1, true);
                toDestPxls[i+1] = toDestData;

                // Warp from dest to src
                //Color[,] toSrcData = warp(frameLines, toSrcPxls[j+2], j +1, false);
                Color[,] toSrcData = warp(frameLines, toSrcPxls[j+2], j + 1, false);
                toSrcPxls[j+1] = toSrcData;


                //g_frames[i] = Utility.convertDataToBitmap(toDestData);
                g_frames[j] = Utility.convertDataToBitmap(toSrcData);

            }
            /*
            for (int i=0; i < g_numFrames; i++)
            {
                g_frames[i] = new Bitmap(source_pb.Width, source_pb.Height);
                int frames = g_numFrames + 1; // +1 for the initial source/destination frame
                double toDestFrac = ((double)(g_numFrames - i) / frames);
                crossDissolve(g_frames[i], toDestPxls[i+1], toSrcPxls[i+1], toDestFrac);
                //crossDissolve(g_frames[i], toDestPxls[0], toSrcPxls[0], toDestFrac);
            }*/
            
            morphed_pb.Image = g_frames[0];
        }

        private void frames_trackbar_ValueChanged(object sender, EventArgs e)
        {
            morphed_pb.Image = g_frames[frames_trackbar.Value];
        }

        private Color[,] warp(List<Line>[] lines, Color[,] prevData, int lineIndex, bool isToSrc)
        {
            Color[,] result = new Color[morphed_pb.Width, morphed_pb.Height];
            double totalWeight, twd_x, twd_y;
            List<Line> curFrameLines = null;
            List<Line> prevFrameLines = null;
            //Color[,] prevData = null;

            if (isToSrc)
            {
                curFrameLines = lines[lineIndex];
                prevFrameLines = lines[lineIndex-1];
                //prevData = g_toSrcPxls[lineIndex - 1];
            } else
            {
                curFrameLines = lines[lineIndex];
                prevFrameLines = lines[lineIndex + 1];
                //prevData = g_toDestPxls[lineIndex + 1];
            }

            for (int y = 0; y < morphed_pb.Height; y++)
            {
                for (int x = 0; x < morphed_pb.Width; x++)
                {
                    Point pX = new Point(x, y);
                    totalWeight = twd_x = twd_y = 0.0;

                    for (int i = 0; i < prevFrameLines.Count; i++)
                    {
                        Xprime pX_ = new Xprime(curFrameLines[i], prevFrameLines[i], pX);
                        totalWeight += pX_.getWeight();
                        Tuple<double, double> twd = pX_.getWeightedDelta();
                        twd_x += twd.Item1;
                        twd_y += twd.Item2;
                    }

                    Point pAvgWD = new Point(
                        (int)Math.Round(twd_x / totalWeight),
                        (int)Math.Round(twd_y / totalWeight)
                        );

                    Point newX_ = new Point(pX.X + pAvgWD.X, pX.Y + pAvgWD.Y);

                    if (newX_.X < morphed_pb.Width && newX_.X >=0 && newX_.Y < morphed_pb.Height && newX_.Y >= 0)
                    {
                        result[x, y] = prevData[newX_.X, newX_.Y];
                    }
                    else
                    {
                        //result[x, y] = prevData[x, y];
                        //Console.WriteLine(newX_);
                    }

                }// for x
            }// for y
            return result;
        }

        private void crossDissolve(Bitmap bm, Color[,] toDestData, Color[,] toSrcData, double toDestFrac)
        {

            for(int y=0; y < bm.Height; y++)
            {
                for(int x=0; x < bm.Width; x++)
                {      
                    double toSrcFrac = 1.0 - toDestFrac;
                    int a = (int)Math.Round((toDestData[x, y].A * toDestFrac) + (toSrcData[x, y].A * toSrcFrac));
                    int r = (int)Math.Round((toDestData[x, y].R * toDestFrac) + (toSrcData[x, y].R * toSrcFrac));
                    int g = (int)Math.Round((toDestData[x, y].G * toDestFrac) + (toSrcData[x, y].G * toSrcFrac));
                    int b = (int)Math.Round((toDestData[x, y].B * toDestFrac) + (toSrcData[x, y].B * toSrcFrac));

                    Color px = Color.FromArgb(a, r, g, b);
                    bm.SetPixel(x, y, px);
                }
            } 
        }

        private void button1_Click(object sender, EventArgs e)
        {

            /*
            string[,] arr = new string[5, 10];
            for(int y=0; y < 10; y++)
            {
                for(int x=0; x < 5; x++)
                {
                    arr[x, y] = x + " " + y;
                }
            }

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    Console.WriteLine(arr[x, y]);
                }
            }

            */

            //setFramesTrackbar();
            //testLines();
            //testBitmap();
            //testX();
            addBorderLines();

        }

        public void DrawLineInt(Bitmap bmp, Point src, Point dest)
        {
            Pen blackPen = new Pen(Color.Black, 3);

            int x1 = src.X;
            int y1 = src.Y;
            int x2 = dest.X;
            int y2 = dest.Y;
            // Draw line to screen.
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.DrawLine(blackPen, x1, y1, x2, y2);
            }
        }

        private void addBorderLines()
        {
            Point src = new Point(1, 1);
            Point dest = new Point(source_pb.Width-1, 1);
            
            g_Lines.Add(new Tuple<Line, Line>(new Line(src, dest), new Line(src, dest)));
            src = dest;
            dest = new Point(source_pb.Width-1, source_pb.Height-1);
            g_Lines.Add(new Tuple<Line, Line>(new Line(src, dest), new Line(src, dest)));
            src = dest;
            dest = new Point(1, source_pb.Height-1);
            g_Lines.Add(new Tuple<Line, Line>(new Line(src, dest), new Line(src, dest)));
            src = dest;
            dest = new Point(1, 1);
            g_Lines.Add(new Tuple<Line, Line>(new Line(src, dest), new Line(src, dest)));
            source_pb.Invalidate();
            dest_pb.Invalidate();
        }

        public void testLines()
        {
            List<Line>[] frameLines = calcInterLine(); // size = g_numFrames + 2;

            g_frames = new Bitmap[g_numFrames+2];
            
           for (int i = 0; i < g_frames.Length; i++)
           {
               g_frames[i] = new Bitmap(morphed_pb.Width, morphed_pb.Height);
               foreach (Line l in frameLines[i])
               {
                   DrawLineInt(g_frames[i], l.getSrc(), l.getDest());
               }
           }
           
        }

        public void testBitmap()
        {
            Color[,] srcImgData = Utility.convertBitmapToData(new Bitmap(source_pb.Image));
            Bitmap srcImgBm = Utility.convertDataToBitmap(srcImgData);
            morphed_pb.Image = srcImgBm;
        }

        public void testX()
        {
            Xprime x = new Xprime();
            x.test();
        }

        /****************************************************************************************************/
        /****************************************************************************************************/
        /****************************************************************************************************/

        private void resetLines_btn_Click(object sender, EventArgs e)
        {
            g_Lines = new List<Tuple<Line, Line>>();
            source_pb.Invalidate();
            dest_pb.Invalidate();
        }

        private void drawLines(PaintEventArgs e, bool isSource)
        {
            Color color;

            foreach (Tuple<Line, Line> l in g_Lines)
            {
                int index = g_Lines.IndexOf(l);
                color = (g_selLine.getSelected() && (g_selLine == l.Item1 || g_selLine == l.Item2)) ? Color.LightGreen : Color.Blue;
                Point src = (isSource) ? l.Item1.getSrc() : l.Item2.getSrc();
                Point dest = (isSource) ? l.Item1.getDest() : l.Item2.getDest();
                Point mid = (isSource) ? l.Item1.getMid() : l.Item2.getMid();
                e.Graphics.DrawLine(new Pen(color, PEN_WIDTH), src, dest);
                e.Graphics.FillEllipse(new SolidBrush(Color.Yellow), new Rectangle(src.X - RADIUS, src.Y - RADIUS, RADIUS * 2, RADIUS * 2));
                e.Graphics.FillEllipse(new SolidBrush(Color.Red), new Rectangle(dest.X - RADIUS, dest.Y - RADIUS, RADIUS * 2, RADIUS * 2));
                e.Graphics.FillEllipse(new SolidBrush(Color.Orange), new Rectangle(mid.X - RADIUS, mid.Y - RADIUS, RADIUS * 2, RADIUS * 2));
            }
        }

        private void drawRubberLine(PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.LightGreen, PEN_WIDTH), g_srcPnt, g_destPnt);
            e.Graphics.FillEllipse(new SolidBrush(Color.Yellow), new Rectangle(g_srcPnt.X - RADIUS, g_srcPnt.Y - RADIUS, RADIUS * 2, RADIUS * 2));
            e.Graphics.FillEllipse(new SolidBrush(Color.Red), new Rectangle(g_destPnt.X - RADIUS, g_destPnt.Y - RADIUS, RADIUS * 2, RADIUS * 2));
        }

        private void dragLine(ref Line line, MouseEventArgs e)
        {
            Point newDest = new Point();
            Point newSrc = new Point();
            Point curSrc = line.getSrc();
            Point curDest = line.getDest();

            newSrc.X = (curSrc.X < curDest.X) ? (e.X - g_distX) : (e.X + g_distX);
            newDest.X = (curSrc.X > curDest.X) ? (e.X - g_distX) : (e.X + g_distX);
            newSrc.Y = (curSrc.Y < curDest.Y) ? (e.Y - g_distY) : (e.Y + g_distY);
            newDest.Y = (curSrc.Y > curDest.Y) ? (e.Y - g_distY) : (e.Y + g_distY);

            g_srcPnt = newSrc;
            g_destPnt = newDest;

            line.setDest(newDest);
            line.setMid(new Point(e.X, e.Y));
            line.setSrc(newSrc);
        }

        private void moveLinePoint(MouseEventArgs e)
        {
            Point src = g_selLine.getSrc();
            Point dest = g_selLine.getDest();

            if (Math.Sqrt((e.X - src.X) * (e.X - src.X) + (e.Y - src.Y) * (e.Y - src.Y)) < RADIUS)
            {
                g_selLine.setSrc(new Point(e.X, e.Y));
                source_pb.Invalidate();
            }
            if (Math.Sqrt((e.X - dest.X) * (e.X - dest.X) + (e.Y - dest.Y) * (e.Y - dest.Y)) < RADIUS)
            {
                g_selLine.setDest(new Point(e.X, e.Y));
                source_pb.Invalidate();
            }
        }

        private void deselectAll()
        {
            foreach (Tuple<Line, Line> l in g_Lines)
            {
                l.Item1.setSelected(false);
                l.Item2.setSelected(false);
            }
        }

        private void rotationSelected(Point src, Point dest, MouseEventArgs e)
        {
            if (Math.Sqrt((e.X - src.X) * (e.X - src.X) + (e.Y - src.Y) * (e.Y - src.Y)) < RADIUS)
            {
                g_srcRotating = true;
            }
            if (Math.Sqrt((e.X - dest.X) * (e.X - dest.X) + (e.Y - dest.Y) * (e.Y - dest.Y)) < RADIUS)
            {
                g_destRotating = true;
            }
        }

        private void setPoint(ref Point p, MouseEventArgs e)
        {
            p.X = e.X;
            p.Y = e.Y;
        }

        public void setFramesTrackbar()
        {
            //int size = this.imgList.Count - 1;
            frames_trackbar.Maximum = (int)frames_num.Value-1;
            frames_trackbar.Value = 0;
        }

        // Calculates the new position of the lines from source to destination.
        private List<Line>[] calcInterLine()
        {
            int resSize = g_numFrames + 2;
            List<Line>[] result = new List<Line>[resSize];
            int numFrames = resSize - 1;

            for(int i=0; i < result.Length; i++)
            {
                result[i] = new List<Line>();
            }

            foreach(Tuple<Line,Line> pair in g_Lines)
            {
                Point src_srcPnt = pair.Item1.getSrc();
                Point dest_srcPnt = pair.Item2.getSrc();

                float srcPnt_xRatio = (dest_srcPnt.X - src_srcPnt.X) / numFrames;
                float srcPnt_yRatio = (dest_srcPnt.Y - src_srcPnt.Y) / numFrames;

                Point src_destPnt = pair.Item1.getDest();
                Point dest_destPnt = pair.Item2.getDest();

                float destPnt_xRatio = (dest_destPnt.X - src_destPnt.X) / numFrames;
                float destPnt_yRatio = (dest_destPnt.Y - src_destPnt.Y) / numFrames;

                float srcPnt_newX, srcPnt_newY, destPnt_newX, destPnt_newY; 

                for(int i=0; i < resSize; i++)
                {
                    srcPnt_newX = src_srcPnt.X + (i * srcPnt_xRatio);
                    srcPnt_newY = src_srcPnt.Y + (i * srcPnt_yRatio);
                    destPnt_newX = src_destPnt.X + (i * destPnt_xRatio);
                    destPnt_newY = src_destPnt.Y + (i * destPnt_yRatio);

                    result[i].Add(new Line(
                        new Point((int)srcPnt_newX, (int)srcPnt_newY),
                        new Point((int)destPnt_newX, (int)destPnt_newY)
                        ));
                }
            }
            return result;
        }

   

        


    } // End of Class
}
