using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    public int levelSizeX = 32;
    public int LevelSizeY = 8;
    public GameObject LavaPrefab;
    public GameObject BlockPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        // Make lava {
        int lavaCount = 0;
        for (int x = 1; x < (levelSizeX + 1); x++)
        {
            Instantiate(LavaPrefab, new Vector3(x,1),Quaternion.identity);
        }
        // }
        
        // Make borders {
        for (int y = 0; y < (LevelSizeY + 2); y++)
        {
            if (y == 0 || y == (LevelSizeY + 1))
            {
                for (int x = 0; x < (levelSizeX + 2); x++)
                {
                    Instantiate(BlockPrefab, new Vector3(x,y),Quaternion.identity); // Vertical borders
                }
            }
            else
            {
                Instantiate(BlockPrefab, new Vector3(0, y),Quaternion.identity); // Lower horizontal border
                Instantiate(BlockPrefab, new Vector3((levelSizeX + 1), y),Quaternion.identity); // Upper horizontal border
            }
        }
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
