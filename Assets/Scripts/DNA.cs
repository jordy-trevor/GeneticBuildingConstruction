using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA<Block>
{
	public List<Block> Genes { get; private set; }
	public float Fitness { get; private set; }
	public int geneSize;

	private System.Random random;
	private Func<Block> getRandomGene;
	private Func<int, float> fitnessFunction;

	public DNA(int size, System.Random random, Func<Block> getRandomGene, Func<int, float> fitnessFunction, bool shouldInitGenes = true)
	{
		Genes = new List<Block>();
		this.random = random;
		this.getRandomGene = getRandomGene;
		this.fitnessFunction = fitnessFunction;
		this.geneSize = size;

		if (shouldInitGenes)
		{
			for (int i = 0; i < geneSize; i++)
			{
				Genes.Add(getRandomGene());
			}
		}
	}

	public float CalculateFitness(int index)
	{
		Fitness = fitnessFunction(index);
		return Fitness;
	}

	public DNA<Block> Crossover(DNA<Block> otherParent)
	{
		DNA<Block> child = new DNA<Block>(geneSize, random, getRandomGene, fitnessFunction, shouldInitGenes: false);

		for (int i = 0; i < geneSize; i++)
		{
			child.Genes[i] = random.NextDouble() < 0.5 ? Genes[i] : otherParent.Genes[i];
		}

		return child;
	}

	public void Mutate(float mutationRate)
	{
		for (int i = 0; i < geneSize; i++)
		{
			if (random.NextDouble() < mutationRate)
			{
				Genes[i] = getRandomGene();
			}
		}
	}
}