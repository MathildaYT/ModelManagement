﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelShow : UIWindow
{
   
    public UniFBXImport uniFBXImport;
    public Text modelContent;
    public Button closeBtn;
    GameObject model;
    //string content;
    public override void OnOpen(params object[] datas)
    {
        base.OnOpen();
        string modelName = datas[0].ToString();
        uniFBXImport= _transform.GetComponent<UniFBXImport>();
        modelContent = _transform.Find("Canvas/ShowPanel/Content").GetComponent<Text>();
        string content = "";
        closeBtn = _transform.Find("Canvas/ShowPanel/CloseBtn").GetComponent<Button>();
        ModelDataManager.GetInstance.ShowModelContent(modelName,out content);
        modelContent.text = content;
        closeBtn.onClick.AddListener(OnClose);
        LoadModel(modelName);
    }
    public override void OnClose()
    {
        base.OnClose();
        DeleteModel();
    }
    public void LoadModel(string name)
    {
        uniFBXImport.setting.paths.urlModels = Constant.GetModelPath(name);
        uniFBXImport.setting.paths.urlTextures = Constant.GetModelTexPath();
        uniFBXImport.setting.paths.filename = name;
        uniFBXImport.Load();
        MonoHelper.StartCoroutine(WaitForLoad());
    }
    IEnumerator WaitForLoad()
    {
        while (!uniFBXImport.IsDone)
        {
            yield return null;
        }
        model= uniFBXImport.GetObject();
    }
    void DeleteModel()
    {
        if (model!=null)
        {
            GameObject.Destroy(model);
        }
    }
}
