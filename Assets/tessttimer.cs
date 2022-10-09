using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tessttimer : MonoBehaviour
{
    private float floatTime = 2;//用于方法一：用来递减delatime的2秒
    private float floatTime1 = 0;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        PrintTime();
    }
    void PrintTime()
    {
        floatTime -= Time.deltaTime;
        floatTime1 += Time.deltaTime;

        if (floatTime1 > 1)
        {
            Debug.Log("Now time is " + Mathf.RoundToInt(Time.time));
            floatTime1 = 0;
        }

    }
}
