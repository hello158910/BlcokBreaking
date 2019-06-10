using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_PlaneControler : MonoBehaviour {

    int m_count;
    bool m_isDbClicked;
    Vector2 m_screenPos;

    [SerializeField]
    AudioClip[] m_clips;

    const float MAX_POS_X = 1.8f;
    const float MIN_POS_X = -1.7f;
    const float PLAYER_POS_Y = -4.3f;
    const float ENLARGE_SACLE = 0.3f;
    const float SHRINK_SCALE = -0.25f;
    

    // Use this for initialization
    void Start() {
        m_screenPos = new Vector2();
    }


    private void OnMouseDrag()
    {
        if (!M_GameManager.IsPause && !M_GameManager.IsReady)
        {
            m_screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (m_screenPos.x < MAX_POS_X && m_screenPos.x > MIN_POS_X)
            {
                transform.position = new Vector2(m_screenPos.x, PLAYER_POS_Y);
            }
        }
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

    }
    private void OnMouseUp()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void Expend()
    {
        gameObject.transform.localScale += new Vector3(0, ENLARGE_SACLE, 0);     
    }

    public void Shrink()
    {
        gameObject.transform.localScale += new Vector3(0, SHRINK_SCALE, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("props"))
        {
            gameObject.GetComponent<AudioSource>().clip = m_clips[1];
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    public void PlayBounceSound()
    {
        gameObject.GetComponent<AudioSource>().clip = m_clips[0];
        gameObject.GetComponent<AudioSource>().Play();
    }

}
