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
    //UserType userType;
    Button QuitBtn;
    Text userText;
    public override void OnOpen()
    {
        base.OnOpen();
        userText = _transform.Find("UserShowTxt").GetComponent<Text>();
        lookBtn = _transform.Find("SelectPanel/LookBtn").GetComponent<Button>();
        EditBtn = _transform.Find("SelectPanel/EditBtn").GetComponent<Button>();
        AddBtn = _transform.Find("SelectPanel/AddBtn").GetComponent<Button>();
        alterPasswordBtn = _transform.Find("base/AlterPassword").GetComponent<Button>();
        QuitBtn = _transform.Find("base/Quit").GetComponent<Button>();
        QuitBtn.onClick.AddListener(Quit);
        if (UserManager.Instance.Type==UserType.Administrator)
        {
            userText.text = "登录人：管理员";
            alterPasswordBtn.onClick.AddListener(AlterPassWord);
            lookBtn.onClick.AddListener(delegate {

                OnSelectPanel(2);
                EditBtn.interactable = true;
                AddBtn.interactable = true;
                lookBtn.interactable = false;
            });
            EditBtn.onClick.AddListener(delegate {
                OnSelectPanel(1);

                EditBtn.interactable = false;
                AddBtn.interactable = true;
                lookBtn.interactable = true;
            });
            AddBtn.onClick.AddListener(delegate {
                OnSelectPanel(0);

                EditBtn.interactable = true;
                AddBtn.interactable = false;
                lookBtn.interactable = true;
            });

            EditBtn.interactable = false;
            UIManager.getInstance.Open<ModelEdit>();
        }
        if (UserManager.Instance.Type == UserType.Normal)
        {
            userText.text = "登录人：游客";

            lookBtn.onClick.AddListener(delegate {

                //OnSelectPanel(2);
                lookBtn.interactable = false;
            });

            EditBtn.gameObject.SetActive(false);
            AddBtn.gameObject.SetActive(false);
            alterPasswordBtn.gameObject.SetActive(false);
            UIManager.getInstance.Open<ModelLook>();
        }
       
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
        UIManager.getInstance.OpenWindow<AlterPsd>();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
