using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Props : MonoBehaviour {

    [SerializeField]
    int m_type;
    Vector2 m_v;
    Rigidbody2D m_rb;

	// Use this for initialization
	void Start () {
        m_rb = gameObject.GetComponent<Rigidbody2D>();

    }
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            gameObject.SetActive(false);
       
            if (m_type == 1) //enlarge
            {
                collision.GetComponentInParent<PlaneControler>().Expend();
            }
            else if (m_type == -1) //shorten
            {
                collision.GetComponentInParent<PlaneControler>().Shrink();
            }
            else if (m_type == 0)
            {
                GameObject.FindGameObjectWithTag("ball").GetComponent<BallMovement>().Clone();              
            }
            else if (m_type == 3)
            {
                GameObject[] currentBalls = GameObject.FindGameObjectsWithTag("ball");
                for (int i = 0; i < currentBalls.Length; i++)
                {
                    currentBalls[i].GetComponent<BallMovement>().Speed(1.1f);
                }
            }
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
