using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTiles : MonoBehaviour
{
    [SerializeField] int height;
    [SerializeField] int width;

    [SerializeField] GameObject grassTile;
    [SerializeField] GameObject grassTile2;
    [SerializeField] GameObject rockTile;
    [SerializeField] GameObject flowerTile2;
    [SerializeField] GameObject flowerTile;

    void Start()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var currenTileId = Random.Range(1, 20);
                if(currenTileId <= 9)
                {
                    Instantiate(grassTile2, 
                        new Vector3(transform.position.x + i,transform.position.y + j, 0), Quaternion.identity);
                }
                else if(currenTileId >= 10 && currenTileId <= 17)
                {
                    Instantiate(grassTile, 
                        new Vector3(transform.position.x + i, transform.position.y + j, 0), Quaternion.identity);
                }
                else if(currenTileId == 18)
                {
                    Instantiate(rockTile, 
                        new Vector3(transform.position.x + i, transform.position.y + j, 0), Quaternion.identity);
                }
                else if (currenTileId == 19)
                {
                    Instantiate(flowerTile, 
                        new Vector3(transform.position.x + i, transform.position.y + j, 0), Quaternion.identity);
                }
                else if(currenTileId == 20)
                {
                    Instantiate(flowerTile2, 
                        new Vector3(transform.position.x + i, transform.position.y + j, 0), Quaternion.identity);
                }
            }
        }
    }

    void Update()
    {
        
    }
}
