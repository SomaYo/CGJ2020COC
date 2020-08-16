using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level4camera : MonoBehaviour
{
    public GameObject ncamera;
    public GameObject fakemenu;
    public GameObject wall2;
    public GameObject loli;
    public GameObject goal;
    public bool start;
    public bool pause;
    public bool goon;
    public bool finish;

    public int cntmax;
    int cnt;
    GameObject mainCamera;
    public GameObject windowcurtain;
    Vector3 postionnow;
    // Start is called before the first frame update
    void Start()
    {
        start = false;
        pause = false;
        goon = false;
        ncamera = GameObject.Find("CM vcam1");
        mainCamera = GameObject.Find("Main Camera");
        cnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ncamera == null)
        {
            ncamera = GameObject.Find("CM vcam1");
        }
        if (goon)
        {
            if (wall2.GetComponent<Lastwall>().isin)
            {
                cnt++;
                if (cnt <= cntmax)
                {
                    float ftemp = cnt / 100.0f;
                    if (ftemp > 0.12f)
                    {
                        ftemp = 0.12f;
                    }
                    float x = UnityEngine.Random.Range(-ftemp, ftemp);
                    float y = UnityEngine.Random.Range(-ftemp, ftemp);
                    mainCamera.transform.position = new Vector3(x, y, 0) + postionnow;
                }
                else
                {
                    foreach (Transform tran in loli.GetComponentsInChildren<Transform>())
                    {//遍历当前物体及其所有子物体
                        tran.gameObject.layer = 11;//更改物体的Layer层
                        finish = true;
                        wall2.SetActive(false);
                        goal.SetActive(true);
                    }
                }
            }

        }
        else if (start)
        {
            if (!pause)
            {
                ncamera.SetActive(false);
                float x = UnityEngine.Random.Range(-0.08f, 0.08f);
                float y = UnityEngine.Random.Range(-0.08f, 0.08f);
                mainCamera.transform.position = new Vector3(x, y, 0) + postionnow;
            }
            else
            {
                fakemenu.SetActive(true);
                fakemenu.GetComponent<BoxCollider2D>().enabled = false;
                cnt++;
                if (cnt > cntmax)
                {
                    Time.timeScale = 1;
                    goon = true;
                    fakemenu.GetComponent<BoxCollider2D>().enabled = true;
                    windowcurtain.transform.position = postionnow;
                    wall2.SetActive(true);
                    cnt = 0;
                }
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            start = true;
            postionnow = gameObject.transform.position;
        }
        
    }
}
