﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lastwall : MonoBehaviour
{
    public bool isin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isin = true;
        }
    }
}
