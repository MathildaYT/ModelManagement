#region 描述
//-----------------------------------------------------------------------------
// 类 名 称: ConfirmWnd
// 作    者：zhangfan
// 创建时间：2019/8/9 17:54:00
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
using UnityEngine.UI;

public class ConfirmWnd : UIWindow
{
    private Button _confirmbtn;
    private Text _content;
    private Button closeBtn;
    public override void OnOpen(params object[] datas)
    {
        base.OnOpen(datas);

        _confirmbtn = _transform.Find("Root/Confirm").gameObject.GetComponent<Button>();
        _content = _transform.Find("Root/Word").gameObject.GetComponent<Text>();
        closeBtn = _transform.Find("Root/closeBtn").gameObject.GetComponent<Button>();
        _confirmbtn.onClick.AddListener(OnClose);
        _content.text = datas[0].ToString();
        closeBtn.onClick.AddListener(OnClose);
    }

    public override void OnClose()
    {
        base.OnClose();
    }
    
}
