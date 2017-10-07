using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("_Lab1.Test")]

namespace _Lab1
{
    public partial class Form1 : Form
    {
        internal  BufferedGraphics bg;
        List<Charge> charges;
        Random rand;

        public Form1()
        {
            InitializeComponent();

        }

        internal void button1_Click(object sender, EventArgs e)
        {
            startProgram();
        }

        delegate void varOfPart(float dt, Font font, SolidBrush brush, PointF pIterations, float fps);

        internal void startProgram()
        {
            charges.Clear();
            bg.Graphics.Clear(Color.Black);


            Charge tmp1 = new Charge();
            tmp1.id = 0;
            tmp1.mass = 1;
            tmp1.position = new Vector2(50, 50);
            tmp1.radius = 2;
            tmp1.setPolarity(2);
            tmp1.thisBrush = new SolidBrush(Color.Blue);

            Charge tmp2 = new Charge();
            tmp1.id = 1;
            tmp1.mass = 1;
            tmp1.position = new Vector2(70, 50);
            tmp1.radius = 2;
            tmp1.setPolarity(-2);
            tmp1.thisBrush = new SolidBrush(Color.Red);

            charges.Add(tmp1);
            charges.Add(tmp2);
            //getRandomElements();

            if (countOfRandom.Text == "")
                countOfRandom.Text = "0";
            if (textIterations.Text == "")
                textIterations.Text = "0";
            if (textFPS.Text == "")
                textFPS.Text = "0";

            SolidBrush brush = new SolidBrush(Color.White);
            Font font = new Font("Consolas", 16, FontStyle.Bold);
            PointF pIterations = new PointF(pictureBox1.Width / 2, pictureBox1.Height / 11);
            PointF pRuntime = new PointF(pictureBox1.Width / 3 + 40, pictureBox1.Height / 2);

            float iterations = (float)Convert.ToDouble(this.textIterations.Text) / 10;
            float fps = (float)Convert.ToDouble(this.textFPS.Text);

            varOfPart part;
            if (checkBox1.Checked)
                part = new varOfPart(partB);
            else
                part = new varOfPart(partA);

            Stopwatch sw = new Stopwatch();

            sw.Start();

            for (float dt = 0; dt < iterations; dt += 0.01f)
            {
                part(dt, font, brush, pIterations, fps);
            }
            foreach (Charge charge in charges)
            {
                DrawElement(charge);
            }
            sw.Stop();

            bg.Graphics.DrawString("Runtime: " + Convert.ToString(sw.Elapsed), font, brush, pRuntime);
            bg.Render();
        }

        internal void partA(float dt, Font font, SolidBrush brush, PointF pIterations, float fps)
        {
            bg.Graphics.Clear(Color.Black);

            bg.Graphics.DrawString(Convert.ToString((int)(dt * 10 + 1)), font, brush, pIterations);

            computeNextStep(dt);
            computeClashs();
            computeBarrier(0.7F);

            foreach (Charge charge in charges)
            {
                DrawElement(charge);
            }

            Thread.Sleep((int)(1000 / fps));

            bg.Render();
        }

        internal void partB(float dt, Font font, SolidBrush brush, PointF pIterations, float fps)
        {
            computeNextStep(dt);
            computeClashs();
            computeBarrier(0.7F);
        }

        internal void computeNextStep(float dt)
        {
            List<Charge> newCharges = new List<Charge>();

            float f;

            Vector2 vecF = new Vector2();
            Vector2 vecDt = new Vector2(dt, dt);
            Vector2 singleVector;

            foreach (Charge tmp in charges)
            {
                vecF.X = 0; vecF.Y = 0;
                foreach (Charge tmp2d in charges)
                {
                    if (tmp == tmp2d) continue;

                    f = functionCoulomb(tmp.getPolarity(), tmp2d.getPolarity(), norm(tmp.position, tmp2d.position), 0.1F);

                    singleVector = getSingleVector(tmp.position, tmp2d.position);

                    vecF += singleVector * (new Vector2(f, f));
                }
                tmp.acceleration = new Vector2(vecF.X / tmp.mass, vecF.Y / tmp.mass);
                tmp.speed = tmp.speed + tmp.acceleration * vecDt;
                tmp.position = tmp.position + tmp.speed * vecDt;

                newCharges.Add(tmp);
            }
            charges.Clear();
            charges = new List<Charge>(newCharges);
            newCharges.Clear();
        }

        internal float functionCoulomb(float q1, float q2, float r, float k)
        {
            return (k * q1 * q2) / (r * r);
        }

