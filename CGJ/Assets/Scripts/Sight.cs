using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    private bool right;
    private float i = 1;

    void Start()
    {
        right = true;

        StartCoroutine(SightMove());
    }

    void Update()
    {
    }

    IEnumerator SightMove()
    {
        yield return new WaitForSeconds(0.2f);
        Vector3 front = new Vector3(1, 0, 0);
        Vector3 beg = transform.position + new Vector3(0, 0, 0);
        Debug.DrawLine(beg, beg + front * 1.5f, Color.red, 1000);
        //向右走时
        if (Input.GetKeyDown(KeyCode.D))
        {
            i = 1;
            while (i < 6)
            {
                if (Physics2D.Raycast(beg, front, 5f, LayerMask.GetMask("Ground")))
                {
                    //破碎
                }

                front = new Vector3(1, i * 0.1f, 0);
                if (Physics2D.Raycast(beg, front, 5f, LayerMask.GetMask("Ground")))
                {
                    //破碎
                }

                front = new Vector3(1, i * -0.1f, 0);
                if (Physics2D.Raycast(beg, front, 5f, LayerMask.GetMask("Ground")))
                {
                    //破碎
                }

                i++;
            }

            right = true;
        }
        else if (Input.GetKeyDown(KeyCode.A)) //向左走时
        {
            front = new Vector3(-1, 0, 0);
            i = 1;
            while (i < 6)
            {
                if (Physics2D.Raycast(beg, front, 5f, LayerMask.GetMask("Ground")))
                {
                    //破碎
                }

                front = new Vector3(1, i * 0.1f, 0);
                if (Physics2D.Raycast(beg, front, 5f, LayerMask.GetMask("Ground")))
                {
                    //破碎
                }

                front = new Vector3(1, i * -0.1f, 0);
                if (Physics2D.Raycast(beg, front, 5f, LayerMask.GetMask("Ground")))
                {
                    //破碎
                }

                i++;
            }

            right = false;
        }
        else if (Input.GetKeyDown(KeyCode.W)) //抬头时
        {
            if (right) //面向右侧
            {
                front = new Vector3(1, 1, 0);
                i = 1;
                while (i < 6)
                {
                    if (Physics2D.Raycast(beg, front, 5f, LayerMask.GetMask("Ground")))
                    {
                        //破碎
                    }

                    front = new Vector3(1 - i * 0.1f, 1, 0);
                    if (Physics2D.Raycast(beg, front, 5f, LayerMask.GetMask("Ground")))
                    {
                        //破碎
                    }

                    front = new Vector3(1, 1 - i * 0.1f, 0);
                    if (Physics2D.Raycast(beg, front, 5f, LayerMask.GetMask("Ground")))
                    {
                        //破碎
                    }

                    i++;
                }
            }
            else //面向左侧
            {
                front = new Vector3(-1, 1, 0);
                i = 1;
                while (i < 6)
                {
                    if (Physics2D.Raycast(beg, front, 5f, LayerMask.GetMask("Ground")))
                    {
                        //破碎
                    }

                    front = new Vector3(-1 + i * 0.1f, 1, 0);
                    if (Physics2D.Raycast(beg, front, 5f, LayerMask.GetMask("Ground")))
                    {
                        //破碎
                    }

                    front = new Vector3(-1, 1 - i * 0.1f, 0);
                    if (Physics2D.Raycast(beg, front, 5f, LayerMask.GetMask("Ground")))
                    {
                        //破碎
                    }

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
                    if (Physics2D.Raycast(beg, front, 5f, LayerMask.GetMask("Ground")))
                    {
                        //破碎
                    }

                    front = new Vector3(1 - i * 0.1f, -1, 0);
                    if (Physics2D.Raycast(beg, front, 5f, LayerMask.GetMask("Ground")))
                    {
                        //破碎
                    }

                    front = new Vector3(1, i * 0.1f - 1, 0);
                    if (Physics2D.Raycast(beg, front, 5f, LayerMask.GetMask("Ground")))
                    {
                        //破碎
                    }

                    i++;
                }
            }
            else //面向左侧
            {
                front = new Vector3(-1, -1, 0);
                i = 1;
                while (i < 6)
                {
                    if (Physics2D.Raycast(beg, front, 5f, LayerMask.GetMask("Ground")))
                    {
                        //破碎
                    }

                    front = new Vector3(-1 + i * 0.1f, -1, 0);
                    if (Physics2D.Raycast(beg, front, 5f, LayerMask.GetMask("Ground")))
                    {
                        //破碎
                    }

                    front = new Vector3(-1, i * 0.1f - 1, 0);
                    if (Physics2D.Raycast(beg, front, 5f, LayerMask.GetMask("Ground")))
                    {
                        //破碎
                    }

                    i++;
                }
            }
        }

        yield return null;
    }
}