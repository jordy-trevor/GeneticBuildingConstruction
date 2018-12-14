using System;
using System.Collections.Generic;

public class GeneticAlgorithm<Block>
{
	public List<DNA<Block>> Population { get; private set; }
	public int Generation { get; private set; }
	public float BestFitness { get; private set; }
	public Block[] BestGenes { get; private set; }

	public int Elitism;
	public float MutationRate;
	
	private List<DNA<Block>> newPopulation;
	private Random random;
	private float fitnessSum;
	private int dnaSize;
	private Func<Block> getRandomGene;
	private Func<int, float> fitnessFunction;

	public GeneticAlgorithm(int populationSize, int dnaSize, Random random, Func<Block> getRandomGene, Func<int, float> fitnessFunction,
		int elitism, float mutationRate = 0.01f)
	{
		Generation = 1;
		Elitism = elitism;
		MutationRate = mutationRate;
		Population = new List<DNA<Block>>(populationSize);
		newPopulation = new List<DNA<Block>>(populationSize);
		this.random = random;
		this.dnaSize = dnaSize;
		this.getRandomGene = getRandomGene;
		this.fitnessFunction = fitnessFunction;

		BestGenes = new Block[dnaSize];

		for (int i = 0; i < populationSize; i++)
		{
			Population.Add(new DNA<Block>(dnaSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));
		}
	}

	public void NewGeneration(int numNewDNA = 0, bool crossoverNewDNA = false)
	{
		int finalCount = Population.Count + numNewDNA;

		if (finalCount <= 0) {
			return;
		}

		if (Population.Count > 0) {
			CalculateFitness();
			Population.Sort(CompareDNA);
		}
		newPopulation.Clear();

		for (int i = 0; i < Population.Count; i++)
		{
			if (i < Elitism && i < Population.Count)
			{
				newPopulation.Add(Population[i]);
			}
			else if (i < Population.Count || crossoverNewDNA)
			{
				DNA<Block> parent1 = ChooseParent();
				DNA<Block> parent2 = ChooseParent();

				DNA<Block> child = parent1.Crossover(parent2);

				child.Mutate(MutationRate);

				newPopulation.Add(child);
			}
			else
			{
				newPopulation.Add(new DNA<Block>(dnaSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));
			}
		}

		List<DNA<Block>> tmpList = Population;
		Population = newPopulation;
		newPopulation = tmpList;

		Generation++;
	}
	
	private int CompareDNA(DNA<Block> a, DNA<Block> b)
	{
		if (a.Fitness > b.Fitness) {
			return -1;
		} else if (a.Fitness < b.Fitness) {
			return 1;
		} else {
			return 0;
		}
	}

	private void CalculateFitness()
	{
		fitnessSum = 0;
		DNA<Block> best = Population[0];

		for (int i = 0; i < Population.Count; i++)
		{
			fitnessSum += Population[i].CalculateFitness(i);

			if (Population[i].Fitness > best.Fitness)
			{
				best = Population[i];
			}
		}

		BestFitness = best.Fitness;
		best.Genes.CopyTo(BestGenes, 0);
	}

	private DNA<Block> ChooseParent()
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