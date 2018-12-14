using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTower : MonoBehaviour {   
    [Header("Genetic Algorithm")]
    // the height our tower hopes to achieve
    [SerializeField] int dnaSize = 5;
    [SerializeField] int targetHeight = 100;
    //population size
    [SerializeField] int populationSize = 50;
    [SerializeField] float mutationRate = 0.01f;
    [SerializeField] int elitism = 5;

    [Header("Other")]
    [SerializeField] GameObject buildingBlock1;
    // [SerializeField] int buildingBlock1Count = 0;
    [SerializeField] GameObject buildingBlock2;
    // [SerializeField] int buildingBlock2Count = 0;
    [SerializeField] GameObject buildingBlock3;
    // [SerializeField] int buildingBlock3Count = 0;

    // //create a data structure to store how the pieces were placed

     private GeneticAlgorithm<BlockValue> ga;
     private System.Random random;
    //  private int timeThreshold = 5;

    List<Block> blockList = new List<Block>();
     public bool isNewSimulation = true;
     public int populationIndex = 0;
    public float simulationTime = 5.0f;
    public bool isSimulationFinished = false;
    public bool isSimulationinProgress = false;
    DNA<BlockValue> best = null;
    
	// Use this for initialization
	void Start () {
        //random = new System.Random();
        // places each block randomly
        // int bb1c = buildingBlock1Count;
        // int bb2c = buildingBlock2Count;
        // int bb3c = buildingBlock3Count;

        // // run as long as we still have materials to use
        // while( bb1c > 0 || bb2c > 0 || bb3c > 0)
        // {
        //     if(bb1c > 0)
        //     {
        //         bool tryAgain = true;
        //         int xpos = 0, ypos = 0, zpos = 0, xrot = 0, yrot = 0, zrot = 0;
        //         GameObject obj = null; 
        //         while( tryAgain )
        //         {
        //             // place at random postions, with random rotations
        //             // make sure its ontop of earthquake platform
        //             xpos = Mathf.RoundToInt(Random.Range(this.transform.localScale.x / 2 * -1 + 3, this.transform.localScale.x / 2 - 3));
        //             ypos = Mathf.RoundToInt(Random.Range(this.transform.localScale.y / 2 + 3, targetHeight + 5));
        //             zpos = Mathf.RoundToInt(Random.Range(this.transform.localScale.z / 2 * -1 + 3, this.transform.localScale.z / 2 - 3));
        //             // IMPORTANT: maybe we shouldn't randomize rotations. Makes it much harder. 
        //             xrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));
        //             yrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));
        //             zrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));
        //             obj = Instantiate(buildingBlock1, new Vector3(xpos, ypos, zpos), Quaternion.Euler(xrot, yrot, zrot));
        //             // check if new positioning is overlapping with any other block. If so, try to place it again. 
        //             if (!obj.GetComponent<CollisionDetection>().isColliding)
        //             {
        //                 tryAgain = false;
        //             }
        //         }
        //         bb1c--;
        //         blockList.Add(new Block("buildingBlock1", obj, xpos, ypos, zpos, xrot, yrot, zrot));
        //     }
        //     if (bb2c > 0)
        //     {
        //         bool tryAgain = true;
        //         int xpos = 0, ypos = 0, zpos = 0, xrot = 0, yrot = 0, zrot = 0;
        //         GameObject obj = null;
        //         while (tryAgain)
        //         {
        //             // place at random postions, with random rotations
        //             // make sure its ontop of earthquake platform
        //             xpos = Mathf.RoundToInt(Random.Range(this.transform.localScale.x / 2 * -1 + 3, this.transform.localScale.x / 2 - 3));
        //             ypos = Mathf.RoundToInt(Random.Range(this.transform.localScale.y / 2 + 3, targetHeight + 5));
        //             zpos = Mathf.RoundToInt(Random.Range(this.transform.localScale.z / 2 * -1 + 3, this.transform.localScale.z / 2 - 3));
        //             // IMPORTANT: maybe we shouldn't randomize rotations. Makes it much harder. 
        //             xrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));
        //             yrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));
        //             zrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));
        //             obj = Instantiate(buildingBlock1, new Vector3(xpos, ypos, zpos), Quaternion.Euler(xrot, yrot, zrot));
        //             if (!obj.GetComponent<CollisionDetection>().isColliding)
        //             {
        //                 tryAgain = false;
        //             }
        //         }
        //         bb2c--;
        //         blockList.Add(new Block("buildingBlock2", obj, xpos, ypos, zpos, xrot, yrot, zrot));
        //     }
        //     if (bb3c > 0)
        //     {
        //         bool tryAgain = true;
        //         int xpos = 0, ypos = 0, zpos = 0, xrot = 0, yrot = 0, zrot = 0;
        //         GameObject obj = null;
        //         while (tryAgain)
        //         {
        //             // place at random postions, with random rotations
        //             // make sure its ontop of earthquake platform
        //             xpos = Mathf.RoundToInt(Random.Range(this.transform.localScale.x / 2 * -1 + 3, this.transform.localScale.x / 2 - 3));
        //             ypos = Mathf.RoundToInt(Random.Range(this.transform.localScale.y / 2 + 3, targetHeight + 5));
        //             zpos = Mathf.RoundToInt(Random.Range(this.transform.localScale.z / 2 * -1 + 3, this.transform.localScale.z / 2 - 3));
        //             // IMPORTANT: maybe we shouldn't randomize rotations. Makes it much harder. 
        //             xrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));
        //             yrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));
        //             zrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));
        //             obj = Instantiate(buildingBlock1, new Vector3(xpos, ypos, zpos), Quaternion.Euler(xrot, yrot, zrot));
        //             if (!obj.GetComponent<CollisionDetection>().isColliding)
        //             {
        //                 tryAgain = false;
        //             }
        //         }
        //         bb3c--;
        //         blockList.Add(new Block("buildingBlock3", obj, xpos, ypos, zpos, xrot, yrot, zrot));
        //     }
        // }

        random = new System.Random();
        ga = new GeneticAlgorithm<BlockValue>(populationSize,dnaSize, random, getRandomBlockValue, elitism, mutationRate);

        // // print spawn locations/rotations of each block
        // foreach( Block b in blockList) {
        //     Debug.Log(b.toString());
        // }
	}
	
	// Update is called once per frame
	void Update () {
        // Debug.Log(simulationTime);

        //The start of a CaculateFitness
        if(isNewSimulation){
            best = ga.Population[0];
            ga.fitnessSum = 0;
            populationIndex = 0;
            isNewSimulation = false;
            isSimulationFinished = false;
            isSimulationinProgress = true;
            createBlocks(populationIndex); //Create the blocks for the scene
            simulationTime = 5.0f;
        }

        //At the end of the current index and the start of the new index
        if(simulationTime < 0 && isSimulationinProgress){
            Debug.Log(populationIndex);
            float currentFitnessSum = FitnessFunction(blockList);
            destroyBlocks();
            blockList = new List<Block>();
            ga.fitnessSum += currentFitnessSum;
            if(currentFitnessSum > best.Fitness){
                best = ga.Population[populationIndex];
            }
            populationIndex++;
            if(populationIndex >= ga.Population.Count){
                isSimulationinProgress = false;
                isSimulationFinished = true;
            }else{
                createBlocks(populationIndex);
                simulationTime = 5.0f;
            }
        }

        if(isSimulationFinished){
            ga.BestFitness = best.Fitness;
            ga.BestGenes = best.Genes;
            ga.NewGeneration();
            isNewSimulation = true;
        }

        //Timer for 10 seconds
        simulationTime -= Time.deltaTime;
	}

    // spawns the blocks in the blockList into the scene
    public void CreateTower(List<Block> blockList)
    {
        foreach (Block b in blockList)
        {
            if(b.blockType == "buildingBlock1")
            {
                Instantiate(buildingBlock1, new Vector3(b.xpos, b.ypos, b.zpos), Quaternion.Euler(b.xrot, b.yrot, b.zrot));
            } else if (b.blockType == "buildingBlock2")
            {
                Instantiate(buildingBlock2, new Vector3(b.xpos, b.ypos, b.zpos), Quaternion.Euler(b.xrot, b.yrot, b.zrot));
            } else if (b.blockType == "buildingBlock3")
            {
                Instantiate(buildingBlock3, new Vector3(b.xpos, b.ypos, b.zpos), Quaternion.Euler(b.xrot, b.yrot, b.zrot));
            }
        }
    }

    private BlockValue getRandomBlockValue(){
        BlockValue returnValue = null;

        int xpos, ypos, zpos, xrot, yrot, zrot;
        // place at random postions, with random rotations
        // make sure its ontop of earthquake platform
        xpos = Mathf.RoundToInt(Random.Range(this.transform.localScale.x / 2 * -1 + 3, this.transform.localScale.x / 2 - 3));
        ypos = Mathf.RoundToInt(Random.Range(this.transform.localScale.y / 2 + 3, targetHeight + 25));
        zpos = Mathf.RoundToInt(Random.Range(this.transform.localScale.z / 2 * -1 + 3, this.transform.localScale.z / 2 - 3));
        // IMPORTANT: maybe we shouldn't randomize rotations. Makes it much harder. 
        xrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));
        yrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));
        zrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));

        if(targetHeight == 0){
            // Debug.Log("HELLO WORLD");
        }

        returnValue = new BlockValue(buildingBlock1, xpos, ypos, zpos, xrot, yrot, zrot);
        return returnValue;
    }

    // //function: FitnessFunction(int index)
    // //input: int index - the index of blockList in ga Population
    // //output: float score - a fitness score of the current blockList which the sum of the heights
    // private float FitnessFunction(int index){
    //     float score = 0;
    //     DNA<BlockValue> dna = ga.Population[index];
    //     List<Block> blockList = new List<Block>();

    //     Debug.Log("Original YPOS");
    //     foreach(BlockValue blockValue in dna.Genes){
    //         GameObject obj = Instantiate(buildingBlock1, new Vector3(blockValue.xpos, blockValue.ypos, blockValue.zpos), Quaternion.Euler(blockValue.xrot, blockValue.yrot, blockValue.zrot));
    //         blockList.Add(new Block("buildingBlock1", obj, blockValue.xpos, blockValue.ypos, blockValue.zpos, blockValue.xrot, blockValue.yrot, blockValue.zrot));
    //         Debug.Log(blockValue.ypos);
    //     }
    //     // int timeLeft = 0;
    //     // while(timeLeft < 150){
    //     //     // Debug.Log(timeLeft);
    //     //     Update();
    //     //     timeLeft++;
    //     // }

    //         Debug.Log("FINAL YPOS");
    //         foreach(Block block in blockList){ 
    //         Debug.Log(block.obj.transform.position.y);
    //         // Destroy(block.obj);
    //     }

    //     // this.enabled = false;

    //     return score;
    // }

    public void createBlocks(int index){
        DNA<BlockValue> dna = ga.Population[index];
        foreach(BlockValue blockValue in dna.Genes){
            GameObject obj = Instantiate(buildingBlock1, new Vector3(blockValue.xpos, blockValue.ypos, blockValue.zpos), Quaternion.Euler(blockValue.xrot, blockValue.yrot, blockValue.zrot));
            blockList.Add(new Block("buildingBlock1", obj, blockValue.xpos, blockValue.ypos, blockValue.zpos, blockValue.xrot, blockValue.yrot, blockValue.zrot));
        }
    }

    public void destroyBlocks(){
        foreach(Block block in blockList){
            Destroy(block.obj);
        }
    }


    private float FitnessFunction(List<Block> blockList){
        float score = 0;
        foreach(Block block in blockList){
            score += block.obj.transform.position.y;
        }

        if(score > (3.5*dnaSize)){
            score = score*2;
        }

        return score;
    }

}

