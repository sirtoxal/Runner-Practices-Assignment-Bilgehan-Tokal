﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform CameraPosTar;
    public Transform LookTar;

    float time = 0.1f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        


        transform.position = Vector3.Lerp(transform.position, CameraPosTar.position, time);
        transform.LookAt(LookTar);



    }
}
