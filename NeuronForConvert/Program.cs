using System;

namespace NeuronForConvert
{
    class Program
    {

        public class Neuron
        {
            private decimal weight = 0.5m;
            public decimal LastError { get; private set; }
            public decimal Smoothing { get; set; } = 0.000001m;
            public decimal ProcessInputData(decimal input)
            {
                return input * weight;
            }

            public decimal RestoreInputData(decimal output)
            {
                return output / weight;
            }

            public void Train(decimal input, decimal expectedResult)
            {
                var actualResult = input * weight;
                LastError = expectedResult - actualResult;
                var correction = (LastError / actualResult) * Smoothing;
                weight += correction;
            }
        }

        static void Main(string[] args)
        {
            decimal km = 100;
            decimal miles = 62.1371m;

            Neuron neuron = new Neuron();

            int i = 0;
            do
            {
                i++;
                neuron.Train(km, miles);
                if(i % 100000 == 0)
                {
                    Console.WriteLine($"Iteration: {i}\tError:\t {neuron.LastError}");
                }
            } while (neuron.LastError > neuron.Smoothing || neuron.LastError < -neuron.Smoothing);

            Console.WriteLine("Neuron traing end.\n");

            Console.WriteLine($"{neuron.ProcessInputData(100)} miles in {100} km.");
            Console.WriteLine($"{neuron.ProcessInputData(541)} miles in {541} km.");
            Console.WriteLine($"{neuron.RestoreInputData(10)} km in {10} miles.");
            Console.ReadKey();
        }
    }
}
