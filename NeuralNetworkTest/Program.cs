using System;
using System.Collections.Generic;

namespace NeuralNetworkTest
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Training sin()...");
            Console.WriteLine("");

            //FannCSharpTest(args);
            //NeuralNetTest(args);
            //NeuralNetTest2(args);
            NeuralNetworkTest(args);
        }

        /**
         * FANNCSHARP does not seem to be working on MAC
         */
        public static void FannCSharpTest(string[] args)
        {
            Random r = new Random();

            List<uint> layers = new List<uint>();
            layers.Add(1); // inputs
            layers.Add(10); // hidden
            layers.Add(1); // output

            FANNCSharp.Double.NeuralNet network = new FANNCSharp.Double.NeuralNet(20.0f, layers.ToArray());

            System.Collections.Generic.List<double> input = new System.Collections.Generic.List<double>();
            System.Collections.Generic.List<double> output = new System.Collections.Generic.List<double>();

            Console.WriteLine("\tTraining...");
            for (int i = 0; i < 650000; i++)
            {
                double random = r.NextDouble();
                input.Clear();
                input.Add(random);

                output.Clear();
                output.Add(Math.Sin(random));
                network.Train(input.ToArray(), output.ToArray());
                //Console.WriteLine("Train In: " + input[0] + " out: " + output[0]);
            }

            Console.WriteLine("Training complete.");

            for (int i = 0; i < 10; i++)
            {
                input.Clear();
                input.Add(r.NextDouble());
                double[] theOuptut = network.Run(input.ToArray());

                Console.WriteLine("In: " + input[0] + " out: " + theOuptut[0] + " expected " + Math.Sin(input[0]));
            }
        }

		/**
         * NeuralNet test, but this seems to not train properly, at least i'm
         * not gettign results near the wanted accuracy, the results seem random
         * so either i'm training/running it run, or there's a bug in the neural
         * network library code
         *
         * http://social.technet.microsoft.com/wiki/contents/articles/36428.basis-of-neural-networks-in-c.aspx
         */
		public static void NeuralNetTest(string[] args)
        {
            NeuralNet.NeuralNet.NeuralNetwork network = new NeuralNet.NeuralNet.NeuralNetwork(
                21.5, new int[] { 1, 10, 1 }
            );

            System.Collections.Generic.List<double> input = new System.Collections.Generic.List<double>();
            System.Collections.Generic.List<double> output = new System.Collections.Generic.List<double>();
            Random r = new Random();

            Console.WriteLine("Training...");
            double[][] trainingSet = new double[65000][];

            for (int i = 0; i < 65000; i++)
            {
                double random = r.NextDouble();

                trainingSet[i] = new double[] {
                    random,
                    Math.Sin(random)
                };
            }

            for (int round = 0; round < 100; round++)
            {
                for (int i = 0; i < 65000; i++)
                {
                    input.Clear();
                    input.Add(trainingSet[i][0]);

                    output.Clear();
                    output.Add(trainingSet[i][1]);

                    network.Train(input, output);
                }
            }

            Console.WriteLine("Training complete.");

            for (int i = 0; i < 10; i++)
            {
                input.Clear();
                input.Add(r.NextDouble());
                double[] theOuptut = network.Run(input);

                Console.WriteLine("In: " + input[0] + " out: " + theOuptut[0] + " expected " + Math.Sin(input[0]));
            }
        }

		/**
         * NeuralNet test, but this seems to not train properly, at least i'm
         * not gettign results near the wanted accuracy, the results seem random
         * so either i'm training/running it run, or there's a bug in the neural
         * network library code
         * 
         * http://social.technet.microsoft.com/wiki/contents/articles/36428.basis-of-neural-networks-in-c.aspx
         */
		public static void NeuralNetTest2(string[] args)
        {
            NeuralNet.NeuralNet.NeuralNetwork network = new NeuralNet.NeuralNet.NeuralNetwork(
                21.5, new int[] { 1, 10, 1 }
            );


            System.Collections.Generic.List<double> input = new System.Collections.Generic.List<double>();
            System.Collections.Generic.List<double> output = new System.Collections.Generic.List<double>();
            Random r = new Random();

            Console.WriteLine("Training...");
            double[][] trainingSet = new double[65000][];

            for (int i = 0; i < 65000; i++)
            {
                double random = r.NextDouble();
                input.Clear();
                input.Add(random);

                output.Clear();
                output.Add(Math.Sin(random));
                network.Train(input, output);
                //Console.WriteLine("Train In: " + input[0] + " out: " + output[0]);
            }

            Console.WriteLine("Training complete.");

            for (int i = 0; i < 10; i++)
            {
                input.Clear();
                input.Add(r.NextDouble());
                double[] theOuptut = network.Run(input);

                Console.WriteLine("In: " + input[0] + " out: " + theOuptut[0] + " expected " + Math.Sin(input[0]));
            }

        }

		/**
         * NeurelNetwork test, this one works
         *
         * But for this you need to construct to total training set,
         * then train it so you need everything in memory, also there is no
         * support for multiple layers of neurons.
         *
         * https://github.com/trentsartain/Neural-Network
         */
		public static void NeuralNetworkTest(string[] args)
        {
            NeuralNetwork.Network.Network network = new NeuralNetwork.Network.Network(1, 10, 1);

            System.Collections.Generic.List<double> input = new System.Collections.Generic.List<double>();
            System.Collections.Generic.List<double> output = new System.Collections.Generic.List<double>();
            Random r = new Random();

            Console.WriteLine("Building training set...");

            System.Collections.Generic.List<NeuralNetwork.Network.DataSet> trainingSet
                      = new System.Collections.Generic.List<NeuralNetwork.Network.DataSet>();

            for (int i = 0; i < 10000; i++)
            {
                double random = r.NextDouble();

                trainingSet.Add(
                    new NeuralNetwork.Network.DataSet(
                        new double[] { random },
                        new double[] { Math.Sin(random) }
                    )
                );
            }

            Console.WriteLine("Training...");
            network.Train(trainingSet, 0.00275);

            Console.WriteLine("Training complete.");

            for (int i = 0; i < 10; i++)
            {
                input.Clear();
                input.Add(r.NextDouble());
                double[] theOuptut = network.Compute(input.ToArray());

                Console.WriteLine("In: " + input[0] + " out: " + theOuptut[0] + " expected " + Math.Sin(input[0]));
            }

        }
    }
}