public class Block
{
    // name of prefab
    public string blockType;
    // reference to game object
    public GameObject obj;
    // the positions of the block
    public int xpos;
    public int ypos;
    public int zpos;
    // rotations of block
    public int xrot;
    public int yrot;
    public int zrot;

    public Block(string blockType, GameObject obj, int xpos, int ypos, int zpos, int xrot, int yrot, int zrot)
    {
        this.blockType = blockType;
        this.obj = obj;
        this.xpos = xpos;
        this.ypos = ypos;
        this.zpos = zpos;
        this.xrot = xrot;
        this.yrot = yrot;
        this.zrot = zrot;
    }

    public string toString()
    {
        return "blockType:" + blockType + " xpos:" + xpos + " ypos:" + ypos + " zpos:" + zpos + " xrot:" + xrot + " yrot:" + yrot + " zrot:" + zrot;
    }

}

public class BlockValue{
    public object blockType;
    public int xpos;
    public int ypos;
    public int zpos;
    public int xrot;
    public int yrot;
    public int zrot;

    public BlockValue(object blockType, int xpos, int ypos, int zpos, int xrot, int yrot, int zrot)
    {
        this.blockType = blockType;
        this.xpos = xpos;
        this.zpos = zpos;
        this.xrot = xrot;
        this.yrot = yrot;
        this.zrot = zrot;
    }
}
