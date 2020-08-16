using Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public float lenght = 3;
    private bool right;
    private bool open;
    private float i = 1;

    void Start()
    {
        right = true;
        open = true;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow))
        {
            right = true;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            right = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            open = !open;
        }
        SightMove();
    }

    void SightMove()
    {
        if (open)
        {
            Vector3 front = new Vector3(1, 0, 0);
            Vector3 beg = transform.position + new Vector3(0, 0, 0);
            Debug.DrawLine(beg, beg + front * 1.5f, Color.red, 1000);
            //向右走时
            if (right)
            {
                i = 1;
                while (i < 6)
                {
                    Raycheck(beg, front);

                    front = new Vector3(1, i * 0.1f, 0);
                    Raycheck(beg, front);


                    front = new Vector3(1, i * -0.1f, 0);
                    Raycheck(beg, front);
                    i++;
                }

            }
            else if (!right) //向左走时
            {
                front = new Vector3(-1, 0, 0);
                i = 1;
                while (i < 6)
                {
                    Raycheck(beg, front);

                    front = new Vector3(1, i * 0.1f, 0);
                    Raycheck(beg, front);

                    front = new Vector3(1, i * -0.1f, 0);
                    Raycheck(beg, front);

                    i++;
                }

            }
            if (Input.GetKeyDown(KeyCode.W)) //抬头时
            {
                if (right) //面向右侧
                {
                    front = new Vector3(1, 1, 0);
                    i = 1;
                    while (i < 6)
                    {
                        Raycheck(beg, front);

                        front = new Vector3(1 - i * 0.1f, 1, 0);
                        Raycheck(beg, front);

                        front = new Vector3(1, 1 - i * 0.1f, 0);
                        Raycheck(beg, front);

                        i++;
                    }
                }
                else //面向左侧
                {
                    front = new Vector3(-1, 1, 0);
                    i = 1;
                    while (i < 6)
                    {
                        Raycheck(beg, front);

                        front = new Vector3(-1 + i * 0.1f, 1, 0);
                        Raycheck(beg, front);

                        front = new Vector3(-1, 1 - i * 0.1f, 0);
                        Raycheck(beg, front);

                        i++;
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.S)) //低头时
            {
                if (right) //面向右侧
                {
                    front = new Vector3(1, -1, 0);
                    i = 1;
                    while (i < 6)
                    {
                        Raycheck(beg, front);

                        front = new Vector3(1 - i * 0.1f, -1, 0);
                        Raycheck(beg, front);

                        front = new Vector3(1, i * 0.1f - 1, 0);
                        Raycheck(beg, front);

                        i++;
                    }
                }
                else //面向左侧
                {
                    front = new Vector3(-1, -1, 0);
                    i = 1;
                    while (i < 6)
                    {
                        Raycheck(beg, front);

                        front = new Vector3(-1 + i * 0.1f, -1, 0);
                        Raycheck(beg, front);

                        front = new Vector3(-1, i * 0.1f - 1, 0);
                        Raycheck(beg, front);

                        i++;
                    }
                }
            }
        }
       
    }

    void Raycheck(Vector3 mbeg,Vector3 mfront)
    {
        RaycastHit2D hit;
        hit= Physics2D.Raycast(mbeg, mfront, lenght, LayerMask.GetMask("Placement"));
        if (hit.collider == null)
        {
            return ;
            
        }
        else if (hit.collider.CompareTag("Placement"))
        {
            hit.collider.GetComponent<Placement>().Break();
        }
    }
}