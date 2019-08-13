﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelRegister : UIPanel
{
    public InputField modelName;
    public Dropdown modelType;
    public Button openModelBtn;
    public InputField modelContent;
    public Button confirmAddBtn;
    public Button exportModelBtn;
    public Button backBtn;
    public Button openWordBtn;
    public Text tips;
    private ModelType type;
    //public string path;
    public string modelResouseName;
    public string wordPath;
    string tip;

    public override void OnBegin()
    {
        base.OnBegin();

        modelName = _transform.Find("InputName").GetComponent<InputField>();
        modelType = _transform.Find("modelType").GetComponent<Dropdown>();
        openModelBtn = _transform.Find("OpenModelBtn").GetComponent<Button>();
        modelContent = _transform.Find("InputContent").GetComponent<InputField>();
        confirmAddBtn = _transform.Find("AddmodelBtn").GetComponent<Button>();
        //exportModelBtn = _transform.Find("ExportBtn").GetComponent<Button>();
        backBtn = _transform.Find("BackBtn").GetComponent<Button>();
        openWordBtn = _transform.Find("OpenWordBtn").GetComponent<Button>();
        openWordBtn.onClick.AddListener(OpenWordFile);
        openModelBtn.onClick.AddListener(OpenModelFile);
        confirmAddBtn.onClick.AddListener(SaveModelMsg);
        backBtn.onClick.AddListener(Back);
    }
    public override void OnOpen(params object[] datas)
    {
        base.OnOpen();

        Debug.Log("open ModelRegister");

        modelName.text = "";
        modelContent.text = "";
        modelType.value = 0;
        //path = "";
        //name = "";
        modelResouseName = "";
        wordPath = "";
        type = ModelType.TypeOne;
}

    public override void OnClose()
    {
        base.OnClose();

        Debug.Log("close ModelRegister");
    }

    public void SaveModelMsg()
    {
        ModelDataManager.GetInstance.AddModel(modelName.text, modelContent.text, modelResouseName,wordPath, type, out tip);
        //tips.text = tip;
        Debug.Log(tip);
        OpenWindow<ConfirmWnd>(tip);
    }
    public void OpenModelFile()
    {
        FileOperation.OpenSingleFile(out string path, out string name, "FBX");
        if(!ModelDataManager.GetInstance.IsHasModel(modelName.text,out tip) &&path != "")
        {
            FileOperation.CopyFile(path, Constant.GetModelFullPath(name));
        }
        modelResouseName = name;
    }
    public void OpenWordFile()
    {
        FileOperation.OpenSingleFile(out string path, out string name, "txt","doc","docx","pdf");

        if (name != "")
        {
            FileOperation.CopyFile(path, Constant.GetWordPath(name));

            var ext = FileOperation.GetExt(path);
            wordPath = string.Format("{0}.{1}", name, ext);

        }


    }
    public void Back()
    {
        UIManager.getInstance.Back();
    }
}
