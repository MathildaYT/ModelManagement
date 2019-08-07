using System.Collections;
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

    public Text tips;
    private ModelType type;
    public string path;
    public string name;

    public override void OnOpen()
    {
        base.OnOpen();

        Debug.Log("open ModelRegister");

        modelName = _transform.Find("InputName").GetComponent<InputField>();
        modelType = _transform.Find("modelType").GetComponent<Dropdown>();
        openModelBtn = _transform.Find("OpenModelBtn").GetComponent<Button>();
        modelContent = _transform.Find("InputContent").GetComponent<InputField>();
        confirmAddBtn = _transform.Find("AddmodelBtn").GetComponent<Button>();
        //exportModelBtn = _transform.Find("ExportBtn").GetComponent<Button>();
        backBtn = _transform.Find("BackBtn").GetComponent<Button>();

        openModelBtn.onClick.AddListener(OpenModelFile);
        confirmAddBtn.onClick.AddListener(SaveModelMsg);
        backBtn.onClick.AddListener(Back);
    }

    public override void OnClose()
    {
        base.OnClose();

        Debug.Log("close ModelRegister");
    }

    public void SaveModelMsg()
    {
        string tip;
        ModelDataManager.GetInstance.AddModel(modelName.text, modelContent.text, type, out tip);
        tips.text = tip;
    }
    public void OpenModelFile()
    {
        FileOperation.OpenSingleFile(out path, out name, "FBX");

        FileOperation.CopyFile(path, Constant.GetModelFullPath(name));
    }
    public void Back()
    {
        UIManager.getInstance.Back();
    }
}
