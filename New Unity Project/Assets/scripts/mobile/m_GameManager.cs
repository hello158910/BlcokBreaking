using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class M_GameManager : MonoBehaviour {

    [SerializeField]
    Image[] m_uiScores;
    [SerializeField]
    Sprite[] m_scoreNums;
    [SerializeField]
    Image[] m_uiLifes = null;
    [SerializeField]
    GameObject[] m_props;
    [SerializeField]
    AudioClip m_hurtSound;


    public static bool IsReady;     // to shoot ball
    public static bool IsStart;     // game start
    public static bool IsPause = false;
    public static int Score = 0;
    public static int Hp; // current num of balls on stage
    public static int PlaneHp = 5;
    public static Vector3 LastBrickPos;
    public static bool Isnormal = true;

    const float DOWN_VALUE = -0.6f;
    const float MIN_X_POS = -2.5f; //player
    const float MAX_X_POS = 2.5f;
    const float POWERTIME = 2.0f;
    const float PICBORDER = 0.5f;
    const float MIN_POSY = -4.0f; //bricks

    bool m_canDrop, m_canDrop_Y;
    static bool s_getHurt = false;
    float m_time = 0;
   

    // Use this for initialization
    void Start() {
        IsReady = true;
        IsStart = false;
        Hp = 1;
        m_canDrop = true;
        m_canDrop_Y = true;

        //image = GameObject.FindGameObjectWithTag("plane");
        //image.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

        //-------

        if (m_uiScores != null && IsStart) SetScore();

        if (s_getHurt)
        {
            SetHp(PlaneHp);
            s_getHurt = false;
        }


        if (!Isnormal)
        {
            m_time += Time.deltaTime;
            if (m_time >= POWERTIME) { Isnormal = !Isnormal; m_time = 0; }
        }


        if (Score > 0 && Score % 7 == 0 && m_canDrop && IsStart)
        //if (Score == 1)
        {
            if (DropProp()) m_canDrop = false;
        }
        else if(Score > 0 && Score % 13 == 0 && m_canDrop_Y && IsStart)
        {
            DropYellow(); m_canDrop_Y = false;
        }
        if (Score % 7 != 0 )
        {
            m_canDrop = true;
        }
        if (Score%13!=0)
        {
            m_canDrop_Y=true;
        }

    }

    void SetHp(int hp)
    {
        if (m_uiLifes.Length != 0)
        {
            for (int i = 0; i < 5-hp; i++)
            {
                m_uiLifes[4-i].enabled = false;
            }
        }
        gameObject.GetComponent<AudioSource>().clip = m_hurtSound;
        gameObject.GetComponent<AudioSource>().Play();

    }

    void SetScore()
    {
        for (int i = 0; i < m_uiScores.Length; i++)
        {
            m_uiScores[i].sprite = m_scoreNums[(Score / (int)Mathf.Pow(10, i)) % 10];
        }
    }

    void DropYellow()
    {
        GameObject prop = Instantiate(m_props[0]);
        prop.transform.position = new Vector2(Random.Range(MIN_X_POS+ PICBORDER, MAX_X_POS- PICBORDER), Random.Range(-1.5f, 0.5f));
    }

    bool DropProp()
    {
        int i = Random.Range(1, m_props.Length);
        GameObject prop = Instantiate(m_props[i]);
        prop.transform.position = LastBrickPos;
        prop.SetActive(true);
        return true;
    }

    public static void Down()
    {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("brick");
        foreach (var item in bricks)
        {
            item.transform.position += new Vector3(0, DOWN_VALUE, 0);
            if (item.transform.position.y < MIN_POSY)
            {
                M_Score.Win = false;
                SceneManager.LoadScene("m_Over");
            }
        }

        GameObject[] stones = GameObject.FindGameObjectsWithTag("stone");
        foreach (var item in stones)
        {
            item.transform.position += new Vector3(0, DOWN_VALUE, 0);
        }
        GameObject ceilinig = GameObject.FindGameObjectWithTag("top");
        ceilinig.transform.position += new Vector3(0, DOWN_VALUE, 0);

        s_getHurt = true;
       
    }

    void PauseAll()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("m_ball");
        foreach (var item in balls)
        {
            item.GetComponent<M_BallMovement>().Pause();
        }

        GameObject[] m_props = GameObject.FindGameObjectsWithTag("props");
        foreach (var item in m_props)
        {
            item.GetComponent<M_props>().Pause();
        }
    }

    public void UnfreezeAll()
    {

        GameObject[] balls = GameObject.FindGameObjectsWithTag("m_ball");
        foreach (var item in balls)
        {
            item.GetComponent<M_BallMovement>().ContinuePlay();
        }

        GameObject[] m_props = GameObject.FindGameObjectsWithTag("props");
        foreach (var item in m_props)
        {
            item.GetComponent<M_props>().ContinuePlay();
        }
    }

    public void SetIsPause(bool b)
    {
        IsPause = b;
    }

    public void PauseClicked(GameObject img)
    {
        IsPause = !IsPause;
        img.SetActive(IsPause);
        if (IsPause)
        {
            PauseAll();
            //image.SetActive(true);
        }
        else
        {
            UnfreezeAll();
            //image.SetActive(false);
        }
    }

    public void Restart()
    {
        Score = 0;
        PlaneHp = 5;
    }

}
