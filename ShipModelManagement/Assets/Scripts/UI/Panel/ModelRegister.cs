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
    public Button openWordBtn;
    public Button backBtn;

    public Text tips;
    private ModelType type;
    public string path;
    public string name;

    private string wordpath;
    private string wordname;

    public override void OnBegin()
    {
        base.OnBegin();

        modelName = _transform.Find("InputName").GetComponent<InputField>();
        modelType = _transform.Find("modelType").GetComponent<Dropdown>();
        openModelBtn = _transform.Find("OpenModelBtn").GetComponent<Button>();
        modelContent = _transform.Find("InputContent").GetComponent<InputField>();
        confirmAddBtn = _transform.Find("AddmodelBtn").GetComponent<Button>();
        openWordBtn = _transform.Find("OpenWordBtn").GetComponent<Button>();
        backBtn = _transform.Find("BackBtn").GetComponent<Button>();

        openModelBtn.onClick.AddListener(OpenModelFile);
        openWordBtn.onClick.AddListener(OpenWordFile);
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
        path = "";
        name = "";
        type = ModelType.TypeOne;
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

        if (path != "")
        {
            FileOperation.CopyFile(path, Constant.GetModelFullPath(name));
        }

        if (wordpath != "")
        {
            FileOperation.CopyFile(wordpath, Constant.GetModelFullPath(wordname));
        }
        //tips.text = tip;
        Debug.Log(tip);
        OpenWindow<ConfirmWnd>(tip);
    }
    public void OpenModelFile()
    {
        FileOperation.OpenSingleFile(out path, out name, "FBX");
    }
    public void OpenWordFile()
    {
        FileOperation.OpenSingleFile(out wordpath, out wordname, "txt", "pdf", "doc", "docx");

        Application.OpenURL(wordpath);
    }
    public void Back()
    {
        UIManager.getInstance.Back();
    }
}
