using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour {

    [SerializeField]
    float m_speedX, m_speedY;
    public GameObject plane;
    
    public static bool IsPlaying = false;

    Rigidbody2D m_rb;
    CircleCollider2D m_collider;

    static Vector2 s_ball_v;
    
    // Use this for initialization
    void Start() {
        m_rb = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<CircleCollider2D>();
        plane = GameObject.FindGameObjectWithTag("player");
        
    }

    // Update is called once per frame
    void Update() {

        // test
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameObject clone = Instantiate(gameObject);
            clone.GetComponent<Rigidbody2D>().velocity = new Vector2(2 * Time.deltaTime, 7 * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.T)) m_rb.velocity = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.R)) BallReset();

        //  ----

        if (GameManager.IsStart)
        {
            if (GameStart()) IsPlaying = true;
            GameManager.IsStart = false;
        }

        if (transform.position.y < -4.5f) {

            Damage();
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(m_rb.velocity);
        if (IsPlaying)
        {
            //    Vector2 vec = new Vector2(Mathf.Abs(m_rb.velocity.normalized.x), Mathf.Abs(m_rb.velocity.normalized.y));
            //    float cos = Vector2.Dot(vec, Vector2.zero);
            //    float degree = Mathf.Acos(cos) / Mathf.Deg2Rad;

            // //   if (degree < 0.15f || degree > 88.5f) m_rb.velocity = new Vector2(-3.0f, 5.2f);


            if (m_rb.velocity.x <= 0.1f && m_rb.velocity.x >= -0.1f) { m_rb.velocity = new Vector2(4.0f, -5.0f); Debug.Log("horizontal"); }
            if (m_rb.velocity.y >= -0.2f && m_rb.velocity.y <= 0.2f) { m_rb.velocity = new Vector2(-3.0f, 5.2f); Debug.Log("vertical"); }
        }
        ////hit RL
        //if (collision.transform.tag == "side")
        //{
        //    m_rb.velocity = new Vector2(-speedX, speedY);
        //    speedX = -speedX;
        //}

        ////hit TOP or PLANE
        //if (collision.transform.tag == "top" || collision.transform.tag == "plane")
        //{
        //    m_rb.velocity = new Vector2(speedX, -speedY);
        //    speedY = -speedY;
        //}

        //if (m_rb.velocity.x < 0.2f || m_rb.velocity.y < 0.2f)
        //{
        //    m_rb.velocity += new Vector2(3, 3);
        //}
    }

    public void BallReset()
    {
        GameObject clone = Instantiate(GameObject.FindGameObjectWithTag("ball"));
        clone.transform.localScale = new Vector2(0.3f, 0.3f);
        clone.transform.position = new Vector2(plane.transform.position.x, -4.0f);
        clone.transform.SetParent(plane.transform);


        m_rb.velocity = Vector2.zero;

        m_collider.enabled = false;
        GameManager.Score -= 100;
        GameManager.IsReady = true;
        GameManager.IsStart = false;
        IsPlaying = false;
        GameManager.Hp = 1;

    }

    public void Speed(float n)
    {

        float speedx = m_rb.velocity.x * n;
        float speedy = m_rb.velocity.y * n;
        m_rb.velocity = new Vector2(speedx, speedy);
    }

    public bool GameStart()
    {
        gameObject.transform.parent = null;
        m_collider.enabled = true;
        m_rb.velocity = new Vector2(m_speedX, m_speedX);
        return true;
    }

    void Damage()
    {
        GameManager.Hp--;

        if (GameManager.Hp == 0)
        {
            GameManager.PlaneHP = GameManager.PlaneHP - 1;
            GameManager.Down();
            if (GameManager.PlaneHP <= 0) SceneManager.LoadScene("Over");
            else BallReset();
        }

    }

    public void Clone()
    {
        GameObject clone = Instantiate(GameObject.FindGameObjectWithTag("ball"));
        //clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(10, 13));
        clone.transform.localScale = new Vector2(0.3f, 0.3f);
        clone.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 7);
        GameManager.Hp++;
    }

   

    public void Pause()
    {
        s_ball_v = m_rb.velocity;
        m_rb.velocity = Vector2.zero;
    }

    public void ContinuePlay()
    {
        m_rb.velocity = s_ball_v;
    }
}
