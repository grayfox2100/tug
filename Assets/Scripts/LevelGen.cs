using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    public int levelSizeX = 32;
    public int LevelSizeY = 8;
    public int pointStepX = 7;
    public GameObject LavaPrefab;
    public GameObject BlockPrefab;
    public GameObject FinishPrefab;
    
    private System.Random rnd = new System.Random();
    
    // Start is called before the first frame update
    void Start()
    {
        MakeLava();
        MakeBorders();
        MakePath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int MakeStartPoint()
    {
        int startPointY = rnd.Next(2, (LevelSizeY - 1));
        Instantiate(BlockPrefab, new Vector3(1, startPointY),Quaternion.identity);
        return startPointY;
    }

    private int MakeFinishPoint()
    {
        int finishPointY = rnd.Next(2, (LevelSizeY - 1));
        Instantiate(FinishPrefab, new Vector3(levelSizeX, finishPointY),Quaternion.identity);
        return finishPointY;
    }

    private void MakeLava()
    {
        //int lavaCount = 0;
        for (int x = 1; x < (levelSizeX + 1); x++)
        {
            Instantiate(LavaPrefab, new Vector3(x,1),Quaternion.identity);
        }
    }

    private void MakeBorders()
    {
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
    }
    
    private void MakePath()
    {
        int previousPointX = 1;
        int previousPointY = MakeStartPoint();
        int currentPointX = pointStepX;
        int currentPointY;

        while (currentPointX < (levelSizeX - pointStepX))
        {
            currentPointY = rnd.Next(2, (LevelSizeY - 1));
            Instantiate(BlockPrefab, new Vector3(currentPointX, currentPointY),Quaternion.identity);
            
            // Make route to point {
            MakeRouteToPoint(previousPointX, previousPointY, currentPointX, currentPointY);
            // }

            previousPointX = currentPointX;
            previousPointY = currentPointY;
            currentPointX += pointStepX;
        }
        
        // Make route to finish {
        MakeRouteToPoint(previousPointX, previousPointY, (levelSizeX - 1), MakeFinishPoint());
        // }
    }
    
    private void MakeRouteToPoint(int startPointX, int startPointY, int targetPointX, int targetPointY)
    {
        int currentX = startPointX;
        int currentY = startPointY;
        // h(v) = |v.x - goal.x| + |v.y - goal.y|
        int priceToStart = 0;
        int g; //to previous
        int h; //to target
        int[] F = new int[8]; // F = g + h
        /*int lessF;
        int i = 0;*/

        //while (currentX != targetPointX && currentY != targetPointY)
        while (currentX < targetPointX)
        {
            // Up Left
            //g = Mathf.Abs((currentX + 14) - startPointX) + Mathf.Abs((currentY - 14) - startPointY); // To parent cell price
            g = priceToStart + 14;
            h = (Math.Abs((currentX - 1) - targetPointX) + Mathf.Abs((currentY + 1) - targetPointY)) * 10; // To target cell price
            F[0] = g + h;
            
            // Up
            //g = Mathf.Abs((currentX + 10) - startPointX) + Mathf.Abs(currentY - startPointY); // To parent cell price
            g = priceToStart + 10;
            h = (Math.Abs(currentX - targetPointX) + Mathf.Abs((currentY + 1) - targetPointY)) * 10; // To target cell price
            F[1] = g + h;
            
            // Up Right
            //g = Mathf.Abs((currentX + 14) - startPointX) + Mathf.Abs((currentY + 14) - startPointY); // To parent cell price
            g = priceToStart + 14;
            h = (Math.Abs((currentX + 1) - targetPointX) + Mathf.Abs((currentY + 1) - targetPointY)) * 10; // To target cell price
            F[2] = g + h;
            
            // Left
            //g = Mathf.Abs(currentX - startPointX) + Mathf.Abs((currentY - 10) - startPointY); // To parent cell price
            g = priceToStart + 10;
            h = (Math.Abs((currentX - 1) - targetPointX) + Mathf.Abs(currentY - targetPointY)) * 10; // To target cell price
            F[3] = g + h;
            
            // Right
            //g = Mathf.Abs(currentX - startPointX) + Mathf.Abs((currentY + 10) - startPointY); // To parent cell price
            g = priceToStart + 10;
            h = (Math.Abs((currentX + 1) - targetPointX) + Mathf.Abs(currentY - targetPointY)) * 10; // To target cell price
            F[4] = g + h;
            
            // Down Left
            //g = Mathf.Abs((currentX - 14) - startPointX) + Mathf.Abs((currentY - 14) - startPointY); // To parent cell price
            g = priceToStart + 14;
            h = (Math.Abs((currentX - 1) - targetPointX) + Mathf.Abs((currentY - 1) - targetPointY)) * 10; // To target cell price
            F[5] = g + h;
            
            // Down
            //g = Mathf.Abs((currentX - 10) - startPointX) + Mathf.Abs(currentY - startPointY); // To parent cell price
            g = priceToStart + 10;
            h = (Math.Abs(currentX - targetPointX) + Mathf.Abs((currentY - 1) - targetPointY)) * 10; // To target cell price
            F[6] = g + h;
            
            // Down Right
            //g = Mathf.Abs((currentX - 14) - startPointX) + Mathf.Abs((currentY + 14) - startPointY); // To parent cell price
            g = priceToStart + 14;
            h = (Math.Abs((currentX + 1) - targetPointX) + Mathf.Abs((currentY - 1) - targetPointY)) * 10; // To target cell price
            F[7] = g + h;

            /*lessF = 0;
            while (i < 8)
            {
                if (F[lessF] > F[i]) lessF = i;
                i++;
            }*/

            int lessFIndex = 0;
            for (int i = 1; i < F.Length; i++)
            {
                if (F[lessFIndex] > F[i]) lessFIndex = i;
            }
            
            /*
                0 | 1 | 2
                3 |   | 4    F-indexes location
                5 | 6 | 7
            */
            //Debug.Log(lessFIndex);
            switch (lessFIndex)
            {
                case 0:
                    Instantiate(BlockPrefab, new Vector3((currentX - 1), (currentY + 1)), Quaternion.identity);
                    priceToStart += 14;
                    currentX--;
                    currentY++;
                    break;
                case 1:
                    Instantiate(BlockPrefab, new Vector3(currentX, (currentY + 1)), Quaternion.identity);
                    priceToStart += 10;
                    currentY++;
                    break;
                case 2:
                    Instantiate(BlockPrefab, new Vector3((currentX + 1), (currentY + 1)), Quaternion.identity);
                    priceToStart += 14;
                    currentX++;
                    currentY++;
                    break;
                case 3:
                    Instantiate(BlockPrefab, new Vector3((currentX - 1), currentY), Quaternion.identity);
                    priceToStart += 10;
                    currentX--;
                    break;
                case 4:
                    Instantiate(BlockPrefab, new Vector3((currentX + 1), currentY), Quaternion.identity);
                    priceToStart += 10;
                    currentX++;
                    break;
                case 5:
                    Instantiate(BlockPrefab, new Vector3((currentX - 1), (currentY - 1)), Quaternion.identity);
                    priceToStart += 14;
                    currentX--;
                    currentY--;
                    break;
                case 6:
                    Instantiate(BlockPrefab, new Vector3(currentX, (currentY - 1)), Quaternion.identity);
                    priceToStart += 10;
                    currentY--;
                    break;
                case 7:
                    Instantiate(BlockPrefab, new Vector3((currentX + 1), (currentY - 1)), Quaternion.identity);
                    priceToStart += 14;
                    currentX++;
                    currentY--;
                    break;
                default:
                    break;
            }
        }
    }
}
