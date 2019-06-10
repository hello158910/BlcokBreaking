using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static bool IsReady;
    public static bool IsStart;
    public static int Score = 0;
    public static int Hp;
    public static Vector3 lastBrickPos;
    public static int PlaneHP = 5;
    public static bool isPause = false;

    public Text uiScore, uiLife;
    public GameObject[] props;

    [SerializeField]
    bool canDrop;

    GameObject image;

    // Use this for initialization
    void Start() {
        IsReady = true;
        IsStart = false;
        Hp = 1;
        canDrop = true;

        image = GameObject.FindGameObjectWithTag("plane");
        image.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
            if (isPause)
            {
                PauseAll();
                image.SetActive(true);
            }
            else
            {
                UnfreezeAll();
                image.SetActive(false);
            }
        }

        if (IsReady)
        {
            if (Input.GetMouseButtonDown(0)) { IsStart = true; IsReady = false; }
        }
        if (uiScore != null) uiScore.text = "Score: " + Score;
        if (uiLife != null) uiLife.text = "Life:" + PlaneHP;



        if (Score != 0 && Score % 3 == 0 && canDrop && BallMovement.IsPlaying)
        //if (Score == 1)
        {
            if (DropProp()) canDrop = false;
        }
        if (Score % 3 != 0)
        {
            canDrop = true;
        }

    }
    bool DropProp()
    {
        int i = Random.Range(0, props.Length);
        GameObject prop = Instantiate(props[i]);
        prop.transform.position = lastBrickPos;
        prop.SetActive(true);
        return true;
    }

    public static void Down()
    {
        foreach (var item in LevelManager.BrickList)
        {
            item.transform.position += new Vector3(0, -1.1f, 0);
        }
        GameObject ceilinig = GameObject.FindGameObjectWithTag("top");
        ceilinig.transform.position += new Vector3(0, -1.1f, 0);
    }

    public static void NextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ToLevel1()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void PauseAll()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("ball");
        foreach (var item in balls)
        {
            item.GetComponent<BallMovement>().Pause();
        }

        GameObject[] props = GameObject.FindGameObjectsWithTag("props");
        foreach (var item in props)
        {
            item.GetComponent<Props>().Pause();
        }
    }

    public void UnfreezeAll()
    {

        GameObject[] balls = GameObject.FindGameObjectsWithTag("ball");
        foreach (var item in balls)
        {
            item.GetComponent<BallMovement>().ContinuePlay();
        }

        GameObject[] props = GameObject.FindGameObjectsWithTag("props");
        foreach (var item in props)
        {
            item.GetComponent<Props>().ContinuePlay();
        }
    }

    public void SetIsPause(bool b)
    {
        isPause = b;
    }

}
