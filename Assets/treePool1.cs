using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treePool1 : MonoBehaviour {

    static int numTrees = 100; // determines max number of trees
    public GameObject treePrefab;
    static GameObject[] trees;


	// Use this for initialization
	void Start () {

        trees = new GameObject[numTrees];
        for (int i = 0; i < numTrees; i++)
        {
            trees[i] = (GameObject)Instantiate(treePrefab, Vector3.zero, Quaternion.identity);
            trees[i].SetActive(false);
        }
	}

    static public GameObject getTree()
    {
        for(int i = 0; i < numTrees; i++)
        {
            if (!trees[i].activeSelf)
            {
                return trees[i];
            }
        }
        return null;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
