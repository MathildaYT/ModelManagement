using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelEditBar : UIBar
{
    Dropdown _selectpanels;

    public override void OnOpen()
    {
        base.OnOpen();

        var panels = new List<string>();
        panels.Add("模型编辑");
        panels.Add("模型查看");
        panels.Add("模型录入");

        _selectpanels.AddOptions(panels);
    }

    public override void OnClose()
    {
        base.OnClose();
    }
}
