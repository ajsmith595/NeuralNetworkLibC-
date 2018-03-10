using NeuralNetworkLibrary;
using System;
using System.Collections.Generic;

namespace TestBed
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix m1 = new Matrix(2, 3);
            m1[0] = new List<float>() { 0, 1, 7 };
            m1[1] = new List<float>() { 8, 9, 4 };

            Console.WriteLine(m1);
            Console.WriteLine(m1.Transposed());

            Matrix m2 = new Matrix(3, 2);
            m2[0] = new List<float>() { 9, 2 };
            m2[1] = new List<float>() { 1, 3 };
            m2[2] = new List<float>() { 0, 4 };
            Console.WriteLine(m2);
            m2.Transpose();
            Console.WriteLine(m2);

            m2.Transpose();


            Matrix m = m1 * m2;
            Console.WriteLine(m);
            Console.Read();
        }
    }
}
