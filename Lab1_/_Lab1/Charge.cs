using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace _Lab1
{
    public class Charge
    {
        public int id;
        public float mass;
        public Vector2 position;
        public Vector2 speed;
        public Vector2 acceleration;
        public int radius;
        public SolidBrush thisBrush;
        private float polarity;

        public Charge()
        {
            id = 0;
            mass = 0;
            position = new Vector2(0f, 0f);
            speed = new Vector2(0f, 0f);
            acceleration = new Vector2(0f, 0f); ;
            polarity = 0;
            radius = 0;
        }

        public Charge(Charge charge)
        {
            id = charge.id;
            mass = charge.mass;
            position = charge.position;
            speed = charge.speed;
            acceleration = charge.acceleration;
            polarity = charge.polarity;
            radius = charge.radius;
        }

        public void setPolarity(float pol)
        {
            if (pol > 0)
                thisBrush = new SolidBrush(Color.Blue);
            else if (pol == 0)
                thisBrush = new SolidBrush(Color.Gray);
            else
                thisBrush = new SolidBrush(Color.Red);
            polarity = pol;
        }

        public static bool operator ==(Charge tmp1, Charge tmp2)
        {
            if (tmp1.id == tmp2.id)
                return true;
            return false;
        }

        public static bool operator !=(Charge tmp1, Charge tmp2)
        {
            if(tmp1 == tmp2) 
                return false;
            return true;
        }

        public float getPolarity()
        {
            return polarity;
        }
    }
}
