using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTower : MonoBehaviour {   
    [Header("Genetic Algorithm")]
    // the height our tower hopes to achieve
    [SerializeField] int targetHeight = 10;
    //population size
    //[SerializeField] int populationSize = 200;
    //[SerializeField] float mutationRate = 0.01f;
    //[SerializeField] int elitism = 5;

    [Header("Other")]
    [SerializeField] GameObject buildingBlock1;
    [SerializeField] int buildingBlock1Count = 0;
    [SerializeField] GameObject buildingBlock2;
    [SerializeField] int buildingBlock2Count = 0;
    [SerializeField] GameObject buildingBlock3;
    [SerializeField] int buildingBlock3Count = 0;

    public int bb1c, bb2c, bb3c; // changable counts
    public bool finishedSpawning = false;

    //create a data structure to store how the pieces were placed
    List<Block> blockList = new List<Block>();

    // spawn the random tower once, using update
    //bool initialTowerSpawned = false;

    //  private System.Random random;
     private GeneticAlgorithm<Block> ga;
     private System.Random random;
     private int timeThreshold = 10;

    
	// Use this for initialization
	void Start () {
        // places each block randomly
        bb1c = buildingBlock1Count;
        bb2c = buildingBlock2Count;
        bb3c = buildingBlock3Count;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(FitnessFunction(blockList).ToString());
        Debug.Log(FitnessFunction(blockList).ToString());
        if (Time.time > timeThreshold)
        {
            Debug.Log(Time.time);
            ga.NewGeneration();
            // if(ga.BestFitness == )

            timeThreshold += 10;
        }
        //IMPORTANT: this is just for testing purposes. Remove once not needed
        if (Input.GetKeyDown("space"))
        {
            CreateTower(blockList);
        }

        //IMPORTANT: this is just for testing purposes. Remove once not needed
        if (Input.GetKeyDown("space"))
        {
            CreateTower(blockList);
        }

        // random spawn here
        if (bb1c > 0)
        {
            int xpos = 0, ypos = 0, zpos = 0, xrot = 0, yrot = 0, zrot = 0;
            GameObject obj = null;
            // place at random postions, with random rotations
            // make sure its ontop of earthquake platform
            xpos = Mathf.RoundToInt(Random.Range(this.transform.localScale.x / 2 * -1 + 3, this.transform.localScale.x / 2 - 3));
            ypos = Mathf.RoundToInt(Random.Range(this.transform.localScale.y / 2 + 3, targetHeight + 5));
            zpos = Mathf.RoundToInt(Random.Range(this.transform.localScale.z / 2 * -1 + 3, this.transform.localScale.z / 2 - 3));
            // IMPORTANT: maybe we shouldn't randomize rotations. Makes it much harder. 
            xrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));
            yrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));
            zrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));
            obj = Instantiate(buildingBlock1, new Vector3(xpos, ypos, zpos), Quaternion.Euler(xrot, yrot, zrot));
            bb1c--;
            blockList.Add(new Block("buildingBlock1", obj, xpos, ypos, zpos, xrot, yrot, zrot));

        }
        else if (bb2c > 0)
        {
            int xpos = 0, ypos = 0, zpos = 0, xrot = 0, yrot = 0, zrot = 0;
            GameObject obj = null;

            // place at random postions, with random rotations
            // make sure its ontop of earthquake platform
            xpos = Mathf.RoundToInt(Random.Range(this.transform.localScale.x / 2 * -1 + 3, this.transform.localScale.x / 2 - 3));
            ypos = Mathf.RoundToInt(Random.Range(this.transform.localScale.y / 2 + 3, targetHeight + 5));
            zpos = Mathf.RoundToInt(Random.Range(this.transform.localScale.z / 2 * -1 + 3, this.transform.localScale.z / 2 - 3));
            // IMPORTANT: maybe we shouldn't randomize rotations. Makes it much harder. 
            xrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));
            yrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));
            zrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));
            obj = Instantiate(buildingBlock1, new Vector3(xpos, ypos, zpos), Quaternion.Euler(xrot, yrot, zrot));
            bb2c--;
            blockList.Add(new Block("buildingBlock2", obj, xpos, ypos, zpos, xrot, yrot, zrot));
        }
        else if (bb3c > 0)
        {
            int xpos = 0, ypos = 0, zpos = 0, xrot = 0, yrot = 0, zrot = 0;
            GameObject obj = null;

            // place at random postions, with random rotations
            // make sure its ontop of earthquake platform
            xpos = Mathf.RoundToInt(Random.Range(this.transform.localScale.x / 2 * -1 + 3, this.transform.localScale.x / 2 - 3));
            ypos = Mathf.RoundToInt(Random.Range(this.transform.localScale.y / 2 + 3, targetHeight + 5));
            zpos = Mathf.RoundToInt(Random.Range(this.transform.localScale.z / 2 * -1 + 3, this.transform.localScale.z / 2 - 3));
            // IMPORTANT: maybe we shouldn't randomize rotations. Makes it much harder. 
            xrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));
            yrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));
            zrot = Mathf.RoundToInt(Random.Range(0.0f, 0.0f));
            obj = Instantiate(buildingBlock1, new Vector3(xpos, ypos, zpos), Quaternion.Euler(xrot, yrot, zrot));

            bb3c--;
            blockList.Add(new Block("buildingBlock3", obj, xpos, ypos, zpos, xrot, yrot, zrot));
        }
        else if (!finishedSpawning)
        {
            
            int count = 0;
            List<Block> tmpbl = new List<Block>(blockList);
            foreach (Block b in tmpbl)
            {
                
                if(b.obj.GetComponent<CollisionDetection>().isColliding)
                {
                    
                    blockList.Remove(b);
                    Destroy(b.obj); 
                    if(b.blockType == "buildingBlock1")
                    {
                        bb1c++;
                    } else if (b.blockType == "buildingBlock2")
                    {
                        bb2c++;
                    } else if (b.blockType == "buildingBlock3")
                    {
                        bb3c++;
                    }
                    count++;
                } 
            }
            if(count == 0)
            {
                // if none of them are colliding
                // unfreeze all 
                Debug.Log("unfreeze time");
                finishedSpawning = true;
                foreach(Block b in blockList)
                {
                    Debug.Log(b.toString());
                    b.obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    b.obj.GetComponent<Rigidbody>().isKinematic = false;
                    b.obj.GetComponent<Rigidbody>().useGravity = true;
                }
            }
        } 

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

    private Block getRandomPos(){
        Block randomBlock = null;
        // List<Block> randomBlockList = null;
        // return randomBlockList;
        return randomBlock;
    }

    //function: FitnessFunction(int index)
    //input: int index - the index of blockList in ga Population
    //output: float score - a fitness score of the current blockList which the sum of the heights
    private float FitnessFunction(int index){
        float score = 0;
        DNA<Block> dna = ga.Population[index];

        foreach(Block block in dna.Genes){
            score += block.obj.transform.position.y;
        }
        return score;
    }


    //function: FitnessFunction(List<Block> blockList)
    //input: List<Block> blockList - a list of blocks
    //output: float score - a fitness score of the current blockList which the sum of the heights
    private float FitnessFunction(List<Block> blockList){
        float score = 0;
        foreach(Block block in blockList){
            score += block.obj.transform.position.y;
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

