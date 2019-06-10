using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_LevelManager : MonoBehaviour {

    [SerializeField]
    GameObject[] m_brickTypes;
    //   public static List<GameObject> BrickList;
    [SerializeField]
    int m_level;
    [SerializeField]
    AudioClip m_passSound;

    public static int BrickAmount;
    const float CEILINGINITPOSY = 5.89f;

    // Use this for initialization
    void Start() {
        BrickAmount = 0;
        SetLevel(m_level);
        // BrickList = new List<GameObject>();

    }
    public void SetLevel(int num)
    {
        switch (num)
        {
            case 11:
                SetBrick(2, 2, new Vector2(-2.5f, 3.0f), 0);
                SetBrick(2, 2, new Vector2(-0.8f, 1.5f), 1);
                SetBrick(1, 3, new Vector2(-2.8f, 0.2f), 2, 1.3f);

                break;
            case 12:
                SetBrick(1, 4, new Vector2(-2.5f, 3.0f), 0);
                SetBrick(4, 1, new Vector2(-2.5f, 2.55f),2 );
                SetBrick(1, 4, new Vector2(-2.5f, 0.6f), 0);

                SetBrick(1, 3, new Vector2(-1.515f, 2.55f), 2);
                SetBrick(1, 3, new Vector2(-1.515f, 1.05f), 2);


                SetBrick(1, 3, new Vector2(-2.8f, -0.1f), 1);
                SetBrick(1, 3, new Vector2(-1.4f, -0.65f), 2);
                SetBrick(1, 3, new Vector2(-2.8f, -1.2f), 1);

                TurnStone(10);
                break;

            case 13:
                //SetBrick(4, 1, new Vector2(0.8f, -0.1f), 0);
                //SetBrick(4, 1, new Vector2(-2.7f, 2.8f), 1);
                //SetBrick(4, 1, new Vector2(0.8f, 2.8f), 2);


                //SetBrick(1, 4, new Vector2(-3.2f, 0.8f), 3, 1.3f);
                SetBrick(4, 1, new Vector2(-2.7f, -0.8f), 2);

                break;


        }

    }

    void SetBrick(int col, int row, Vector2 startPos, int type, float range = 0.985f)
    {
        for (int j = 0; j < col; j++)
        {
            for (int i = 0; i < row; i++)
            {
                GameObject clone = Instantiate(m_brickTypes[type]);
                if (type != 3) BrickAmount++;
                Vector2 pos = new Vector2(startPos.x + range * (i + 1), startPos.y - (j + 1) * range / 2);
                clone.transform.position = new Vector2(pos.x, pos.y);
            }
        }
    }

    void TurnStone(int ammount)
    {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("brick");
        for (int i = 0; i < ammount; i++)
        {
            int num = Random.Range(0, bricks.Length);
            if (bricks[num]!=null)
            {
                GameObject clone = Instantiate(m_brickTypes[3]);
                BrickAmount--;
                clone.transform.position = bricks[num].transform.position;
                Destroy(bricks[num]);
                bricks[num] = null;
            }
            
        }
    }

    // Update is called once per frame
    void Update () {
        if (BrickAmount==0)
        {
            M_BallMovement.ToNextLevel = true;
            m_level++;
            
            if (m_level == 14)
            {
                M_Score.Win = true;
                SceneManager.LoadScene("m_Over");
            }
            else
            {
                GameObject[] stones = GameObject.FindGameObjectsWithTag("stone");
                for (int i = 0; i < stones.Length; i++)
                {
                    Destroy(stones[i]);
                }

                SetLevel(m_level);
                gameObject.GetComponent<AudioSource>().clip = m_passSound;
                gameObject.GetComponent<AudioSource>().Play();
                GameObject ceilinig = GameObject.FindGameObjectWithTag("top");
                ceilinig.transform.position = new Vector3(0, CEILINGINITPOSY, 0);
                
            }

        }

    }

}
