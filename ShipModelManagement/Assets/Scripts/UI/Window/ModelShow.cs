using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelShow : UIWindow
{
    string name;
    public UniFBXImport uniFBXImport;
    public Text modelContent;
    public Button closeBtn;
    GameObject model;
    public override void OnOpen(params object[] datas)
    {
        base.OnOpen();
        uniFBXImport.GetComponent<UniFBXImport>();
        modelContent = _transform.Find("Canvas/ShowPanel/Content").GetComponent<Text>();
        closeBtn = _transform.Find("Canvas/ShowPanel/CloseBtn").GetComponent<Button>();
        LoadModel();
        ModelDataManager.GetInstance.ShowModel(name, modelContent);
        closeBtn.onClick.AddListener(OnClose);
    }
    public override void OnClose()
    {
        base.OnClose();
        DeleteModel();
    }
    public void LoadModel()
    {
        uniFBXImport.setting.paths.urlModels = Constant.GetModelPath();
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
