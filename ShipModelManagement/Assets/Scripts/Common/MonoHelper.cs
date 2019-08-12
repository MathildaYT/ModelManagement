#region 描述
//-----------------------------------------------------------------------------
// 类 名 称: MonoHelper
// 作    者：zhangfan
// 创建时间：2019/8/12 10:28:59
// 描    述：
// 版    本：
//-----------------------------------------------------------------------------
// Copyright (C) 2017-2019 零境科技有限公司
//-----------------------------------------------------------------------------
#endregion
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public delegate void MonoUpdaterEvent();

public class MonoHelper : MonoSingleton<MonoHelper>
{
    //===========================================================

    private event MonoUpdaterEvent UpdateEvent;
    private event MonoUpdaterEvent FixedUpdateEvent;
    private event MonoUpdaterEvent LateUpdateEvent;

    public static void AddUpdateListener(MonoUpdaterEvent listener)
    {
        if (Instance != null)
        {
            Instance.UpdateEvent += listener;
        }
    }

    public static void RemoveUpdateListener(MonoUpdaterEvent listener)
    {
        if (Instance != null)
        {
            Instance.UpdateEvent -= listener;
        }
    }

    public static void AddFixedUpdateListener(MonoUpdaterEvent listener)
    {
        if (Instance != null)
        {
            Instance.FixedUpdateEvent += listener;
        }
    }

    public static void RemoveFixedUpdateListener(MonoUpdaterEvent listener)
    {
        if (Instance != null)
        {
            Instance.FixedUpdateEvent -= listener;
        }
    }

    public static void AddLateUpdateListener(MonoUpdaterEvent listener)
    {
        if (Instance != null)
        {
            Instance.LateUpdateEvent += listener;
        }
    }

    public static void RemoveLateUpdateListener(MonoUpdaterEvent listener)
    {
        if (Instance != null)
        {
            Instance.LateUpdateEvent -= listener;
        }
    }

    void Update()
    {
        if (UpdateEvent != null)
        {
            try
            {
                UpdateEvent();
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("Update() Error:{0}\n{1}", e.Message, e.StackTrace);
            }
        }
    }

    void FixedUpdate()
    {
        if (FixedUpdateEvent != null)
        {
            try
            {
                FixedUpdateEvent();
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("FixedUpdate() Error:{0}\n{1}", e.Message, e.StackTrace);
            }
        }
    }

    private void LateUpdate()
    {
        if (LateUpdateEvent != null)
        {
            try
            {
                LateUpdateEvent();
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("LateUpdateEvent() Error:{0}\n{1}", e.Message, e.StackTrace);
            }
        }
    }

    //===========================================================

    public new static void StartCoroutine(IEnumerator routine)
    {
        MonoBehaviour mono = Instance;
        mono.StartCoroutine(routine);
    }

    public new static void StopCoroutine(IEnumerator routine)
    {
        MonoBehaviour mono = Instance;
        mono.StopCoroutine(routine);
    }
}
