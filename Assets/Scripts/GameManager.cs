using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public int bestScore = 0;
    public int currentScore = 0;
    public int currentLevel = 0;
    public static GameManager singleton;  

    
    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(singleton.gameObject);
        }
        bestScore = PlayerPrefs.GetInt("highScore");
    }

    public void NextLevel()
    {
        currentLevel++;
        FindAnyObjectByType<BallController>().ResetBall(); // Reset the ball's position
        FindAnyObjectByType<HelixController>().LoadStage(currentLevel); // Load the next level
        Debug.Log("Next Level");
    }
    public void RestartLevel()
    {
    Debug.Log("Restart");
    singleton.currentScore = 0;
    FindAnyObjectByType<BallController>().ResetBall();
    FindAnyObjectByType<HelixController>().LoadStage(currentLevel);  // Reset the ball's position
    }
    public void AddScore(int scoretoAdd)
    {
        currentScore += scoretoAdd;
        if (currentScore > bestScore)
        {
            PlayerPrefs.SetInt("highScore", currentScore);
            
        }
    }
 }