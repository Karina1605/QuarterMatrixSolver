using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            double inaccuracy;
            double max=0;
            double min=0;

            //Пример
            /*Matrix mex = new Matrix();
            Vector vex = new Vector(new double[] {1, 2, 3, 1,4, 2, 3, 1 });
            Vector resex = mex * vex;
            Console.Write("Searching vector: ");
            vex.Print();
            Console.WriteLine("Matrix: ");
            mex.PrintAll(resex);
            Vector resultex = mex.LinearSolve(resex);
            Console.Write("Vector fom method: ");
            resultex.Print();
            Console.WriteLine("Error: " + Vector.GetInaccuracy(vex, resultex));
            */
            //Генерация по 5 матриц каждого размера и вычисление погрешностей
            Console.WriteLine("N\t\t|Средняя погрешность\t\t\t|Максимальная\t\t\t\t|Минимальная");
            for (int i=10; i<=1500; i*=2)
            {
                inaccuracy = 0;
                int count = 0;
                while(count==0)
                {
                    Matrix m1 = Matrix.GenerateMatrix(i, 2 + r.Next(i - 4));
                    Vector v1 = Vector.GenerateVector(i);
                    Vector resv = m1 * v1;
                    try
                    {
                        Vector res = m1.LinearSolve(resv);
                        count++;
                        inaccuracy += Vector.GetInaccuracy(v1, res);
                        max = min = inaccuracy;
                    }
                    catch (DivideByZeroException e)
                    {

                    }
                }
                while (count<5)
                {
              
                    Matrix m1 = Matrix.GenerateMatrix(i, 2+ r.Next(i-4));
                    Vector v1 = Vector.GenerateVector(i);
                    Vector resv = m1 * v1;
                    try
                    {
                        Vector res = m1.LinearSolve(resv);
                        count++;
                        double error = Vector.GetInaccuracy(v1, res);
                        inaccuracy += error;
                        if (error < min)
                            min = error;
                        else
                            if (error > max)
                            max = error;
                    }
                    catch (DivideByZeroException e)
                    {

                    }
                }
                Console.Write(i + "\t   ");
                Console.WriteLine("{0:e20}\t\t  |{1:e20}\t\t  |{2:e20}", (inaccuracy / count), max, min);
            }
            Console.ReadLine();
        }
    }
}
