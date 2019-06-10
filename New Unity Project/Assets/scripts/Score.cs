using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    static int s_highScore = 0;

    public Text title;
    public Text high;
    public Text now;
    public GameObject newBest;
    public static bool Win = false;

	// Use this for initialization
	void Start () {

        if (Win)
        {
            title.text = "Victory";
        } 
        else
        {
            title.text = "Gameover";
        }

        if (GameManager.Score > s_highScore)
        {
            s_highScore = GameManager.Score;
            newBest.SetActive(true);
        }

        high.text = "highScore: " + s_highScore;
        now.text = "score: " + GameManager.Score; 
        
	}

    public void GameReset()
    {
        GameManager.Score = 0;
        GameManager.PlaneHP = 5;
        Win = false;
        Debug.Log("reset");
        // manager.GetComponent<LevelManager>().level = 1;
        LevelManager.BrickAmount = 2;
    }
}
