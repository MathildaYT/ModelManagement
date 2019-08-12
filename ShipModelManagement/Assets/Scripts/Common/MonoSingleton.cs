#region 描述
//-----------------------------------------------------------------------------
// 类 名 称: MonoSingleton
// 作    者：zhangfan
// 创建时间：2019/8/12 10:29:39
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

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T ms_instance;
    public static T Instance
    {
        get
        {
            if (ms_instance == null)
            {
                ms_instance = Instantiate();
            }

            return ms_instance;
        }
    }

    protected static T Instantiate()
    {
        if (ms_instance == null)
        {
            ms_instance = (T)FindObjectOfType(typeof(T));
            if (FindObjectsOfType(typeof(T)).Length > 1)
            {
                return ms_instance;
            }

            if (ms_instance == null)
            {
                GameObject singleton = new GameObject("[Singleton]" + typeof(T).Name);
                if (singleton != null)
                {
                    ms_instance = singleton.AddComponent<T>();
                    ms_instance.InitSingleton();
                }

            }
        }

        return ms_instance;
    }

    protected virtual void InitSingleton()
    {

    }

    private void Awake()
    {
        if (ms_instance == null)
        {
            ms_instance = this as T;
        }
    }

    public void OnApplicationQuit()
    {
        ms_instance = null;
    }
}