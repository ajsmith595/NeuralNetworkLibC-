using System;

namespace NeuralNetworkLibrary
{
    public static class ActivationFunctions
    {
        public static double Sigmoid(double x)
        {
            return (1 / (1 + Math.Exp(-x)));
        }
    }
}
