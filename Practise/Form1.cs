using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Practise
{
    public partial class Form1 : Form
    {
        private float x1 = 0, x2 = 0;
        private float freeCoef1 = 0, freeCoef2 = 0;

        List<PointF> list1 = new List<PointF>();
        List<PointF> list2 = new List<PointF>();
        PointF crossPoint;

        Bitmap bmp;
        Graphics graph;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {

            try
            {
                x1 = Convert.ToSingle(textBoxX1.Text) * -1;
                x2 = Convert.ToSingle(textBoxX2.Text) * -1;
                freeCoef1 = Convert.ToSingle(textBoxFreeCoef1.Text);
                freeCoef2 = Convert.ToSingle(textBoxFreeCoef2.Text);

                list1.Clear();
                list2.Clear();

                for (float i = -100; i < 100; i += 10)
                {
                    PointF point = new PointF();
                    point.X = i;
                    point.Y = (x1 * i) + freeCoef1;
                    point.X += 240;
                    point.Y += 140;

                    list1.Add(point);

                    point.Y = (x2 * i) + freeCoef2;
                    point.Y += 140;

                    list2.Add(point);
                }

                listBoxLine1.DataSource = null;
                listBoxLine2.DataSource = null;

                listBoxLine1.DataSource = list1;
                listBoxLine2.DataSource = list2;
            }
            catch(FormatException)
            {
                MessageBox.Show("Неккоректные данные");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Неккоректные данные");
            }

            labelPoint.Text = findCross(x1, freeCoef1, x2, freeCoef2).ToString();
            label8.Text = findAngle().ToString();


        }
        private void button1_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graph = Graphics.FromImage(bmp);
            Pen pen = new Pen(Color.Black);
            graph.DrawLine(pen, list1[0], list1[19]);
            pen.Color = Color.Red;
            graph.DrawLine(pen, list2[0], list2[19]);
            pictureBox1.Image = bmp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private PointF findCross(float x1, float freeCoef1, float x2, float freeCoef2)
        {
            float y1 = -1;
            float y2 = -1;
            x1 = -x1;
            x2 = -x2;

            PointF pt = new PointF();
            float d = (float)(x1 * y2 - y1 * x2);
            float dx = (float)(-freeCoef1 * y2 + y1 * freeCoef2);
            float dy = (float)(-x1 * freeCoef2 + freeCoef1 * x2);
            pt.X = (float)(dx / d);
            pt.Y = (float)(dy / d);
            return pt;

            
            
        }
       
        private double findAngle()
        {
            float a1 = list1[0].X;
            float a2 = list1[1].Y;
            float b1 = list2[0].X;
            float b2 = list2[1].Y;

            float scalar = a1 * b1 + a2 * b2;
            double modulA = Math.Sqrt(Math.Pow(a1, 2) + Math.Pow(a2, 2));
            double modulB = Math.Sqrt(Math.Pow(b1, 2) + Math.Pow(b2, 2));
            double cos = scalar / (modulA * modulB);

            double angle = Math.Acos(cos);
            return  Math.Round(angle * 100, 2);
        }

        https://github.com/RaskhozhevaNatalia/practika.git
    }
}
