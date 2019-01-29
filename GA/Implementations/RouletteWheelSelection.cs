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
			double sumOfFitness = 0;
			for (int i = 0; i < currentPopulation.Length; ++i)
			{
				sumOfFitness += currentPopulation[i].Fitness;
			}

			double cummulativeFitness = 0;

			distribuance = new List<double>();
			for (int i = 0; i < currentPopulation.Length; ++i)
			{
				cummulativeFitness += currentPopulation[i].Fitness / sumOfFitness;
				distribuance.Add(cummulativeFitness);
			}

			//var random = RandomProvider.Current;

			//var sumOfFitness = currentPopulation
			//	.Sum(x => x.Fitness);

			//distribuance = currentPopulation
			//	.Select(x =>
			//	{
			//		cummulativeFitness += x.Fitness / sumOfFitness;
			//		return cummulativeFitness;
			//	})
			//	.ToList();
		}

		private Individual SelectCurrentParent(double random, Individual[] currentPopulation, List<double> distribuance)
        {
			int currentIndex = 0;
			for (int i = 0; i < distribuance.Count; i++)
			{
				if (distribuance[i] > random)
				{
					currentIndex = i;
					break;
				}
			}

			//var currentIndex = distribuance
			//	.Select((x, i) => new { Index = i, Value = x })
			//	.FirstOrDefault(x => x.Value > random).Index;

			return currentPopulation[currentIndex].Clone();
		}

        public Individual[] GenerateParentPopulation(Individual[] currentPopulation)
        {
			//var random = RandomProvider.Current;
			var random = new Random();

            List<double> distribuance;
            CalculateDistribuance(currentPopulation, out distribuance);

			Individual[] individuals = new Individual[currentPopulation.Length];

			for (int i = 0; i < individuals.Length; i++)
			{
				individuals[i] = SelectCurrentParent(random.NextDouble(), currentPopulation, distribuance);
			}
			
			return individuals;

			//return currentPopulation
			//	.Select(x => SelectCurrentParent(random.NextDouble(), currentPopulation, distribuance))
			//	.ToArray();
		}
	}
}
