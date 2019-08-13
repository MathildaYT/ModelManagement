using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlterPsd : UIWindow
{
    public Button submitBtn;
    public InputField inputPsd;
    public Button closeBtn;

    public override void OnOpen(params object[] datas)
    {
        base.OnOpen(datas);
        submitBtn= _transform.Find("Submit").GetComponent<Button>();
        inputPsd = _transform.Find("AlterPanel/InputpassWord").GetComponent<InputField>();
        closeBtn = _transform.Find("CloseBtn").GetComponent<Button>();
    }
    public override void OnClose()
    {
        base.OnClose();
    }


}
