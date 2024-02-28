using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject LaserManager;

    private int score;
    private float startTime;
    private int elapsedTime = 0;
    private float elapsedTimeMin = 0;

    private static int gameState = 0;

    void Start()
    {
        
    }

    void Awake()
    {
        startTime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        elapsedTime = (int) (Time.realtimeSinceStartup - startTime);
        elapsedTimeMin = (Time.realtimeSinceStartup - startTime) / 60;
        score = elapsedTime;

    }

    void LateUpdate()
    {
        UpdateGameState();
    }

    void UpdateGameState()
    {
        if(elapsedTimeMin < 1 ){
            gameState = 0;
        } else if (elapsedTimeMin < 3){
            gameState = 1;
        } else if (elapsedTimeMin < 5){
            gameState = 2;
        } else if (elapsedTimeMin < 7){
            gameState = 3;
        } else if (elapsedTimeMin < 10){
            gameState = 4;
        } else {
            gameState = 5;
        }
    }

    public int GetGameState(){
        return gameState;
    }

    public int GetScore(){
        return score;
    }
}
