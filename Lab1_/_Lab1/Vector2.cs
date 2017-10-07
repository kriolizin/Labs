using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _Lab1
{
    public class Vector2
    {
        public Vector2()
        {
            X = 0;
            Y = 0;
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2(Vector2 vec)
        {
            X = vec.X;
            Y = vec.Y;
        }

        public static Vector2 operator *(Vector2 tmp1, Vector2 tmp2)
        {
            Vector2 res = new Vector2();
            res.X = tmp1.X * tmp2.X;
            res.Y = tmp1.Y * tmp2.Y;

            return res;
        }

        public static Vector2 operator +(Vector2 tmp1, Vector2 tmp2)
        {
            Vector2 res = new Vector2();
            res.X = tmp1.X + tmp2.X;
            res.Y = tmp1.Y + tmp2.Y;

            return res;
        }

        public static Vector2 operator -(Vector2 tmp1, Vector2 tmp2)
        {
            Vector2 res = new Vector2();
            res.X = tmp1.X - tmp2.X;
            res.Y = tmp1.Y - tmp2.Y;

            return res;
        }

        public static Vector2 operator /(Vector2 tmp1, Vector2 tmp2)
        {
            Vector2 res = new Vector2();
            res.X = tmp1.X / tmp2.X;
            res.Y = tmp1.Y / tmp2.Y;

            return res;
        }

        public static bool operator ==(Vector2 tmp1, Vector2 tmp2)
        {
            return (tmp1.X == tmp2.X && tmp1.Y == tmp2.Y);
        }

        public static bool operator !=(Vector2 tmp1, Vector2 tmp2)
        {
            return (tmp1.X != tmp2.X || tmp1.Y != tmp2.Y);
        }

        public float X;
        public float Y;
    }
}
