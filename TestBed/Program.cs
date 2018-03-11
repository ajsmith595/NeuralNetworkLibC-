using NeuralNetworkLibrary;
using System;
using System.Collections.Generic;

namespace TestBed
{
    class Program
    {
        static void Main(string[] args)
        {

            NeuralNetwork nn = new NeuralNetwork(2, 2, 1);
            List<List<double[]>> targets = new List<List<double[]>>() {
                new List<double[]>(){new double[]{0, 0 }, new double[] {0} },
                new List<double[]>(){new double[]{1, 0 }, new double[] {1} },
                new List<double[]>(){new double[]{0, 1 }, new double[] {1} },
                new List<double[]>(){new double[]{1, 1 }, new double[] {0} }
            };

            int iterations = 50000;
            Random r = new Random();
            for (var i = 0; i < iterations; i++)
            {
                int val = (int)Math.Floor(r.NextDouble() * targets.Count);
                nn.Train(targets[val][0], targets[val][1]);
                Console.WriteLine("Trained: " + (i + 1).ToString());
            }

            ArrayPrint(nn.FeedForward(new double[] { 0, 0 }));
            ArrayPrint(nn.FeedForward(new double[] { 1, 0 }));
            ArrayPrint(nn.FeedForward(new double[] { 0, 1 }));
            ArrayPrint(nn.FeedForward(new double[] { 1, 1 }));

            Console.Read();
        }

        static void ArrayPrint<T>(T[] arr)
        {
            string s = "";
            for (var i = 0; i < arr.Length; i++)
            {
                s += arr[i].ToString() + ", ";
            }
            s = s.Substring(0, s.Length - 2);
            Console.WriteLine(s);
        }
    }
}
