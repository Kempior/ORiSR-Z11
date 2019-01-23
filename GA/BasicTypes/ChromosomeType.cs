using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GA.Extensions;
using GA.Helpers;
using GA.Abstracts;

namespace GA.BasicTypes
{
    public class ChromosomeType : ICloneable<ChromosomeType>
    {
        public int Size { get { return Genes.Length; } }

        public bool this[int index]
        {
            get { return Genes[index]; }
            set { Genes[index] = value; }
        }

        public bool[] Genes { get; set; }
        public double DecodedValue { get { return GetDecodedValue(); } }

        public ChromosomeType(int chromosomeSize)
        {
            var random = RandomProvider.Current;

            Genes = new bool[chromosomeSize];
            for (int i = 0; i < Genes.Length; i++)
            {
                Genes[i] = random.NextDouble() < 0.5;
            }
        }

        private double GetDecodedValue()
        {
			double sum = 0;
			for (int i = Size - 1; i >= 0; --i)
			{
				sum += Genes[i] ? (1 << i) : 0;
			}

			return sum;

            /*return Genes
                .Reverse()
                .Select((x, i) => (x ? Math.Pow(2, i) : 0))
                .Sum();*/
        }

        public ChromosomeType Clone()
        {
            var result = new ChromosomeType(Size);

			Array.Copy(this.Genes, result.Genes, Size);

            /*result.Genes = Genes
                .Select(x => x)
                .ToArray();*/

            return result;
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }

}
