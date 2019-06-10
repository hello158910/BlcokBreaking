using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickAction : MonoBehaviour {

    List<Vector2> m_randomPos;
    //Vector2[] m_randomPos;

	// Use this for initialization
	void Start () {
        m_randomPos = new List<Vector2>();
        for (int j = 0; j < 4; j++)
        {        
            for (int i = 0; i < 5; i++)
            {
                Vector2 pos =new Vector2(-2.5f+1.0f*(i+1),1.0f-(j+1)*0.45f);
                m_randomPos.Add(pos);
            }
        }
    }
	
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 pos = m_randomPos[Random.Range(0, m_randomPos.Count)];
        m_randomPos.Remove(pos);
        gameObject.transform.position = pos;
    }
}
