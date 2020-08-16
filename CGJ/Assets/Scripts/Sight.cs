using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public float lenght = 3;
    private bool right;
    private float i = 1;
    private string targettag;
    //private Collider2D coll;
    private bool ishiting = false;

    void Start()
    {
        right = true;
    }

    void Update()
    {
        ishiting = false;
        SightMove();
    }

    void SightMove()
    {
        Vector3 front = new Vector3(1, 0, 0);
        Vector3 beg = transform.position + new Vector3(0, 0, 0);
        Debug.DrawLine(beg, beg + front * 1.5f, Color.red, 1000);
        RaycastHit hit;
        //向右走时
        if (Input.GetKeyDown(KeyCode.D))
        {
            i = 1;
            while (i < 6)
            {
                if (Physics.Raycast(beg, front,out hit,lenght, LayerMask.GetMask("Placement")))
                {
                    targettag = hit.collider.tag;
                    ishiting = true;
                    //破碎(此处判定家具破碎，下同)
                    //Destroy(GetComponent<BoxCollider2D>());
                    Debug.Log(targettag);
                }

                front = new Vector3(1, i * 0.1f, 0);
                if (Physics.Raycast(beg, front, out hit, lenght, LayerMask.GetMask("Placement")))
                {
                    targettag = hit.collider.tag;
                    ishiting = true;
                    //破碎
                    Debug.Log(targettag);
                }

                front = new Vector3(1, i * -0.1f, 0);
                if (Physics.Raycast(beg, front, out hit, lenght, LayerMask.GetMask("Placement")))
                {
                    targettag = hit.collider.tag;
                    ishiting = true;
                    //破碎
                    Debug.Log(targettag);
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
                if (Physics2D.Raycast(beg, front, lenght, LayerMask.GetMask("Placement")))
                {

                    //破碎
                }

                front = new Vector3(1, i * 0.1f, 0);
                if (Physics2D.Raycast(beg, front, lenght, LayerMask.GetMask("Placement")))
                {
                    //破碎
                }

                front = new Vector3(1, i * -0.1f, 0);
                if (Physics2D.Raycast(beg, front, lenght, LayerMask.GetMask("Placement")))
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
                    if (Physics2D.Raycast(beg, front, lenght, LayerMask.GetMask("Placement")))
                    {
                        //破碎
                    }

                    front = new Vector3(1 - i * 0.1f, 1, 0);
                    if (Physics2D.Raycast(beg, front, lenght, LayerMask.GetMask("Placement")))
                    {
                        //破碎
                    }

                    front = new Vector3(1, 1 - i * 0.1f, 0);
                    if (Physics2D.Raycast(beg, front, lenght, LayerMask.GetMask("Placement")))
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
                    if (Physics2D.Raycast(beg, front, lenght, LayerMask.GetMask("Placement")))
                    {
                        //破碎
                    }

                    front = new Vector3(-1 + i * 0.1f, 1, 0);
                    if (Physics2D.Raycast(beg, front, lenght, LayerMask.GetMask("Placement")))
                    {
                        //破碎
                    }

                    front = new Vector3(-1, 1 - i * 0.1f, 0);
                    if (Physics2D.Raycast(beg, front, lenght, LayerMask.GetMask("Placement")))
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
                    if (Physics2D.Raycast(beg, front, lenght, LayerMask.GetMask("Placement")))
                    {
                        //破碎
                    }

                    front = new Vector3(1 - i * 0.1f, -1, 0);
                    if (Physics2D.Raycast(beg, front, lenght, LayerMask.GetMask("Placement")))
                    {
                        //破碎
                    }

                    front = new Vector3(1, i * 0.1f - 1, 0);
                    if (Physics2D.Raycast(beg, front, lenght, LayerMask.GetMask("Placement")))
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
                    if (Physics2D.Raycast(beg, front, lenght, LayerMask.GetMask("Placement")))
                    {
                        //破碎
                    }

                    front = new Vector3(-1 + i * 0.1f, -1, 0);
                    if (Physics2D.Raycast(beg, front, lenght, LayerMask.GetMask("Placement")))
                    {
                        //破碎
                    }

                    front = new Vector3(-1, i * 0.1f - 1, 0);
                    if (Physics2D.Raycast(beg, front, lenght, LayerMask.GetMask("Placement")))
                    {
                        //破碎
                    }

                    i++;
                }
            }
        }


    }

    public string Gettag(){
        if (!ishiting)
        {
            string temp = "none";
            return temp;
        }
        return targettag;

    }
}