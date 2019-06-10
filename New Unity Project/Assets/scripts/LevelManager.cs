using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public GameObject[] brickTypes;
    public GameObject start; 
    public string nextSceneName;

    public static List<GameObject> BrickList;
    public static int BrickAmount;

    [SerializeField]
    int m_level;
    [SerializeField]
    bool m_canset; // is not Level1

    GameObject m_ball;
    bool m_setDone;
    

    // Use this for initialization
    void Start () {
        BrickAmount = -1;
        m_setDone = false;
        BrickList = new List<GameObject>();
        m_ball = GameObject.FindGameObjectWithTag("ball");

    }
	public void SetBrick(int num)
    {
        switch (num)
        {
            case 1:
                BrickAmount = 0;
                //Arrange(0, amount, new Vector2(-2.5f, 1.0f));
                //Arrange(0, amount, new Vector2(-2.5f, 1.45f));
                //Arrange(1, amount, new Vector2(-2.5f, 1.9f));
                //Arrange(1, amount, new Vector2(-2.5f, 2.35f));
               
                Arrange(0, 5, new Vector2(-2.5f, 3.7f));
                Arrange(2, 4, new Vector2(-2.5f, 4.15f));
                break;
            case 2:
                BrickAmount = 0;
                Arrange(0, 5, new Vector2(-2.5f, 2.3f));
                //Arrange(2, amount, new Vector2(-2.5f, 1.45f));
                
                //Arrange(0, amount, new Vector2(-2.5f, 2.35f));
                Arrange(1, 4, new Vector2(-0.4f, 3.8f));
                //Arrange(1, amount, new Vector2(-0.4f, 3.25f));
                //Arrange(0, amount, new Vector2(-0.4f, 3.7f));
                //Arrange(2, amount, new Vector2(-0.4f, 4.15f));
                Arrange(2, 5, new Vector2(-2.65f, 1.6f), 1.3f);
                break;
            case 3:
                BrickAmount = 0;
                Arrange(2, 2, new Vector2(0, 3.8f));
                Arrange(2, 2, new Vector2(0, 4.25f));
                Arrange(1, 3, new Vector2(-2.5f, 2.9f));
                Arrange(1, 3, new Vector2(-2.5f, 3.35f));
                Arrange(0, 5, new Vector2(-2.65f, 1.4f), 1.3f);
                break;


        }
       
    }

    void Arrange(int type, int amount, Vector2 pos)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject clone = Instantiate(brickTypes[type]);
            BrickList.Add(clone);
            BrickAmount++;
            clone.transform.position = new Vector2(pos.x + 0.985f * i, pos.y);
            
        }
    }

    void Arrange(int type, int amount, Vector2 pos, float range)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject clone = Instantiate(brickTypes[type]);
            BrickList.Add(clone);
            BrickAmount++;
            clone.transform.position = new Vector2(pos.x + range * i, pos.y);
        }
    }

    // Update is called once per frame
    void Update () {
        
        
        if (start!= null) { m_canset = start.GetComponent<GameStart>().canSet; }
        if (!m_setDone)
        {
            if (m_ball.transform.position.y < -1.0f && m_canset)
            {
                SetBrick(m_level);
               // start.canSet = false;
                m_setDone = true;
            }
        }

        if (BrickAmount ==0)
        {
            if (m_level == 3) Score.Win = true;
            GameManager.NextScene(nextSceneName);

        }
    }

}
