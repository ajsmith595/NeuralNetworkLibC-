using NeuralNetworkLibrary;
using System;
using System.Collections.Generic;

namespace TestBed
{
    class Program
    {
        static void Main(string[] args)
        {

            NeuralNetwork nn = new NeuralNetwork(2, 400, 1);
            List<double[]> inputs = new List<double[]>() {
                new double[] { 0, 0 },
                new double[] { 1, 0 },
                new double[] { 0, 1 },
                new double[] { 1, 1 }
            };
            List<double[]> targets = new List<double[]>() {
                new double[] { 0 },
                new double[] { 1 },
                new double[] { 1 },
                new double[] { 0 }
            };
            nn.RandomTrain(inputs, targets, -1, true);

            ArrayPrint(nn.Predict(new double[] { 0, 0 }));
            ArrayPrint(nn.Predict(new double[] { 1, 0 }));
            ArrayPrint(nn.Predict(new double[] { 0, 1 }));
            ArrayPrint(nn.Predict(new double[] { 1, 1 }));

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