        internal Vector2 getSingleVector(Vector2 tmp1, Vector2 tmp2)
        {
            float x = (tmp1.X - tmp2.X) / norm(tmp1, tmp2);
            float y = (tmp1.Y - tmp2.Y) / norm(tmp1, tmp2);

            return new Vector2(x, y);
        }

        internal float norm(Vector2 pos1, Vector2 pos2)
        {
            return (float) Math.Sqrt(
                (pos2.X - pos1.X) *
                (pos2.X - pos1.X) +
                (pos2.Y - pos1.Y) *
                (pos2.Y - pos1.Y));
        }

        internal void computeClashs()
        {
            List<Charge> deleteList = new List<Charge>();

            foreach(Charge tmp in charges)
                 foreach(Charge tmp2d in charges)
                 {
                     if (tmp == tmp2d) continue;

                     if (isClash(tmp, tmp2d) /*&& isDifferentCharges(tmp, tmp2d)*/)
                     {
                         if (tmp.radius == 0 || tmp2d.radius == 0) continue;

                         tmp.speed = tmp.speed + tmp2d.speed;
                         tmp.mass += tmp2d.mass;
                         tmp.acceleration = new Vector2(0, 0);
                         tmp.setPolarity(tmp.getPolarity() + tmp2d.getPolarity());
                         tmp.position = (tmp.position + tmp2d.position) / (new Vector2(2, 2));

                         deleteList.Add(tmp2d);

                         //tmp2d.speed = new Vector2(0, 0);
                         //tmp2d.acceleration = new Vector2(0, 0);
                         tmp2d.setPolarity(0);
                         tmp2d.radius = 0;
                         

                     }
                 }
            foreach(Charge tmp in deleteList)
                charges.Remove(tmp);
            deleteList.Clear();
        }

        internal bool isClash(Charge tmp1, Charge tmp2)
        {
            if (norm(tmp1.position, tmp2.position) <= (tmp1.radius + tmp2.radius))
                return true;
            return false;
        }

        internal bool isDifferentCharges(Charge tmp1, Charge tmp2)
        {
            if ((tmp1.getPolarity() * tmp2.getPolarity()) > 0)
                return false;
            return true;
        }

        internal void computeBarrier(float speedLoss)
        {
            foreach (Charge tmp in charges)
            {
                if (tmp.position.X < tmp.radius || tmp.position.X > (pictureBox1.Width - tmp.radius))
                {
                    tmp.speed.X *= -speedLoss;
                    if (tmp.position.X < tmp.radius)
                        tmp.position.X = tmp.radius;
                    else
                        tmp.position.X = pictureBox1.Width - tmp.radius;
                }
                if (tmp.position.Y < tmp.radius || tmp.position.Y > (pictureBox1.Height - tmp.radius))
                {
                    tmp.speed.Y *= -speedLoss;
                    if (tmp.position.Y < tmp.radius)
                        tmp.position.Y = tmp.radius;
                    else
                        tmp.position.Y = pictureBox1.Height - tmp.radius;
                }
            }
        }

        internal void DrawElement(Charge charge)
        {
            bg.Graphics.FillEllipse(charge.thisBrush, charge.position.X, charge.position.Y, charge.radius, charge.radius);
        }

        internal void Form1_Load(object sender, EventArgs e)
        {
            bg = BufferedGraphicsManager.Current.Allocate(pictureBox1.CreateGraphics(), pictureBox1.DisplayRectangle);
            charges = new List<Charge>();
            rand = new Random();
        }

        internal void getRandomElements()
        {
            charges.Clear();

            int count;
            if (this.countOfRandom.Text == "")
                count = 0;
            else
                count = Convert.ToInt32(this.countOfRandom.Text);

            for (int i = 0; i < count; i++)
            {
                Charge charge = new Charge();
                charge.id = i;
                charge.radius = 5;
                charge.mass = 1;
                charge.setPolarity(rand.Next(-10, 10));
                charge.position.X = rand.Next(0 + charge.radius * 2, pictureBox1.Width - charge.radius * 2);
                charge.position.Y = rand.Next(0 + charge.radius * 2, pictureBox1.Height - charge.radius * 2);
                //charge.speed = new Vector2(rand.Next(1, 2), rand.Next(1, 2));
                //charge.acceleration = new Vector2(rand.Next(0, 1), rand.Next(0, 1));
                charge.speed = new Vector2(0,0);
                charge.acceleration = new Vector2(0, 0);

                charges.Add(charge);
            }
        }
    }
}
