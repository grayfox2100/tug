using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    public int levelSizeX = 32;
    public int levelSizeY = 8;
    public int pointStepX = 7;
    public int numberOfPaths = 4;
    public int numberOfEnemies = 4;
    private int _startPoint;
    private int _finishPoint;  
    
    public GameObject lavaPrefab;
    public GameObject blockPrefab;
    public GameObject finishPrefab;
    
    private System.Random _rnd = new System.Random();

    void Start()
    {
        _startPoint = MakeExtremePoint();
        _finishPoint = MakeExtremePoint(true);
        
        for (int i = 0; i < numberOfPaths; i++)
        {
            MakePath();
        }
        
        MakeLava();
        SpawnEnemies();
        SpawnPlayer();
    }

    private int MakeExtremePoint(bool isFinish = false)
    {
        int pointY = _rnd.Next(1, levelSizeY);
        
        if (isFinish)
        {
            Instantiate(finishPrefab, new Vector3(levelSizeX, pointY),Quaternion.identity);
        }
        else
        {
            Instantiate(blockPrefab, new Vector3(0, pointY),Quaternion.identity);
        }

        return pointY;
    }
    
    private void MakeLava()
    {
        for (int x = -5; x < (levelSizeX + 5); x++)
        {
            Instantiate(lavaPrefab, new Vector3(x,0),Quaternion.identity);
        }
    }
    
    private void MakePath()
    {
        int previousPointX = 0;
        int previousPointY = _startPoint;
        int currentPointX = pointStepX;

        while (currentPointX < (levelSizeX - pointStepX))
        {
            int currentPointY = _rnd.Next(1, levelSizeY);
            Instantiate(blockPrefab, new Vector3(currentPointX, currentPointY),Quaternion.identity);
            
            MakeRouteToPoint(previousPointX, previousPointY, currentPointX, currentPointY);

            previousPointX = currentPointX;
            previousPointY = currentPointY;
            currentPointX += pointStepX;
        }
        
        MakeRouteToPoint(previousPointX, previousPointY, (levelSizeX - 1), _finishPoint);
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
        if (numberOfEnemies > 0)
        {
            int enemiesFrequency = (levelSizeX / numberOfEnemies);
            int i = 2;
            while (i < levelSizeX)
            {
                //Character.Create(new EnemyLifecycle(), new Vector3(i, levelSizeY), Character.Types.Enemy);
                i += enemiesFrequency;
            }
        } else return;
    }
    
    private void SpawnPlayer()
    {
        LevelData.PlayerSpawn = new Vector3(0, _startPoint + 1);
        LevelData.Player = CharFactory.Create(CharFactory.Types.Player, LevelData.PlayerSpawn);
    }
}
