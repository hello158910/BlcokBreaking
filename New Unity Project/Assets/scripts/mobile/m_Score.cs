using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_Score : MonoBehaviour {

    [SerializeField]
    GameObject m_title;
    [SerializeField]
    Sprite[] m_titlePics;
    [SerializeField]
    Image[] m_uiScores;
    [SerializeField]
    Image[] m_uiHighScores;
    [SerializeField]
    Sprite[] m_scoreNums;
    [SerializeField]
    GameObject m_newBest;
    [SerializeField]
    AudioClip[] m_sounds;

    public static bool Win = false;

    AudioSource m_bgm;

    static int s_highScore = 0;


 //   public bool Win_tes;

	// Use this for initialization
	void Start () {
        m_bgm = gameObject.GetComponent<AudioSource>();
        SpriteRenderer title = m_title.GetComponent<SpriteRenderer>();
        if (Win)
        {
            title.sprite = m_titlePics[0];
            m_bgm.clip = m_sounds[0];
            m_bgm.Play();
        } 
        else
        {
            title.sprite = m_titlePics[1];
            m_bgm.clip = m_sounds[1];
            m_bgm.Play();
        }

        if (M_GameManager.Score > s_highScore)
        {
            s_highScore = M_GameManager.Score;
            m_newBest.SetActive(true);
        }

        //--score 

        for (int i = 0; i < m_uiScores.Length; i++)
        {
            m_uiScores[i].sprite = m_scoreNums[(M_GameManager.Score / (int)Mathf.Pow(10, i)) % 10];
        }

        //--highscore

        for (int i = 0; i < m_uiHighScores.Length; i++)
        {
            m_uiHighScores[i].sprite = m_scoreNums[(s_highScore / (int)Mathf.Pow(10, i)) % 10];
        }
        
	}

    public void GameReset()
    {
        M_GameManager.Score = 0;
        M_GameManager.PlaneHp = 5;
        Win = false;
        // manager.GetComponent<LevelManager>().level = 1;
        M_LevelManager.BrickAmount = 2;
        M_GameManager.Isnormal = true;
    }
}
