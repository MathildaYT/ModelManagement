using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelEditBar : UIBar
{
    Button lookBtn;
    Button EditBtn;
    Button AddBtn;
    Button alterPasswordBtn;
    public override void OnOpen()
    {
        base.OnOpen();
        lookBtn = _transform.Find("SelectPanel/LookBtn").GetComponent<Button>();
        EditBtn = _transform.Find("SelectPanel/EditBtn").GetComponent<Button>();
        AddBtn = _transform.Find("SelectPanel/AddBtn").GetComponent<Button>();
        alterPasswordBtn = _transform.Find("AlterPassword").GetComponent<Button>();
        alterPasswordBtn.onClick.AddListener(AlterPassWord);
        lookBtn.onClick.AddListener(delegate { OnSelectPanel(2); });
        EditBtn.onClick.AddListener(delegate { OnSelectPanel(1); });
        AddBtn.onClick.AddListener(delegate { OnSelectPanel(0); });

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
                    UIManager.getInstance.Open<ModelRegister>();
                }
                break;
            case 1:
                {
                    UIManager.getInstance.Open<ModelEdit>();
                }
                break;
            case 2:
                {
                    UIManager.getInstance.Open<ModelLook>();
                }
                break;
        }
    }
    public void AlterPassWord()
    {
       // UserManager.Instance.AlterPassword();
    }
}
