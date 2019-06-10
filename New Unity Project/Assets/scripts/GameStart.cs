using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {

    public bool canSet;

    // Use this for initialization
    void Start() {
        canSet = false;
 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canSet = true;
        gameObject.SetActive(false);

    }

}
