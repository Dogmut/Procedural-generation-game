using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrain_Generation : MonoBehaviour {

    int heightScale = 15;
    float detailScale = 40.0f;
    List<GameObject> trees1 = new List<GameObject>();

	// Use this for initialization
	void Start () {

        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        for (int v = 0; v < vertices.Length; v++)
        {
            vertices[v].y = Mathf.PerlinNoise((vertices[v].x + this.transform.position.x) / detailScale,
                                                (vertices[v].z + this.transform.position.z) / detailScale) * heightScale;

            if (vertices[v].y > 5.5 && Mathf.PerlinNoise((vertices[v].x+5)/10, (vertices[v].z+5)/10)*10 > 4.6)
            {
                GameObject newTree1 = treePool1.getTree();
                if (newTree1 != null)
                {
                    Vector3 tree1Pos = new Vector3(vertices[v].x + this.transform.position.x,
                                                   vertices[v].y,
                                                   vertices[v].z + this.transform.position.z);
                    newTree1.transform.position = tree1Pos;
                    newTree1.SetActive(true);
                    trees1.Add(newTree1);
                }
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        this.gameObject.AddComponent<MeshCollider>();
	}

    void OnDestroy()
    {
        for (int i = 0; i < trees1.Count; i++)
        {
            if (trees1[i] != null)
                trees1[i].SetActive(false);
        }
        trees1.Clear();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
