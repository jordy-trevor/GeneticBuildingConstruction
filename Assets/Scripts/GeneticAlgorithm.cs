using System;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm<BlockValue>
{
	public List<DNA<BlockValue>> Population { get; private set; }
	public int Generation { get; private set; }
	public float BestFitness;
	public List<BlockValue> BestGenes;

	public int Elitism;
	public float MutationRate;
	
	private List<DNA<BlockValue>> newPopulation;
	private System.Random random;
	public float fitnessSum;
	private int dnaSize;
	private Func<BlockValue> getRandomGene;
	// private Func<int, float> fitnessFunction;

	public GeneticAlgorithm(int populationSize, int dnaSize, System.Random random, Func<BlockValue> getRandomGene, int elitism, float mutationRate = 0.01f)
	{
		Generation = 1;
		Elitism = elitism;
		MutationRate = mutationRate;
		Population = new List<DNA<BlockValue>>(populationSize);
		newPopulation = new List<DNA<BlockValue>>(populationSize);
		this.random = random;
		this.dnaSize = dnaSize;
		this.getRandomGene = getRandomGene;
		// this.fitnessFunction = fitnessFunction;

		BestGenes = new List<BlockValue>();
		for (int i = 0; i < populationSize; i++)
		{
			Population.Add(new DNA<BlockValue>(dnaSize, random, getRandomGene, shouldInitGenes: true));
		}
	}

	public void NewGeneration(int numNewDNA = 0, bool crossoverNewDNA = false)
	{		
		Population.Sort(CompareDNA);

		newPopulation.Clear();

		for (int i = 0; i < Population.Count; i++)
		{
			if (i < Elitism && i < Population.Count)
			{
				newPopulation.Add(Population[i]);
			}
			else if (i < Population.Count || crossoverNewDNA)
			{
				DNA<BlockValue> parent1 = ChooseParent();
				DNA<BlockValue> parent2 = ChooseParent();
				DNA<BlockValue> child;

				if(parent1 == null && parent2 == null){
					child = new DNA<BlockValue>(dnaSize, random, getRandomGene, shouldInitGenes: true);
				}else if(parent1 == null){
					child = parent2;
				}else if(parent2 == null){
					child = parent1;
				}else{
					child = parent1.Crossover(parent2);
				}

				child.Mutate(MutationRate);

				newPopulation.Add(child);
			}
			else
			{
				newPopulation.Add(new DNA<BlockValue>(dnaSize, random, getRandomGene, shouldInitGenes: true));
			}
		}

		List<DNA<BlockValue>> tmpList = Population;
		Population = newPopulation;
		newPopulation = tmpList;

		Generation++;
	}
	
	private int CompareDNA(DNA<BlockValue> a, DNA<BlockValue> b)
	{
		if (a.Fitness > b.Fitness) {
			return -1;
		} else if (a.Fitness < b.Fitness) {
			return 1;
		} else {
			return 0;
		}
	}

	private DNA<BlockValue> ChooseParent()
	{
		double randomNumber = random.NextDouble() * fitnessSum;

		for (int i = 0; i < Population.Count; i++)
		{
			if (randomNumber < Population[i].Fitness)
			{
				return Population[i];
			}

			randomNumber -= Population[i].Fitness;
		}
		return null;
	}
}