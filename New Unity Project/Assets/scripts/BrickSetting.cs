using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSetting : MonoBehaviour {

    [SerializeField]
    int m_life;
    [SerializeField]
    int m_score;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            m_life--;
            gameObject.GetComponent<SpriteRenderer>().color += new Color(0.2f, 0.2f, 0.2f);
            if (m_life == 0)
            {
                GameManager.lastBrickPos = gameObject.transform.position;
                LevelManager.BrickList.Remove(gameObject);
                Destroy(gameObject);
                GameManager.Score += m_score;
                LevelManager.BrickAmount--;
            }
        }
    }
}






