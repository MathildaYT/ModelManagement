#region 描述
//-----------------------------------------------------------------------------
// 类 名 称: UIBar
// 作    者：zhangfan
// 创建时间：2019/8/8 10:43:18
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

public class UIBar
{
    protected Transform _transform;
    public void OnInit(string name)
    {
        var obj = GameObject.Instantiate(Resources.Load("UI/Bar/" + name)) as GameObject;
        _transform = obj.transform;
    }

    virtual public void OnOpen()
    {
        _transform.gameObject.SetActive(true);
    }

    virtual public void OnClose()
    {
        _transform.gameObject.SetActive(false);
    }
}
