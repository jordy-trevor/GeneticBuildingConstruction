using System;
using System.Collections.Generic;

public class DNA<T>
{
	public List<int>[] Genes { get; private set; }
	public float Fitness { get; private set; }

	private Random random;
	private Func<List<int>> getRandomGene;
	private Func<int, float> fitnessFunction;

	public DNA(int size, Random random, Func<List<int>> getRandomGene, Func<int, float> fitnessFunction, bool shouldInitGenes = true)
	{
		Genes = new List<int> [size];
		this.random = random;
		this.getRandomGene = getRandomGene;
		this.fitnessFunction = fitnessFunction;

		if (shouldInitGenes)
		{
			for (int i = 0; i < Genes.Length; i++)
			{
				Genes[i] = getRandomGene();
			}
		}
	}

	public float CalculateFitness(int index)
	{
		Fitness = fitnessFunction(index);
		return Fitness;
	}

	public DNA<T> Crossover(DNA<T> otherParent)
	{
		DNA<T> child = new DNA<T>(Genes.Length, random, getRandomGene, fitnessFunction, shouldInitGenes: false);

		for (int i = 0; i < Genes.Length; i++)
		{
			for( int j = 0 ; j < 3; j++)
			{
				child.Genes[i][j] = random.NextDouble() < 0.5 ? Genes[i][j] : otherParent.Genes[i][j];
			}
			
		}

		return child;
	}

	public void Mutate(float mutationRate)
	{
		for (int i = 0; i < Genes.Length; i++)
		{
			if (random.NextDouble() < mutationRate)
			{
				Genes[i] = getRandomGene();
			}
		}
	}
}