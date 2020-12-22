using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Matrix
    {
        Vector a, b, c, k1, k2;
        int k;
        static Random random = new Random();
        public static int diapazon = 100;
        public int Rows => b.Dimension;
        public int Columns => b.Dimension;
        public Matrix(int d, int k)
        {
            a = new Vector(d -1);
            b = new Vector(d);
            c = new Vector(d - 1);
            k1 = new Vector(d-3);
            k2 = new Vector(d-3);
            this.k = k;
        }
        
        public double this [int i, int j]
        {
            get
            {
                if (i > Rows || i <= 0 || j > Columns || j <= 0)
                    throw new Exception();
                if (i == j)
                    return b[i];
                if (i + 1 == j && i>=1)
                    return c[i];
                if (i - 1 == j && i>=2)
                    return a[j];
                if (k == j && i < j)
                    return k1[i];
                if (k==j && i>j)
                    return k1[i - 3];
                if (k + 1 == j && i<j)
                    return k2[i];
                if (k + 1 == j && i > j)
                    return k2[i - 3];
               // throw new Exception();
                return 0;
            }
            set
            {
                if (i > Rows || i <= 0 || j > Columns || j <= 0)
                    throw new Exception();
                if (i == j)
                    b[i]=value;
                if (i + 1 == j && i>=1)
                    c[i] =value;
                if (i - 1 == j && i>=2)
                    a[i - 1]=value;
                if (k == j)
                    k1[i] =value;
                if (k + 1 == j)
                    k2[i]=value;
                //throw new Exception();
            }
        }
        public static Vector operator * (Matrix m, Vector v)
        {
            if (m.Columns != v.Dimension)
                throw new Exception("IncompableTypes");
            Vector res = new Vector(v.Dimension);
            for (int i=1; i<=m.Rows; ++i)
            {

                res[i] = 0;
                if (i-1>0)
                {
                    res[i] += (m.a[i - 1] * v[i - 1]);
                }
                    
                res[i] += (m.b[i] * v[i]);
                if (i < m.Columns)
                {
                    res[i] += (m.c[i] * v[i + 1]);
                }
                    
                if (i < m.k - 1)
                {
                    res[i] += (m.k1[i] * v[m.k]);
                    res[i] += (m.k2[i] * v[m.k+1]);
                }
                else 
                    if (i > m.k + 2)
                    {
                        res[i] += (m.k1[i-3] * v[m.k]);
                        res[i] += (m.k2[i-3] * v[m.k + 1]);
                    }
                else
                    if(i==m.k-1)
                    {
                        res[i] += (m.k2[i] * v[m.k+1]);
                    }
                else
                    if (i==m.k+2)
                    {
                        res[i] += (m.k1[i-3] * v[m.k]);
                        
                    }

            }
            return res;
        }
        
        public void PrintAll(Vector res)
        {
            Print();
            Console.Write("a: ");
            a.Print();
            Console.Write("b: ");
            b.Print();
            Console.Write("c: ");
            c.Print();
            Console.Write("k1: ");
            k1.Print();
            Console.Write("k2: ");
            k2.Print();

            Console.Write("result: ");
            res.Print();
            Console.WriteLine();

        }
        public Matrix()
        {
            a = new Vector(new double[] { 2, 3, 2, 3, 1, 4, 1 });
            b = new Vector(new double[] { 1, 3, 2, 2, 1, 4, 1, 3, });
            c = new Vector(new double[] { 2, 4, 2, 1, 5, 2, 1 });
            k1 = new Vector(new double[] { 2, 1, 3, 1, 3, });
            k2 = new Vector(new double[] { 3, 1, 3, 2, 1 });
            k = 4;
        }
        void Step1 (Vector res)
        {
            double s;
            //Обнуляем вектор под главной диагональю
            for (int i=1; i<k; ++i)
            {
                s = (1/b[i]);
                b[i] = 1;
                c[i] *= s;
                res[i] *= s;
                if (i != k - 1)
                {
                    k1[i] *= s;
                    k2[i] *= s;
                }
                else
                    k2[i] *= s;


                s = a[i]*(-1);
                a[i] = 0;
                b[i + 1] += c[i] * s;
                res[i + 1] += res[i] * s;

                //Проверяем 'наложение' диагональных векторов на векторы k1, k2
                if(k-i>2)
                {
                    k1[i + 1] += k1[i] * s;
                    k2[i + 1] += k2[i] * s;
                }
                else
                if (k-i==2)
                {
                    c[i + 1] += k1[i] * s;
                    k2[i + 1] += k2[i] * s;
                }
                else
                    if(k-i==1)
                    {
                        c[i + 1] += k2[i] * s;
                    }
            }
        }
        //Обнудяем вектор над главной диагональю
        void Step2(Vector res)
        {
            double s;
            for (int i=Rows; i>=k+2; --i)
            {
                s = (1/b[i]);
                b[i] = 1;
                a[i - 1] *= s;
                res[i] *= s;

                if (i != k + 2)
                {
                    k1[i - 3] *= s;
                    k2[i - 3] *= s;
                }
                else
                    k1[i - 3] *= s;

                s = -c[i - 1];
                c[i - 1] = 0;
                b[i - 1] += (a[i - 1] * s);
                res[i - 1] += res[i] * s;
                if (i>k+3)
                {
                    k1[i - 4] += k1[i - 3] * s;
                    k2[i - 4] += k2[i - 3] * s;
                }
                else
                if(i==k+3)
                {
                    k1[i - 4] += k1[i - 3] * s;
                    a[i - 2] += k2[i - 3] * s;
                }
                else
                    if (i==k+2)
                    {
                        a[i - 2] += k1[i - 3] * s;
                    }
            }
        }
        //уничтожение k1
        void Step3(Vector res)
        {

            double s = (1 / b[k]);
            b[k] = 1;
            c[k] *= s;
            res[k] *= s;
            //обнуляем k до элементов диагонали
            for (int i=1; i<=k-2; ++i)
            {
                s = -k1[i];
                k1[i] = 0;
                k2[i] += c[k] * s;
                res[i] += res[k] * s;
            }


            s = -c[k - 1];
            c[k - 1] = 0;
            k2[k - 1] += c[k] * s;
            res[k - 1] += res[k] * s;

            s = -a[k];
            a[k] = 0;
            b[k + 1] += c[k] * s;
            res[k + 1] += res[k] * s;

            for (int i=k+2; i<=Rows; ++i)
            {
                s = -k1[i - 3];
                k1[i - 3] = 0;
                res[i] += res[k] * s;
                if (i == k + 2)
                {
                    a[k + 1] += c[k] * s;
                }
                else
                    k2[i - 3] += c[k] * s;
            }
        }

        //Аналогично c k2
        void Step4(Vector res)
        {
            double s = (1 / b[k + 1]);
            b[k + 1] = 1;
            res[k + 1] *= s;
            for (int i=1; i<=k-1; ++i)
            {
                s = -k2[i];
                k2[i] = 0;
                res[i] += res[k + 1] * s;
            }
            s = -c[k];
            c[k] = 0;
            res[k] += res[k + 1] * s;
            s = -a[k + 1];
            a[k + 1] = 0;
            res[k + 2] += res[k + 1] * s;
            for (int i=k+3; i<=Rows; ++i)
            {
                s = -k2[i - 3];
                k2[i - 3] = 0;
                res[i] += res[k + 1] * s;
            }
        }
        //Приведение к единичной
        void Step5(Vector res)
        {
            double s;
            for (int i=k+2; i<Rows; ++i)
            {
                s = -a[i];
                a[i] = 0;
                res[i + 1] += res[i] * s;
            }
            for (int i=k-1; i>1; --i)
            {
                s = -c[i - 1];
                c[i - 1] = 0;
                res[i - 1] += res[i] * s;
            }
        }
        public Vector LinearSolve (Vector res)
        {
            //Console.WriteLine("Showing vectors");
            //PrintAll(res);
            Vector TransformToIdentityMatrix()
            {
                Vector result = Vector.Copy(res);
                if (k == Columns || k <= 0)
                    throw new Exception();
                Step1(result);
                Step2(result);
                Step3(result);
                Step4(result);
                Step5(result);
                return result;
            }
            
            return TransformToIdentityMatrix();
        }
        public static Matrix ReadFromFile(System.IO.StreamReader r )
        {
            string[] v = r.ReadLine().Split(' ');
            int d = Int32.Parse(v[0]);
            int k = Int32.Parse(v[1]);
            Matrix res = new Matrix(d, k);
            for (int i=1; i<=d; ++i)
            {
                string[] nums = r.ReadLine().Split(' ');
                for (int j=1; j<=d; ++j)
                    res[i, j] = Double.Parse(nums[j]);
            }
            return res;
        }
        public static Matrix GenerateMatrix( int d, int k)
        {
            Matrix res = new Matrix(d, k);
            for (int i = 1; i <= res.a.Dimension; ++i)
            {
                res.a[i] = random.Next(diapazon, diapazon*2)+random.NextDouble();
            }

            for (int i = 1; i <= d; ++i)
            {
                res.b[i] = 0;
                while (res.b[i] == 0)
                    res.b[i] = random.Next(-diapazon, diapazon) +random.NextDouble();
            }
            for (int i = 1; i <= d - 1; i++)
            {
                res.c[i] = random.Next(-diapazon, diapazon) +random.NextDouble();
            }

            for (int i=1; i<=d-3; i++)
            {
                res.k1[i] = random.Next(-diapazon, diapazon) +random.NextDouble();
                res.k2[i] = random.Next(-diapazon, diapazon) +random.NextDouble();
            }
            return res;
        }
        public void Print()
        {
            for (int i=1; i<=Rows; ++i)
            {
                for (int j = 1; j <= Columns; j++)
                    Console.Write( "{0:f16}      ", this[i, j]);
                Console.WriteLine();
            }
                
        }
        
    }
}
