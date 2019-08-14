﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelAlter : UIPanel
{
    public InputField model;
    public Dropdown modelType;
    public Button openModelBtn;
    public InputField modelContent;
    public Button confirmAlterBtn;
    public Button exportModelBtn;
    public Button backBtn;
    public InputField modelPath;
    public InputField wordPath;
    public Text tips;
    private ModelType type;
    //public string path;
    public string modelResouseName;
    private string wordName;
    private ModelDaTaCache data;

    public override void OnBegin()
    {
        base.OnBegin();
        model= _transform.Find("InputName").GetComponent<InputField>();
        modelType = _transform.Find("modelType").GetComponent<Dropdown>();
        openModelBtn = _transform.Find("OpenModelBtn").GetComponent<Button>();
        modelContent = _transform.Find("InputContent").GetComponent<InputField>();
        confirmAlterBtn = _transform.Find("AltermodelBtn").GetComponent<Button>();
        modelPath = _transform.Find("InputPath").GetComponent<InputField>();
        wordPath = _transform.Find("InputWordPath").GetComponent<InputField>();
        //backBtn = _transform.Find("BackBtn").GetComponent<Button>();
        confirmAlterBtn.onClick.AddListener(ConfirmAlter);
        openModelBtn.onClick.AddListener(OpenModelFile);
        //  confirmAddBtn.onClick.AddListener(SaveModelMsg);
        modelType.onValueChanged.AddListener(ChangeType);
        model.onValueChanged.AddListener(AlterName);
        modelContent.onValueChanged.AddListener(AlterContent);
        data = new ModelDaTaCache();
    }

    public override void OnOpen(params object[] datas)
    {
        base.OnOpen();

        Debug.Log("open ModelAlter");

        string modelName=datas[0].ToString();
        //  backBtn.onClick.AddListener(Back);
        //  ShowModelMsg();
        wordName ="";
        ShowModelMsg(modelName);

    }
    public override void OnClose()
    {
        base.OnClose();

        Debug.Log("close ModelAlter");
    }
    public void SaveModelMsg()
    {
        string tip;
        ModelDataManager.GetInstance.AddModel(model.text, modelContent.text, modelResouseName, wordName, type, out tip);
        tips.text = tip;
    }
    public void OpenModelFile()
    {
        FileOperation.OpenSingleFile(out string path, out string name, "FBX");

        FileOperation.CopyFile(path, Constant.GetModelFullPath(name));

        modelResouseName = name;
    }
    public void Back()
    {
        UIManager.getInstance.Back();
    }
    public void ShowModelMsg(string modelName)
    {
        string content;
        ModelType mtype;
        ModelDataManager.GetInstance.ShowModel(modelName, out content, out string modelpath, out wordName, out mtype);
        modelContent.text=content;
        type = mtype;
        modelType.value = (int)mtype;
        //modelPath.text = Constant.GetModelPath(modelName);
        modelPath.text = modelpath;
        model.text = modelName;
        wordPath.text = wordName;
    }
    public void ConfirmAlter()
    {
      
       ModelDataManager.GetInstance.AlterModel(data); 
    }
    public void ChangeType(int Value)
    {
        switch (Value)
        {
            case 0:
                type = ModelType.NUll;
                break;
            case 1:
                type = ModelType.TypeOne;
                break;
            case 2:
                type = ModelType.TypeTwo;
                break;
            case 3:
                type = ModelType.TypeThree;
                break;
            default:
                break;
        }
        data.modelType = type;
    }
    public void AlterName(string name)
    {
        name = model.text;
        data.modelName = name;
    }
    public void AlterContent(string content)
    {
        content = modelContent.text;
        data.modelContent = content;
    }
    public void AlterPath(string path)
    {
        path = modelPath.text;
        data.modelPath = path;
    }
    public void OpenWordFile()
    {
        FileOperation.OpenSingleFile(out string path, out string name, "txt", "doc", "docx", "pdf");
        if (name!="")
        {
            FileOperation.CopyFile(path, Constant.GetWordPath(name));

            var ext = FileOperation.GetExt(path);
            wordName = string.Format("{0}.{1}", name, ext);

        }
    }

}
