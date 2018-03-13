using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NeuralNetworkLibrary
{
    public class NeuralNetwork
    {
        public int inputNodes;
        public int hiddenNodes;
        public int outputNodes;
        public double learningRate = .1;

        public Weights weights;

        public Func<double, double> activationFunction;


        public NeuralNetwork(int inputNodes, int hiddenNodes, int outputNodes)
        {
            this.inputNodes = inputNodes;
            this.hiddenNodes = hiddenNodes;
            this.outputNodes = outputNodes;


            Weights w = new Weights();
            #region WeightsInit
            Matrix ihWeights = new Matrix(this.hiddenNodes, this.inputNodes);
            Matrix hoWeights = new Matrix(this.outputNodes, this.hiddenNodes);
            Matrix ihBias = new Matrix(this.hiddenNodes, 1);
            Matrix hoBias = new Matrix(this.outputNodes, 1);
            ihWeights.RandomiseValues();
            hoWeights.RandomiseValues();
            ihBias.RandomiseValues();
            hoBias.RandomiseValues();
            w.inputHiddenWeight = ihWeights;
            w.hiddenOutputWeight = hoWeights;
            w.inputHiddenBias = ihBias;
            w.hiddenOutputBias = hoBias;
            #endregion WeightsInit

            this.weights = w;

            this.activationFunction = ActivationFunctions.Sigmoid;
        }

        public double[] Predict(double[] inputArray)
        {
            // Generating hidden outputs
            Matrix input = new Matrix(inputArray);
            Matrix hidden = this.weights.inputHiddenWeight * input;
            hidden += this.weights.inputHiddenBias;
            hidden.ApplyFunction(activationFunction);
            // Generating output output
            Matrix output = this.weights.hiddenOutputWeight * hidden;
            output += this.weights.hiddenOutputBias;
            output.ApplyFunction(activationFunction);
            return output.ToList().ToArray();
        }
        private List<double[]> FeedForwardWithHidden(double[] inputArray)
        {
            // Generating hidden outputs
            Matrix input = new Matrix(inputArray);
            Matrix hidden = this.weights.inputHiddenWeight * input;
            hidden += this.weights.inputHiddenBias;
            hidden.ApplyFunction(activationFunction);
            // Generating output output
            Matrix output = this.weights.hiddenOutputWeight * hidden;
            output += this.weights.hiddenOutputBias;
            output.ApplyFunction(activationFunction);
            return new List<double[]>() { output.ToList().ToArray(), hidden.ToList().ToArray() };
        }
        public void Train(double[] inputArray, double[] targetArray)
        {
            var feedForward = FeedForwardWithHidden(inputArray);
            var inputs = new Matrix(inputArray);
            var outputs = new Matrix(feedForward[0]);
            var hiddens = new Matrix(feedForward[1]);
            var targets = new Matrix(targetArray);
            var outputErrors = targets - outputs;

            var gradients = outputs.FunctionApplied((e) => { return e * (1 - e); });
            gradients %= outputErrors;
            gradients *= learningRate;

            var hiddenTransposed = hiddens.Transposed();
            var ΔhiddenOutputWeights = gradients * hiddenTransposed;

            this.weights.hiddenOutputWeight += ΔhiddenOutputWeights;
            this.weights.hiddenOutputBias += gradients;

            var hiddenOutputWeightTransposed = this.weights.hiddenOutputWeight.Transposed();
            var hiddenErrors = hiddenOutputWeightTransposed * outputErrors;

            var hiddenGradient = hiddens.FunctionApplied((e) => { return e * (1 - e); });
            hiddenGradient %= hiddenErrors;
            hiddenGradient *= learningRate;

            var inputsTransposed = inputs.Transposed();
            var ΔinputHiddenWeight = hiddenGradient * inputsTransposed;

            this.weights.inputHiddenWeight += ΔinputHiddenWeight;
            this.weights.inputHiddenBias += hiddenGradient;

        }
        public void RandomTrain(List<double[]> inputArrays, List<double[]> targetArrays, int iterations = -1, bool consoleLog = false, int consoleLogRepeat = 1000)
        {
            if (iterations == -1)
                iterations = (int)Math.Floor(100000.0 / hiddenNodes);
            if (iterations < 1000)
                iterations = 1000;
            if (inputArrays.Count != targetArrays.Count)
                return;
            Stopwatch s = new Stopwatch();
            s.Start();
            Random r = new Random();
            for (var i = 0; i < iterations; i++)
            {
                var index = (int)Math.Floor(r.NextDouble() * inputArrays.Count);
                this.Train(inputArrays[index], targetArrays[index]);
                if (consoleLog && (i + 1) % consoleLogRepeat == 0)
                {
                    Console.WriteLine("Trained for " + (i + 1).ToString() + " iterations");
                }
            }
            s.Stop();
            if (consoleLog)
                Console.WriteLine(iterations.ToString() + " training iterations completed on " + hiddenNodes.ToString()
                + " hidden neurons in " + s.ElapsedMilliseconds.ToString() + "ms");
        }

        public struct Weights
        {
            public Matrix inputHiddenWeight;
            public Matrix inputHiddenBias;
            public Matrix hiddenOutputWeight;
            public Matrix hiddenOutputBias;
        }
    }
}
