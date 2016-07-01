using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphing
{
    class Xprime
    {
        private const int LENGTH = 1;
        private const double A = 0.01;
        private const int P = 0;
        private const int B = 2;

        //private Point m_pX_;
        private double m_weight;
        private Tuple<double, double> m_weightedDelta;

        public Xprime(Line line, Line line_, Point pX)
        {
            //m_pX_ = new Point(); 
            m_weight = 0;
            m_weightedDelta = new Tuple<double,double>(0.0, 0.0);
            main(line, line_, pX);
        }

        public Point vector(Point t, Point h)
        {
            return new Point(h.X - t.X, h.Y - t.Y);
        }

        public double proj(Point sub, Point main)
        {
            return dotProduct(main, sub) / magnitude(sub);
        }

        public Point normalize(Point vec)
        {
            return new Point(vec.Y * -1, vec.X);
        }

        public int dotProduct(Point main, Point sub)
        {
            return (main.X * sub.X) + (main.Y * sub.Y);
        }

        public double magnitude(Point vec)
        {
            return Math.Sqrt((vec.X * vec.X) + (vec.Y * vec.Y));
        }

        public double fracPercent(double frac, Point vec)
        {
            return (frac / magnitude(vec));
        }

        public Point calcX_(Point pP_, double fracPC, Point vPQ_, double d, Point n)
        {
            Tuple<double,double> n_hat = hat(n);
            double x = pP_.X + (fracPC * vPQ_.X) - (d * n_hat.Item1);
            double y = pP_.Y + (fracPC * vPQ_.Y) - (d * n_hat.Item2);
            return new Point((int)Math.Round(x), (int)Math.Round(y));
        }

        public Tuple<double,double> hat(Point vec)
        {
            double x = vec.X / magnitude(vec);
            double y = vec.Y / magnitude(vec);
            return new Tuple<double, double>(x, y);
        }

        public Point delta(Point pX, Point pX_)
        {
            return new Point(pX_.X - pX.X, pX_.Y - pX.Y);
        }

        public double weight(double d)
        {
            return Math.Pow(Math.Pow(LENGTH, P) / (A + Math.Abs(d)), B);
        }

        public Tuple<double,double> weightedDelta(Point delta, double weight)
        {
            return new Tuple<double,double>(delta.X * weight, delta.Y * weight);
        }

        public void main(Line line, Line line_, Point pX)
        {
            Point pP = line.getSrc();
            Point pQ = line.getDest();

            Point vPQ = vector(pP, pQ);
            Point vN = normalize(vPQ);
            Point vXP = vector(pX, pP);
            Point vPX = vector(pP, pX);
            Point vQX = vector(pQ, pX);

            double d = proj(vN, vXP);
            double fraction = proj(vPQ, vPX);
            double fracPC = fracPercent(fraction, vPQ);
            Point pP_ = line_.getSrc();
            Point pQ_ = line_.getDest(); 
            Point vPQ_ = vector(pP_, pQ_);
            Point vN_ = normalize(vPQ_);
            Point pX_ = calcX_(pP_, fracPC, vPQ_, d, vN_);

            Point dt = delta(pX, pX_);
            m_weight = weight(d);
            m_weightedDelta = weightedDelta(dt, m_weight);
        }

        public double getWeight()
        {
            return m_weight;
        }

        public Tuple<double,double> getWeightedDelta()
        {
            return m_weightedDelta;
        }



        public Xprime()
        {

        }

        public void test()
        {
            Point pP = new Point(5, 10);
            Point pQ = new Point(12, 10);
            Point pX = new Point(15, 10);

            Point vPQ = vector(pP, pQ);
            Point vN = normalize(vPQ);
            Point vXP = vector(pX, pP);
            Point vPX = vector(pP, pX);
            Point vQX = vector(pQ, pX);

            Console.WriteLine("vPQ = " + vPQ);
            Console.WriteLine("vN = " + vN);
            Console.WriteLine("vXP = " + vXP);
            Console.WriteLine("vPX = " + vPX);
            Console.WriteLine("vQX = " + vQX);

            double frac = proj(vPQ, vPX);
            double fracPC = fracPercent(frac, vPQ);

            Console.WriteLine("frac = " + frac);
            Console.WriteLine("fracPC = " + fracPC);

            double d;
            if (fracPC > 0.0 && fracPC < 1.0)
            {
                d = proj(vN, vXP);
                Console.WriteLine("if d = " + d);
            }
            else
            {
                double lenPX = magnitude(vPX);
                double lenQX = magnitude(vQX);
                double chosenLen = Math.Min(lenPX, lenQX);
                d = chosenLen;
                d = proj(vN, vXP);
                Console.WriteLine("else d = " + d);
            }
            

            Point pP_ = new Point(5, 10);
            Point pQ_ = new Point(12, 10);
            Point vPQ_ = vector(pP_, pQ_);
            Point pN_ = normalize(vPQ_);
            Point pX_ = calcX_(pP_, fracPC, vPQ_, d, pN_);

            Console.WriteLine();
            Console.WriteLine("vPQ_ = " + vPQ_);
            Console.WriteLine("pN_ = " + pN_);
            Console.WriteLine("pX_ = " + pX_);

            Point dt = delta(pX, pX_);
            double w = weight(d);
            Tuple <double,double> wd = weightedDelta(dt, w);

            double totalW = w;
            double twd_x = wd.Item1;
            double twd_y = wd.Item2;
            Point pAvgWD = new Point(
                (int)Math.Round(twd_x / totalW),
                (int)Math.Round(twd_y / totalW)
                );
            Point newX_ = new Point(pX.X + pAvgWD.X, pX.Y + pAvgWD.Y);

            Console.WriteLine("dt = " + dt);
            Console.WriteLine("w = " + w);
            Console.WriteLine("wd = " + wd);
            Console.WriteLine("pAvgWD = " + pAvgWD);
            Console.WriteLine("newX_ = " + newX_);

        }
    }
}
