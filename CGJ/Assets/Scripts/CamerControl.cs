using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerControl : MonoBehaviour
{
    public Transform target;
    //边界
    public float MinX;
    public float MaxX;


    void Start()
    {

    }

    void Update()
    {
        Vector3 v = transform.position;
        v.x = target.position.x;
        //边界判断
        if (v.x > MaxX)
        {
            v.x = MaxX;
        }
        else if (v.x < MinX)
        {
            v.x = MinX;
        }
        transform.position = v;
    }
}
