using GA.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BasicTypes;
using GA.Helpers;
using System.Diagnostics;

namespace GA.Implementations
{
    public class RouletteWheelSelection : ISelectionOperator
    {
        public void CalculateDistribuance(Individual[] currentPopulation, out List<double> distribuance)
        {
            var random = RandomProvider.Current;
            var sumOfFitness = currentPopulation
                .Sum(x => x.Fitness);

            double cummulativeFitness = 0;
            distribuance = currentPopulation
                .Select(x =>
                {
                    cummulativeFitness += x.Fitness / sumOfFitness;
                    return cummulativeFitness;
                })
                .ToList();
        }

        private Individual SelectCurrentParent(double random, Individual[] currentPopulation, List<double> distribuance)
        {
			int currentIndex = 0;
			for (int i = 0; i < distribuance.Count; i++)
			{
				if(distribuance[i] > random)
				{
					currentIndex = i;
					break;
				}
			}

            //var currentIndex = distribuance
            //    .Select((x, i) => new { Index = i, Value = x })
            //    .FirstOrDefault(x => x.Value > random).Index;

            //Console.WriteLine($"Individual: {currentIndex}");

            return currentPopulation[currentIndex].Clone();
        }

        public Individual[] GenerateParentPopulation(Individual[] currentPopulation)
        {
            var random = RandomProvider.Current;

            List<double> distribuance;
            CalculateDistribuance(currentPopulation, out distribuance);

			/*Stopwatch sw = new Stopwatch();

			sw.Start();*/

			Individual[] individuals = new Individual[currentPopulation.Length];

			/*Task[] tasks = new Task[individuals.Length];
			for (int i = 0; i < individuals.Length; i++)
			{
				tasks[i] = new Task((object ii) => {
					individuals[(int)ii] = SelectCurrentParent(random.NextDouble(), currentPopulation, distribuance);
                }, (object)i);
				tasks[i].Start();
			}
			foreach (Task task in tasks)
				task.Wait();*/

			Parallel.For(0, individuals.Length, i =>
			{
				individuals[i] = SelectCurrentParent(random.NextDouble(), currentPopulation, distribuance);
			});

			/*sw.Stop();
			Console.WriteLine(sw.ElapsedMilliseconds + " ms" + "\t" + sw.ElapsedTicks);
			Console.ReadLine();*/

			/*for (int i = 0; i < individuals.Length; i++)
			{
				individuals[i] = SelectCurrentParent(random.NextDouble(), currentPopulation, distribuance);
			}*/
			
			return individuals;

            /*return currentPopulation
                .Select(x => SelectCurrentParent(random.NextDouble(), currentPopulation, distribuance))
                .ToArray();*/
        }
    }
}
