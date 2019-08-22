using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterController : MonoBehaviour
{
    //IntPtr myintptr;
    //RECT rect;
    float _w_h;

    float _currentWidth;
    float _currentHeight;

    float _minWidth = 1536;
    float _minheight = 864;

    
    IEnumerator Start()
    {
        _currentHeight = 720;
        _currentWidth = 1280;
        Screen.SetResolution((int)_currentWidth, (int)_currentHeight, false);
        while (DBInitController.GetInstance.DB == null)
        {
            yield return 0;
        }
        
        _w_h = _currentWidth / _currentHeight;                                        //窗口横纵比例				//窗口的高度

        UserManager.Instance.Init();
        ModelDataManager.GetInstance.Init();
        //Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        SceneManager.LoadScene(Constant.LoginSceneName);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        if (Screen.fullScreen)
        {
            if(GUI.Button(new Rect(Screen.width - 18, -2, 20, 20), "-"))
            {
                Screen.fullScreen = false;
            }
        }
    }

    void LateUpdate()
    {
        //if(Screen.width != (int)_currentWidth ||
        //    Screen.height != (int)_currentHeight)
        //{
        //    SetWindow();
        //}
    }

    void SetWindow()
    {
        if(_currentWidth != Screen.width)
        {
            _currentWidth = Screen.width;
            _currentWidth = (_currentWidth < _minWidth) ? _minWidth : _currentWidth;
            _currentHeight = _currentWidth / _w_h;
        }
        else if(_currentHeight != Screen.height)
        {
            _currentHeight = Screen.height;
            _currentHeight = (_currentHeight < _minheight) ? _minheight : _currentHeight;
            _currentWidth = _currentHeight * _w_h;
        }
        Screen.SetResolution((int)_currentWidth, (int)_currentHeight, Screen.fullScreen);
    }

    private void OnDestroy()
    {
        
    }
}
