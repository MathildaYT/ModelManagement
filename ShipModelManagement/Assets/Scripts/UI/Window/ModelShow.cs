using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelShow : UIWindow
{
   
    public UniFBXImport uniFBXImport;
    public Text modelContent;
    public Button closeBtn;
    GameObject model;
    Transform showpos;
    Text tips;
    //string content;
    public override void OnOpen(params object[] datas)
    {
        base.OnOpen();
        string modelName = datas[0].ToString();
        uniFBXImport= _transform.GetComponent<UniFBXImport>();
        modelContent = _transform.Find("Canvas/ShowPanel/Content").GetComponent<Text>();
        string content = "";
        tips = _transform.Find("Canvas/tips").GetComponent<Text>();
        closeBtn = _transform.Find("Canvas/ShowPanel/CloseBtn").GetComponent<Button>();
        showpos = _transform.Find("SpawanPos");
        //ModelDataManager.GetInstance.ShowModelContent(modelName,out content);
        ModelType mtype;
        string modelResouseName;
        ModelDataManager.GetInstance.ShowModel(modelName, out content, out modelResouseName, out mtype);
        if (content=="")
        {
            modelContent.text = "暂未录入模型简介";
        }
        else
        {
        modelContent.text = content;
        }
        if (modelResouseName=="")
        {
            tips.gameObject.SetActive(true);
            tips.text = "暂未录入模型";
        }
        else
        {
            tips.gameObject.SetActive(false);
        }
        closeBtn.onClick.AddListener(OnClose);
        LoadModel(modelResouseName);

    }
    public override void OnClose()
    {
        base.OnClose();
        DeleteModel();
    }
    public void LoadModel(string name)
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
        if(model != null)
        {
            SetGameObjectLayer(model, 9);

            model.transform.position = showpos.position;

            model.transform.rotation = showpos.rotation;
            model.AddComponent<RotateModel>();
        }
    }
    private void SetGameObjectLayer(GameObject trans, int layer)
    {
        trans.layer = layer;
        for (int i=0;i< trans.transform.childCount;++i)
        {
            var child = trans.transform.GetChild(i);
            SetGameObjectLayer(child.gameObject, layer);
        }
    }
    void DeleteModel()
    {
        if (model!=null)
        {
            GameObject.Destroy(model);

            var panel = UIManager.getInstance.CurrentPanel();
            if(panel != null)
            {
                panel.SendMessage("update");
            }
        }
        
    }
}
