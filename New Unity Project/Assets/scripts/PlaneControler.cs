using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneControler : MonoBehaviour {

    public GameObject[] planes;
    //bool m_isDbClicked;
    bool m_isA = true;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {


        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (pos.x < 2.5f && pos.x > -2.5f && !GameManager.isPause)
        {
            transform.position = new Vector3(pos.x, -4.31f, 0);
        }



    }

    public void Expend()
    {
        gameObject.transform.localScale += new Vector3(0.5f, 0, 0);     
    }

    public void Shrink()
    {
        gameObject.transform.localScale += new Vector3(-0.25f, 0, 0);
    }

    void ChangePlane()
    {

        m_isA = !m_isA;
        if (m_isA)
        {
            Debug.Log("a");
            planes[0].SetActive(false);
            planes[1].SetActive(true);
        }
        else
        {
            Debug.Log("b");
            planes[0].SetActive(true);
            planes[1].SetActive(false);
        }
       // m_isDbClicked = false;
    }


}
