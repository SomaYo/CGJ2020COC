using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    WhitePlayer = 0,
    DarkPlayer = 1,
}

public class Player : MonoBehaviour
{
    private static Player _Instance;

    private void OnEnable()
    {
        _Instance = this;
    }

    public static Player Get()
    {
        return _Instance;
    }

    public float moveSpeed = 3;
    public float climbSpeed = 3;
    public int Hp = 1;
    public LayerMask groundLayer;
    Rigidbody2D player;
    private bool onWall;
    private bool onGround;
    private bool onFly;
    private bool isWall;
    private float collisionRadius = 0.15f;
    private Collider2D coll;
    public GameObject[] players = null;
    public PlayerType curPlayerType = PlayerType.DarkPlayer;
    private Animator[] animators = new Animator[2];

    void Start()
    {
        player = transform.GetComponent<Rigidbody2D>();
        coll = transform.GetComponent<Collider2D>();
        for (int i = 0; i < players.Length; i++)
        {
            animators[i] = players[i].GetComponent<Animator>();
        }
        groundLayer = 1 << 8;
        onWall = false;
        onGround = true;
        onFly = false;
        isWall = false;
    }

    void Update()
    {
        if (Hp <= 0)
        {
            return;
        }
        Move();
        Climb();
    }

    void Sight(Vector3 front, Vector3 down)//睁眼时视线
    {
        //Vector3 front = new Vector3(1, 0, 0);
        Vector3 beg = transform.position;
        //Vector3 down = new Vector3(0, 1, 0);
        Debug.DrawLine(beg, beg + front+down * 10, Color.red,1000);
        bool isPlacement = Physics2D.Raycast(beg, front+down, 10, LayerMask.GetMask("Placement"));
        if (isPlacement)
        {
            //家具破碎
        }
    }

    private void Move()//水平移动
    {

        Vector3 front = new Vector3(1, 0, 0);
        Vector3 down = new Vector3(0, 0, 0);
        float h = Input.GetAxisRaw("Horizontal");
        if (h != 0)
        {
            foreach (var player in players)
            {
                if (h > 0)
                {
                    player.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (h < 0)
                {
                    player.transform.eulerAngles = new Vector3(0, 180, 0);
                }
            }
            animators[(int)curPlayerType].SetBool("isWalk",true);
            player.velocity = new Vector2(h * moveSpeed, player.velocity.y);
            if(h>0)//往左看
            {
                front = new Vector3(1, 0, 0);
                Sight(front, down);
            }
            else if(h<0)//往右看
            {
                front = new Vector3(-1, 0, 0);
                Sight(front, down);
            }


        }
        else
        {
            animators[(int)curPlayerType].SetBool("isWalk", false);
            SightMove();
        }

    }

    void SightMove()
    {
        Vector3 front = new Vector3(1, 0, 0);
        if (Input.GetKeyDown(KeyCode.W))
        {
            //看斜上方
            Vector3 down = new Vector3(0, 1, 0);
            Sight(front,down);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //看斜下方
            Vector3 down = new Vector3(0, -1, 0);
            Sight(front,down);
        }

    }

    void Climb()//攀爬 
    {
        if (onGround)
        {
            if (isWall == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    animators[(int)curPlayerType].SetBool("isClimb", true);
                    onGround = false;
                    onWall = true;
                    player.velocity = new Vector2(player.velocity.x, climbSpeed);
                    if (isWall == false)
                    {
                        player.velocity = new Vector2(player.velocity.x, player.velocity.y);
                    }
                }
            }
            else
            {
                onGround = true;
                onWall = false;
            }
        }
        else if (onWall)
        {
            if (isWall == true)
            {
                animators[(int)curPlayerType].SetBool("isClimb", true);
                player.velocity = new Vector2(player.velocity.x, climbSpeed);
            }
            else
            {
                animators[(int)curPlayerType].SetBool("isClimb", false);
                onGround = true;
                onWall = false;
            }
        }

    }

    public void SwitchPlayerShow(bool isDark)
    {
        if (isDark)
        {
            players[(int)curPlayerType].SetActive(false);
            curPlayerType = PlayerType.WhitePlayer;
            players[(int)curPlayerType].SetActive(true);
        }
        else
        {
            players[(int)curPlayerType].SetActive(false);
            curPlayerType = PlayerType.DarkPlayer;
            players[(int)curPlayerType].SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")//碰到怪物死亡
        {
            Hp--;
            if (Hp <= 0)
            {
                //死亡之后

            }
        }
        else if (collision.collider.tag == "Placement")//碰到家具
        {
            isWall = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Placement")//碰到家具
        {
            isWall = false ;
        }
    }
}
