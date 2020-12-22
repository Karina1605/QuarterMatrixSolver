using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Vector
    {
        double[] vector;
        public int Dimension => vector.Length;
        public static int diapazon = 100;
       public double this[int i]
        {
            get
            {
                if (i <= 0 && i > vector.Length)
                    throw new IndexOutOfRangeException();
                return vector[i - 1];
            }
            set
            {
                if (i <= 0 && i > vector.Length)
                    throw new IndexOutOfRangeException();
                vector[i - 1] = value;
            }
        }
        public Vector(double[] arr)
        {
            this.vector = arr;
        }
        public Vector (int dimension=8)
        {
            vector = new double[dimension];

        }
        public static Vector operator + (Vector v1, Vector v2)
        {
            if (v1.Dimension != v2.Dimension)
                throw new Exception("Different dimentions!");
            Vector res = new Vector(v1.Dimension);
            for (int i = 1; i <= res.Dimension; ++i)
                res[i] = v1[i] + v2[i];
            return res;
            
        }
        public static Vector operator -(Vector v1, Vector v2)
        {
            if (v1.Dimension != v2.Dimension)
                throw new Exception("Different dimentions!");
            Vector res = new Vector(v1.Dimension)*(-1);
            for (int i = 1; i <= res.Dimension; ++i)
                res[i] = v1[i] - v2[i];
            return res;

        }
        public static Vector operator * (double d, Vector V)
        {
            Vector r = new Vector(V.Dimension);
            for (int i = 1; i <= r.Dimension; ++i)
                r[i] = V[i] * d;
            return r;
        }
        public static Vector operator * (Vector V, double d)
        {
            return d * V;
        }
        public static double operator * (Vector v1, Vector v2)
        {
            if (v1.Dimension != v2.Dimension)
                throw new Exception();
            double res = 0;
            for (int i=1; i<=v1.Dimension; ++i)
            {
                res += v1[i] * v2[i];
            }
            return res;
        }
        public static Vector Copy ( Vector v2)
        {
            Vector v1 = new Vector(v2.Dimension);
            for (int i = 1; i <= v2.Dimension; i++)
                v1[i] = v2[i];
            return v1;
        }
        public static Vector ReadVector(System.IO.StreamReader r)
        {
            int v = Int32.Parse( r.ReadLine());
            Vector res = new Vector(v);
            string[] nums = r.ReadLine().Split(' ');
            for (int i = 1; i <= v; ++i)
                res[i] = Double.Parse(nums[i]);
            return res;

        }
        public static Vector GenerateVector(int d)
        {
            Vector r = new Vector(d);
            r.FillVector();
            return r;
        }
        public void FillVector()
        {
            Random random = new Random();
            for (int i = 1; i <= Dimension; ++i)
            {
                this[i] = random.Next(-diapazon, diapazon) + random.NextDouble();
            }
                
        }
        public void Print()
        {
            for (int i = 1; i <= Dimension; ++i)
                Console.Write(this[i]+"  ");
            Console.WriteLine();
        }
        public static double GetInaccuracy(Vector v1, Vector v2)
        {
            if (v1.Dimension != v2.Dimension)
                throw new Exception();
            double res= Math.Abs(v1[1] - v2[1]);
            for (int i = 2; i <= v1.Dimension; ++i)
                if (Math.Abs(v1[i]-v2[i])>res)
                    res = Math.Abs(v1[i] - v2[i]);
            return res;
            
        }
    }
}
