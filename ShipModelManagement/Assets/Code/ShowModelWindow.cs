#region 描述
//-----------------------------------------------------------------------------
// 类 名 称: ShowModelWindow
// 作    者：zhangfan
// 创建时间：2019/8/1 11:07:51
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

public class ShowModelWindow : UIWindow
{
    public override void OnOpen()
    {
        base.OnOpen();

        Debug.Log("open ShowModelWindow");
    }

    public override void OnClose()
    {
        base.OnClose();

        Debug.Log("close ShowModelWindow");
    }
}
