using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Trees : MonoBehaviour
{
    public GameObject seed;
    public int population = 10; // number of trees to plant
    
    float range = 13.0f; // in the World

    float x = 0f; //-15.25f;
    float z = 0f; //3.39f;

    void Start()
    {
        for (int i = 0; i < population; i++)
        {
            plantSeed();
        }        
    }

    void plantSeed()
    {
        //GameObject newSeed = Instantiate(seed, new Vector3(x, 0.15f, z), Quaternion.identity);
        var newTree = Instantiate(seed, new Vector3(Random.Range(-range, range) + x, 0f, Random.Range(-range, range) + z), Quaternion.identity);            
        newTree.gameObject.tag = "tree";
        newTree.transform.parent = gameObject.transform;;
    }

    private GameObject[] numberOfTrees;

    int counting()
    {
        numberOfTrees = GameObject.FindGameObjectsWithTag("tree");

        return numberOfTrees.Length;
    }

    void Update()
    {
        if ( counting() < (population / 2) )
        {
            plantSeed();
        }
    }
}
