﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterController : MonoBehaviour
{
    IEnumerator Start()
    {
        Screen.SetResolution(1920, 1080,false);
        while (DBInitController.GetInstance.DB == null)
        {
            yield return 0;
        }
        UserManager.Instance.Init();
        ModelDataManager.GetInstance.Init();
        //Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        SceneManager.LoadScene(Constant.LoginSceneName);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
