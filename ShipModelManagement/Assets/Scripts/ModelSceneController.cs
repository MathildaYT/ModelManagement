﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.getInstance.Open<ModelRegister>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
