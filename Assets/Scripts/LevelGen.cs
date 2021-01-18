using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    public int levelSizeX = 32;
    public int LevelSizeY = 8;
    public GameObject LavaPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        // Make lava{
        for (int x = 0; x < (levelSizeX + 1); x++)
        {
            Instantiate(LavaPrefab.gameObject, new Vector3(x,1),Quaternion.identity);
        }
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
