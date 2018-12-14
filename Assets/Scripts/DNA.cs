using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA<BlockValue>
{
	public List<BlockValue> Genes { get; private set; }
	public float Fitness;
	public int geneSize;

	private System.Random random;
	private Func<BlockValue> getRandomGene;
	// private Func<int, float> fitnessFunction;

	public DNA(int size, System.Random random, Func<BlockValue> getRandomGene, bool shouldInitGenes = true)
	{
		Genes = new List<BlockValue>();
		this.random = random;
		this.getRandomGene = getRandomGene;
		this.geneSize = size;

		if (shouldInitGenes)
		{
			for (int i = 0; i < geneSize; i++)
			{
				Genes.Add(getRandomGene());
			}
		}
	}

	// public float CalculateFitness(int index)
	// {
	// 	Fitness = fitnessFunction(index);
	// 	return Fitness;
	// }

	public DNA<BlockValue> Crossover(DNA<BlockValue> otherParent)
	{
		DNA<BlockValue> child = new DNA<BlockValue>(geneSize, random, getRandomGene, shouldInitGenes: false);

		for (int i = 0; i < geneSize; i++)
		{
			if(random.NextDouble() < 0.5){
				child.Genes.Add(this.Genes[i]);
			}else{
				child.Genes.Add(otherParent.Genes[i]);
			}
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