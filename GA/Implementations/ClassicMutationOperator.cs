using GA.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BasicTypes;
using GA.Helpers;

namespace GA.Implementations
{
    public class ClassicMutationOperator : IMutationOperator
    {
        public void Mutation(Individual individual, double mutationProbability)
        {
			// Użycie RandomProvidera znacząco (10-krotnie) spowalnia wykonanie funkcji
			//var random = RandomProvider.Current;
			var random = new Random();

			// Zamiana tej pętli na Parallel.For() spowalnia program - jedna operacja crossover na całym pokoleniu (przy reszcie sekwencyjnej) spowalnia jej wykonanie do ~40 ms
			for (int i = 0; i < individual.Chromosome.Size; ++i)
			{
				if (random.NextDouble() <= mutationProbability)
				{
					individual.Chromosome[i] = !individual.Chromosome[i];
				}
			}

			// O szybkości LINQ nawet nie wspominam
			/*var randomNumbers = individual
                .Chromosome.Genes
                .Select(x => random.NextDouble())
                .ToArray();

            individual.ReplaceGenes(individual
                .Chromosome.Genes
                .Zip(randomNumbers, (gene, prob) => prob <= mutationProbability ? !gene : gene)
                .ToArray());*/
		}
	}
}
