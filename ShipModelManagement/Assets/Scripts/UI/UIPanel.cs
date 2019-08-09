﻿#region 描述
//-----------------------------------------------------------------------------
// 类 名 称: UIPanel
// 作    者：zhangfan
// 创建时间：2019/8/1 10:32:16
// 描    述：
// 版    本：
//-----------------------------------------------------------------------------
// Copyright (C) 2017-2019 零境科技有限公司
//-----------------------------------------------------------------------------
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

abstract public class UIPanel
{
    protected Dictionary<string, UIWindow> _windows = new Dictionary<string, UIWindow>();

    protected Transform _transform;

    public void OnInit(string name)
    {
        var obj = GameObject.Instantiate(Resources.Load("UI/" + name)) as GameObject;
        _transform = obj.transform;
        OnBegin();
    }

    virtual public void OnBegin()
    {

    }

    virtual public void OnOpen(params object[] datas)
    {
        _transform.gameObject.SetActive(true);
    }

    virtual public void OnClose()
    {
        _transform.gameObject.SetActive(false);
        foreach(var window in _windows)
        {
            window.Value.OnClose();
        }
    }

    public T OpenWindow<T>(params object[] datas) where T : UIWindow, new()
    {
        var uiname = typeof(T).Name;
        if(!_windows.ContainsKey(uiname))
        {
            var window = new T();

            window.OnInit(uiname, _transform);
            window.OnOpen(datas);

            _windows.Add(uiname, window);

            return window;
        }
        var ret = _windows[uiname];
        ret.OnOpen(datas);
        return ret as T;
    }

    public void CloseWindow<T>()
    {
        var uiname = typeof(T).Name;
        if (_windows.ContainsKey(uiname))
        {
            _windows[uiname].OnClose();
        }
    }
}
