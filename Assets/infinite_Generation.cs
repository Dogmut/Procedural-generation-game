using System.Collections;
using UnityEngine;

class tile
{

    public GameObject theTile;
    public float creationTime;

    public tile (GameObject floor, float ct)
    {
        theTile = floor;
        creationTime = ct;
    }
}

public class infinite_Generation : MonoBehaviour {

    public GameObject plane;
    public GameObject player;

    int planeSize = 10;
    int halfTilesX = 5;
    int halfTilesZ = 5;

    Vector3 startPos;

    Hashtable tiles = new Hashtable();

    //Use this for initilization
    void Start ()
    {
        this.gameObject.transform.position = Vector3.zero;
        startPos = Vector3.zero;

        float updateTime = Time.realtimeSinceStartup;

        for (int x = -halfTilesX; x < halfTilesX; x++)
        {
            for (int z = -halfTilesZ; z < halfTilesZ; z++)
            {
                Vector3 pos = new Vector3((x * planeSize + startPos.x),
                                                0,
                                          (z * planeSize + startPos.z));
                GameObject floor = (GameObject)Instantiate(plane, pos,
                                               Quaternion.identity);

                string tileName = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();
                floor.name = tileName;
                tile tile = new tile(floor, updateTime);
                tiles.Add(tileName, tile);
                                                
            }
        }
    }

    // this updates frames around the player
    void Update()
    {
        // determine how far the player has moved since last terrain update in X and Y direction
        int xMove = (int)(player.transform.position.x - startPos.x);
        int zMove = (int)(player.transform.position.z - startPos.z);

        if(Mathf.Abs(xMove) >= planeSize || Mathf.Abs(zMove) >= planeSize) // update tiles around the player
        {
            float updateTime = Time.realtimeSinceStartup; // delete old tiles (out of range from the player) and update if back in range

            //force integer position and round to nearest tilesize
            int playerX = (int)(Mathf.Floor(player.transform.position.x / planeSize) * planeSize);
            int playerZ = (int)(Mathf.Floor(player.transform.position.z / planeSize) * planeSize);

            for(int x = -halfTilesX; x < halfTilesX; x++)
            {
                for (int z = -halfTilesZ; z < halfTilesZ; z++)
                {
                    Vector3 pos = new Vector3((x * planeSize + playerX),
                                                0,
                                                (z * planeSize + playerZ));

                    string tileName = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();

                    if (!tiles.ContainsKey(tileName))
                    {
                        GameObject t = (GameObject)Instantiate(plane, pos,
                                                    Quaternion.identity); // used to identify rotations
                        t.name = tileName;
                        tile tile = new tile(t, updateTime);
                        tiles.Add(tileName, tile);
                    }
                    else
                    {
                        (tiles[tileName] as tile).creationTime = updateTime;
                    }
                }
            }

            //destroy all tiles not just created or with time updated
            // and put new tiles to be kept in a new hashtable

            Hashtable newTerrain = new Hashtable();
            foreach (tile tls in tiles.Values)
            {
                if(tls.creationTime != updateTime)
                {
                    //destroy gameobject
                    Destroy(tls.theTile);
                }
                else
                {
                    newTerrain.Add(tls.theTile.name, tls);
                }
            }
            //copy new hashtable contents to the working hashtable
            tiles = newTerrain;

            startPos = player.transform.position;
        }
    }

}