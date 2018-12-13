using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTower : MonoBehaviour {   
    [Header("Genetic Algorithm")]
    // the height our tower hopes to achieve
    [SerializeField] int targetHeight = 10;
    //population size
    // [SerializeField] int populationSize = 200;
    // [SerializeField] float mutationRate = 0.01f;
    // [SerializeField] int elitism = 5;

    [Header("Other")]
    [SerializeField] GameObject buildingBlock1;
    [SerializeField] int buildingBlock1Count = 0;
    [SerializeField] GameObject buildingBlock2;
    [SerializeField] int buildingBlock2Count = 0;
    [SerializeField] GameObject buildingBlock3;
    [SerializeField] int buildingBlock3Count = 0;

    //private GeneticAlgorithm<char> ga;
    //private System.Random random;
    //create a data structure to store how the pieces were placed

    
	// Use this for initialization
	void Start () {
        //random = new System.Random();
        // places each block randomly
        int bb1c = buildingBlock1Count;
        int bb2c = buildingBlock2Count;
        int bb3c = buildingBlock3Count;

        // run as long as we still have materials to use
        while( bb1c > 0 || bb2c > 0 || bb3c > 0)
        {
            if(bb1c > 0)
            {
                // place at random postions, with random rotations
                // make sure its ontop of earthquake platform
                int xpos = Mathf.RoundToInt(Random.Range(this.transform.localScale.x / 2 * -1 + 3, this.transform.localScale.x/2 - 3)); 
                int ypos = Mathf.RoundToInt(Random.Range(0.0f, targetHeight));
                int zpos = Mathf.RoundToInt(Random.Range(this.transform.localScale.z / 2 * -1 + 3, this.transform.localScale.z/2 - 3));
                int xrot = Mathf.RoundToInt(Random.Range(0.0f, 360.0f));
                int yrot = Mathf.RoundToInt(Random.Range(0.0f, 360.0f));
                int zrot = Mathf.RoundToInt(Random.Range(0.0f, 360.0f));
                Instantiate(buildingBlock1, new Vector3(xpos, ypos, zpos), Quaternion.Euler(xrot, yrot, zrot));
                Debug.Log("xpos: " + xpos + " ypos: " + ypos + " zpos: " + zpos);
                bb1c--;
            }
            if (bb2c > 0)
            {
                // place at random postions, with random rotations
                int xpos = Mathf.RoundToInt(Random.Range(this.transform.localScale.x / 2 * -1 + 3, this.transform.localScale.x/2 -3));
                int ypos = Mathf.RoundToInt(Random.Range(0.0f, targetHeight));
                int zpos = Mathf.RoundToInt(Random.Range(this.transform.localScale.z / 2 * -1 + 3, this.transform.localScale.z/2 -3));
                int xrot = Mathf.RoundToInt(Random.Range(0.0f, 360.0f));
                int yrot = Mathf.RoundToInt(Random.Range(0.0f, 360.0f));
                int zrot = Mathf.RoundToInt(Random.Range(0.0f, 360.0f));
                Instantiate(buildingBlock2, new Vector3(xpos, ypos, zpos), Quaternion.Euler(xrot, yrot, zrot));
                Debug.Log("xpos: " + xpos + " ypos: " + ypos + " zpos: " + zpos);
                bb2c--;
            }
            if (bb3c > 0)
            {
                // place at random postions, with random rotations
                int xpos = Mathf.RoundToInt(Random.Range(this.transform.localScale.x / 2 * -1 + 3, this.transform.localScale.x/2 - 3));
                int ypos = Mathf.RoundToInt(Random.Range(0.0f, targetHeight));
                int zpos = Mathf.RoundToInt(Random.Range(this.transform.localScale.z / 2 * -1 + 3, this.transform.localScale.z/2 - 3));
                int xrot = Mathf.RoundToInt(Random.Range(0.0f, 360.0f));
                int yrot = Mathf.RoundToInt(Random.Range(0.0f, 360.0f));
                int zrot = Mathf.RoundToInt(Random.Range(0.0f, 360.0f));
                Instantiate(buildingBlock3, new Vector3(xpos, ypos, zpos), Quaternion.Euler(xrot, yrot, zrot));
                Debug.Log("xpos: " + xpos + " ypos: " + ypos + " zpos: " + zpos);
                bb3c--;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
