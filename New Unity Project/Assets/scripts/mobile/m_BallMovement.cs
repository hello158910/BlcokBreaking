using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_BallMovement : MonoBehaviour {

    [SerializeField]
    AudioClip []m_powerSounds;

    Rigidbody2D m_rb;
  //  CircleCollider2D m_collider;
    GameObject m_plane;
    Color m_norColor, m_powColor;
    bool m_once = true;
    Vector2 m_ball_v;
    // finger trace
    Vector2 m_start, m_end;
    GameObject m_fire;

    const float SPEED_X = -2.5f;
    const float SPEED_Y = 3.5F;
    const float BALL_INITPOSY = -4.0f;
    const float MIN_Y_POS = -4.5f;
    const float BALLSIZE = 0.3f;
    const float SPEED = 250;

    public static bool ToNextLevel = false;

    // Use this for initialization
    void Start() {
        m_fire = gameObject.GetComponentInChildren<ParticleSystem>().gameObject;
        var emi = m_fire.GetComponentInChildren<ParticleSystem>().emission;
        emi.enabled = false;

        m_powColor = new Color(0.5f, 0.2f, 0.5f);
        m_norColor = new Color(0.5f, 0.5f, 0.2f);
        m_rb = GetComponent<Rigidbody2D>();
        m_plane = GameObject.FindGameObjectWithTag("player");
        gameObject.GetComponent<SpriteRenderer>().color = m_norColor;

    }

   
    // Update is called m_once per frame
    void Update() {

        // ----lose ball
        if (transform.position.y < MIN_Y_POS) {

            Damage();
            Destroy(gameObject);
        }

        //  ----power

        if (!M_GameManager.Isnormal && m_once)
        {
            PowerUp(); m_once = false;
        }
        else if (M_GameManager.Isnormal && !m_once)
        {
            PowerDown(); m_once = true;
        }

        if (ToNextLevel)
        {
            SetLevelBall();
            ToNextLevel = false;
        }
    }

    private void OnMouseDown()
    {
        m_start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        m_end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 v = m_end - m_start;
        if (v != Vector2.zero)
        {
            if (GameStart(v.normalized * SPEED * Time.deltaTime))
            {

                M_GameManager.IsStart = true;
                M_GameManager.IsPause = false;
                M_GameManager.IsReady = false;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (M_GameManager.IsStart)
        {
            //    Vector2 vec = new Vector2(Mathf.Abs(m_rb.velocity.normalized.x), Mathf.Abs(m_rb.velocity.normalized.y));
            //    float cos = Vector2.Dot(vec, Vector2.zero);
            //    float degree = Mathf.Acos(cos) / Mathf.Deg2Rad;

            // //   if (degree < 0.15f || degree > 88.5f) m_rb.velocity = new Vector2(-3.0f, 5.2f);


            if (m_rb.velocity.x <= 0.1f && m_rb.velocity.x >= -0.1f) { m_rb.velocity = new Vector2(4.0f, -5.0f) * 50 * Time.deltaTime; }
            if (m_rb.velocity.y >= -0.2f && m_rb.velocity.y <= 0.2f) { m_rb.velocity = new Vector2(-3.0f, 5.2f) * 50 * Time.deltaTime; }
        }

        if (collision.gameObject.CompareTag("player")) collision.gameObject.GetComponent<M_PlaneControler>().PlayBounceSound();
        else if (collision.gameObject.CompareTag("brick")|| collision.transform.CompareTag("stone")) {
            gameObject.GetComponent<AudioSource>().clip = m_powerSounds[0];
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    void Damage()
    {
        M_GameManager.Hp--;

        if (M_GameManager.Hp == 0)
        {
            M_GameManager.PlaneHp = M_GameManager.PlaneHp - 1;
            M_GameManager.Down();
            if (M_GameManager.PlaneHp <= 0){ M_Score.Win = false; SceneManager.LoadScene("m_Over"); }
            else BallReset();
        }

    }

    public void BallReset()
    {
        GameObject clone = Instantiate(GameObject.FindGameObjectWithTag("m_ball"));
        clone.transform.localScale = new Vector2(BALLSIZE, BALLSIZE);
        clone.transform.position = new Vector2(m_plane.transform.position.x, BALL_INITPOSY);
        clone.transform.SetParent(m_plane.transform);


        m_rb.velocity = Vector2.zero;

        M_GameManager.IsReady = true;
        M_GameManager.IsStart = false;
        M_GameManager.Hp = 1;

    }

    public bool GameStart(Vector2 speed)
    {
        gameObject.transform.parent = null;
        m_rb.velocity = speed;
        
        return true;
    }

    public void Speed(float n)
    {

        float speedx = m_rb.velocity.x * n;
        float speedy = m_rb.velocity.y * n;
        m_rb.velocity = new Vector2(speedx, speedy);
    }

    public void Clone()
    {
        GameObject clone = Instantiate(GameObject.FindGameObjectWithTag("m_ball"));
        clone.GetComponent<SpriteRenderer>().color = m_norColor;
        clone.transform.localScale = new Vector2(BALLSIZE, BALLSIZE);
        clone.GetComponent<Rigidbody2D>().velocity = new Vector2(SPEED_X,SPEED_Y);
        M_GameManager.Hp++;
    }

    public void Pause()
    {
        m_ball_v = m_rb.velocity;
        m_rb.velocity = Vector2.zero;
    }

    public void ContinuePlay()
    {
        m_rb.velocity = m_ball_v;
    }

    public void PowerUp()
    {
        gameObject.GetComponent<SpriteRenderer>().color = m_powColor;
        Speed(1.2f);
        gameObject.GetComponent<Collider2D>().isTrigger = true;

        var emi = m_fire.GetComponentInChildren<ParticleSystem>().emission;
        emi.enabled= true;
    }

    public void PowerDown()
    {
        gameObject.GetComponent<SpriteRenderer>().color = m_norColor;
        Speed(0.8f);
        gameObject.GetComponent<Collider2D>().isTrigger = false;

        var emi = m_fire.GetComponentInChildren<ParticleSystem>().emission;
        emi.enabled= false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("brick")) 
        {
            collision.GetComponent<M_BrickSetting>().BreakBlock();
            gameObject.GetComponent<AudioSource>().clip = m_powerSounds[1];
            gameObject.GetComponent<AudioSource>().Play();
        }
        //hit RL
        else if (collision.transform.CompareTag("side")) 
        {
            m_rb.velocity = new Vector2(-m_rb.velocity.x, m_rb.velocity.y);
        }

        //hit TOP or m_plane
        else if (collision.transform.CompareTag("top") || collision.transform.CompareTag("player")|| collision.transform.CompareTag("stone")) 
        {
            m_rb.velocity = new Vector2(m_rb.velocity.x, -m_rb.velocity.y);
      
        }
    }

    public void SetLevelBall()
    {
        //Destroy(gameObject);
        //BallReset();
        GameObject[] balls = GameObject.FindGameObjectsWithTag("m_ball");
        BallReset();  
        for (int i = 0; i < balls.Length; i++)
        {
            Destroy(balls[i]);
        }
    }
}
