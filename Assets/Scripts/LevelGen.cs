﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    public int levelSizeX = 32;
    public int levelSizeY = 8;
    public int pointStepX = 7;
    public int pathsRareness = 4;
    public int enemiesRareness = 4;
    public GameObject lavaPrefab;
    public GameObject blockPrefab;
    public GameObject finishPrefab;
    public GameObject enemyPrefab;
    
    private System.Random rnd = new System.Random();
    
    
    // Start is called before the first frame update
    void Start()
    {
        int startPoint = MakeStartPoint();
        int finishPoint = MakeFinishPoint();
        
        MakeLava();
        //MakeBorders();
        for (int i = 0; i < (levelSizeY / pathsRareness); i++)
        {
            MakePath(startPoint, finishPoint);
        }
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int MakeStartPoint()
    {
        int startPointY = rnd.Next(2, (levelSizeY - 1));
        Instantiate(blockPrefab, new Vector3(0, startPointY),Quaternion.identity);
        return startPointY;
    }

    private int MakeFinishPoint()
    {
        int finishPointY = rnd.Next(2, (levelSizeY - 1));
        Instantiate(finishPrefab, new Vector3(levelSizeX, finishPointY),Quaternion.identity);
        return finishPointY;
    }

    private void MakeLava()
    {
        for (int x = -5; x < (levelSizeX + 5); x++)
        {
            Instantiate(lavaPrefab, new Vector3(x,1),Quaternion.identity);
        }
    }

    private void MakeBorders()
    {
        for (int y = 0; y < (levelSizeY + 2); y++)
        {
            if (y == 0 || y == (levelSizeY + 1))
            {
                for (int x = 0; x < (levelSizeX + 2); x++)
                {
                    Instantiate(blockPrefab, new Vector3(x,y),Quaternion.identity); // Vertical borders
                }
            }
            else
            {
                Instantiate(blockPrefab, new Vector3(0, y),Quaternion.identity); // Lower horizontal border
                Instantiate(blockPrefab, new Vector3((levelSizeX + 1), y),Quaternion.identity); // Upper horizontal border
            }
        }
    }
    
    private void MakePath(int startPoint, int finishPoint)
    {
        int previousPointX = 0;
        int previousPointY = startPoint;
        int currentPointX = pointStepX;
        int currentPointY;

        while (currentPointX < (levelSizeX - pointStepX))
        {
            currentPointY = rnd.Next(2, (levelSizeY - 1));
            Instantiate(blockPrefab, new Vector3(currentPointX, currentPointY),Quaternion.identity);
            
            // Make route to point {
            MakeRouteToPoint(previousPointX, previousPointY, currentPointX, currentPointY);
            // }

            previousPointX = currentPointX;
            previousPointY = currentPointY;
            currentPointX += pointStepX;
        }
        
        // Make route to finish {
        MakeRouteToPoint(previousPointX, previousPointY, (levelSizeX - 1), finishPoint);
        // }
    }
    
    private void MakeRouteToPoint(int startPointX, int startPointY, int targetPointX, int targetPointY)
    {
        const int directPrice = 10;
        const int diagonPrice = 14;
        
        int currentX = startPointX;
        int currentY = startPointY;
        int summaryPriceToStart = 0;
        int toPreviousPrice;
        int toTargetPrice;
        int[] summaryPrices = new int[8];
        
        while (currentX < targetPointX)
        {
            // Up Left
            toPreviousPrice = summaryPriceToStart + diagonPrice;
            toTargetPrice = (Math.Abs((currentX - 1) - targetPointX) + Mathf.Abs((currentY + 1) - targetPointY)) * 10;
            summaryPrices[0] = toPreviousPrice + toTargetPrice;
            
            // Up
            toPreviousPrice = summaryPriceToStart + directPrice;
            toTargetPrice = (Math.Abs(currentX - targetPointX) + Mathf.Abs((currentY + 1) - targetPointY)) * 10;
            summaryPrices[1] = toPreviousPrice + toTargetPrice;
            
            // Up Right
            toPreviousPrice = summaryPriceToStart + diagonPrice;
            toTargetPrice = (Math.Abs((currentX + 1) - targetPointX) + Mathf.Abs((currentY + 1) - targetPointY)) * 10;
            summaryPrices[2] = toPreviousPrice + toTargetPrice;
            
            // Left
            toPreviousPrice = summaryPriceToStart + directPrice;
            toTargetPrice = (Math.Abs((currentX - 1) - targetPointX) + Mathf.Abs(currentY - targetPointY)) * 10;
            summaryPrices[3] = toPreviousPrice + toTargetPrice;
            
            // Right
            toPreviousPrice = summaryPriceToStart + directPrice;
            toTargetPrice = (Math.Abs((currentX + 1) - targetPointX) + Mathf.Abs(currentY - targetPointY)) * 10;
            summaryPrices[4] = toPreviousPrice + toTargetPrice;
            
            // Down Left
            toPreviousPrice = summaryPriceToStart + diagonPrice;
            toTargetPrice = (Math.Abs((currentX - 1) - targetPointX) + Mathf.Abs((currentY - 1) - targetPointY)) * 10;
            summaryPrices[5] = toPreviousPrice + toTargetPrice;
            
            // Down
            toPreviousPrice = summaryPriceToStart + directPrice;
            toTargetPrice = (Math.Abs(currentX - targetPointX) + Mathf.Abs((currentY - 1) - targetPointY)) * 10;
            summaryPrices[6] = toPreviousPrice + toTargetPrice;
            
            // Down Right
            toPreviousPrice = summaryPriceToStart + diagonPrice;
            toTargetPrice = (Math.Abs((currentX + 1) - targetPointX) + Mathf.Abs((currentY - 1) - targetPointY)) * 10;
            summaryPrices[7] = toPreviousPrice + toTargetPrice;
            

            int lessFIndex = 0;
            for (int i = 1; i < summaryPrices.Length; i++)
            {
                if (summaryPrices[lessFIndex] > summaryPrices[i]) lessFIndex = i;
            }

            switch (lessFIndex)
            {
                case 0:
                    Instantiate(blockPrefab, new Vector3((currentX - 1), (currentY + 1)), Quaternion.identity);
                    summaryPriceToStart += diagonPrice;
                    currentX--;
                    currentY++;
                    break;
                case 1:
                    Instantiate(blockPrefab, new Vector3(currentX, (currentY + 1)), Quaternion.identity);
                    summaryPriceToStart += directPrice;
                    currentY++;
                    break;
                case 2:
                    Instantiate(blockPrefab, new Vector3((currentX + 1), (currentY + 1)), Quaternion.identity);
                    summaryPriceToStart += diagonPrice;
                    currentX++;
                    currentY++;
                    break;
                case 3:
                    Instantiate(blockPrefab, new Vector3((currentX - 1), currentY), Quaternion.identity);
                    summaryPriceToStart += directPrice;
                    currentX--;
                    break;
                case 4:
                    Instantiate(blockPrefab, new Vector3((currentX + 1), currentY), Quaternion.identity);
                    summaryPriceToStart += directPrice;
                    currentX++;
                    break;
                case 5:
                    Instantiate(blockPrefab, new Vector3((currentX - 1), (currentY - 1)), Quaternion.identity);
                    summaryPriceToStart += diagonPrice;
                    currentX--;
                    currentY--;
                    break;
                case 6:
                    Instantiate(blockPrefab, new Vector3(currentX, (currentY - 1)), Quaternion.identity);
                    summaryPriceToStart += directPrice;
                    currentY--;
                    break;
                case 7:
                    Instantiate(blockPrefab, new Vector3((currentX + 1), (currentY - 1)), Quaternion.identity);
                    summaryPriceToStart += diagonPrice;
                    currentX++;
                    currentY--;
                    break;
                default:
                    break;
            }
        }
    }

    private void SpawnEnemies()
    {
        int i = 1;
        while (i < levelSizeX)
        {
            Instantiate(enemyPrefab, new Vector3(i,levelSizeY),Quaternion.identity);
            i += enemiesRareness;
        }
    }
}
