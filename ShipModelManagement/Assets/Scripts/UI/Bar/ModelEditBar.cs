﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        userText = _transform.Find("Root/UserShowTxt").GetComponent<Text>();
        lookBtn = _transform.Find("Root/SelectPanel/LookBtn").GetComponent<Button>();
        EditBtn = _transform.Find("Root/SelectPanel/EditBtn").GetComponent<Button>();
        AddBtn = _transform.Find("Root/SelectPanel/AddBtn").GetComponent<Button>();
        alterPasswordBtn = _transform.Find("Root/base/AlterPassword").GetComponent<Button>();
        QuitBtn = _transform.Find("Root/base/Quit").GetComponent<Button>();
        QuitBtn.onClick.AddListener(Quit);
        if (UserManager.Instance.Type==UserType.Administrator)
        {
            userText.text = "管理员";
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

            lookBtn.interactable = false;
            UIManager.getInstance.Open<ModelLook>();
        }
        if (UserManager.Instance.Type == UserType.Normal)
        {
            userText.text = "普通用户";

            lookBtn.onClick.AddListener(delegate {

                //OnSelectPanel(2);
                lookBtn.interactable = false;
            });
            lookBtn.interactable = false;
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
        // Application.Quit();
        SceneManager.LoadScene(Constant.LoginSceneName);

    }
}
