using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_props : MonoBehaviour {

    [SerializeField]
    int m_type;

    Vector2 m_v;
    Rigidbody2D m_rb;
    //int m_count;  // for m_type2

	// Use this for initialization
	void Start () {
        m_rb = gameObject.GetComponent<Rigidbody2D>();
        //m_count = 0;
    }
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            //gameObject.SetActive(false);
       
            if (m_type == 1) //enlarge
            {
                collision.GetComponent<M_PlaneControler>().Expend();
            }
            else if (m_type == -1) //shorten
            {
                collision.GetComponent<M_PlaneControler>().Shrink();
            }
            else if (m_type == 0)
            {
                GameObject.FindGameObjectWithTag("m_ball").GetComponent<M_BallMovement>().Clone();              
            }
            else if (m_type == 3)
            {
                GameObject[] currentBalls = GameObject.FindGameObjectsWithTag("m_ball");
                for (int i = 0; i < currentBalls.Length; i++)
                {
                    currentBalls[i].GetComponent<M_BallMovement>().Speed(1.1f);
                }
            }
            else if (m_type == 2)
            {
                //m_count++;
                //if (m_count==3)
                //{
                    M_GameManager.Isnormal = false;
                //}
            }

            Destroy(gameObject);
        }
    }

    public void Pause()
    {
        m_v = m_rb.velocity;
        m_rb.gravityScale = 0;
        m_rb.velocity= Vector2.zero;

    }

    public void ContinuePlay()
    {
        m_rb.velocity = m_v;
        m_rb.gravityScale = 0.8f;
    }

}
