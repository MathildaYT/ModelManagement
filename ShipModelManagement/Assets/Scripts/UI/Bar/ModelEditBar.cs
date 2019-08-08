using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelEditBar : UIBar
{
    Dropdown _selectpanels;

    List<string> _panels = new List<string>();

    public override void OnOpen()
    {
        base.OnOpen();

        _selectpanels = _transform.Find("SelectPanel").gameObject.GetComponent<Dropdown>();

        _panels.Clear();
        _panels.Add("模型编辑");
        _panels.Add("模型查看");
        _panels.Add("模型录入");

        _selectpanels.ClearOptions();
        _selectpanels.AddOptions(_panels);

        _selectpanels.onValueChanged.AddListener(OnSelectPanel);
    }

    public override void OnClose()
    {
        base.OnClose();
    }

    private void OnSelectPanel(int index)
    {
        switch(index)
        {
            case 0:
                {
                    UIManager.getInstance.Open<ModelAlter>();
                }
                break;
            case 1:
                {
                    UIManager.getInstance.Open<ModelList>();
                }
                break;
            case 2:
                {
                    UIManager.getInstance.Open<ModelRegister>();
                }
                break;
        }
    }
}
