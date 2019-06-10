using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_BrickSetting : MonoBehaviour {

    [SerializeField]
    int m_life;
    [SerializeField]
    int m_score;

    // Use this for initialization

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("m_ball"))
        {
            m_life--;

            gameObject.GetComponent<SpriteRenderer>().color += new Color(0.1f, 0.1f, 0.1f);
            if (m_life == 0)
            {
                BreakBlock();
            }

        }
    }

    public void BreakBlock()
    {
        M_GameManager.LastBrickPos = gameObject.transform.position;
        Destroy(gameObject);
        M_GameManager.Score += m_score;
        M_LevelManager.BrickAmount--;
    }
     
}






