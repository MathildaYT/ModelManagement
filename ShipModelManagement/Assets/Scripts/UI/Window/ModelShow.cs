using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    ScrollRect scrollView;
    GameObject _modelBtn;
    string modelName="";
    bool hasModel = true;
    Button _topView;
    Button _nightvison;
    bool IsTop = false;
    bool IsNight = false;
    public override void OnOpen(params object[] datas)
    {
        base.OnOpen();
        modelName = datas[0].ToString();
        uniFBXImport= _transform.GetComponent<UniFBXImport>();
        modelContent = _transform.Find("Canvas/ShowPanel/Content").GetComponent<Text>();
        string content = "";
        tips = _transform.Find("Canvas/tips").GetComponent<Text>();
        closeBtn = _transform.Find("Canvas/ShowPanel/CloseBtn").GetComponent<Button>();
        _topView = _transform.Find("Canvas/ShowPanel/TopViewBtn").GetComponent<Button>();
        _nightvison= _transform.Find("Canvas/ShowPanel/nightVisionBtn").GetComponent<Button>();
        showpos = _transform.Find("SpawanPos");
        scrollView = _transform.Find("Canvas/ShowPanel/Scroll View").GetComponent<ScrollRect>();
        //ModelDataManager.GetInstanceShowModelContent(modelName,out content);
        ModelType mtype;
        string modelResouseName;
        _modelBtn = scrollView.content.GetChild(0).gameObject;
        _modelBtn.SetActive(false);
        _modelBtn.transform.parent = null;
        _transform.GetComponent<night>().enabled = false;
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
        _topView.onClick.AddListener(TopView);
        _nightvison.onClick.AddListener(NightVision);
        LoadModel(modelResouseName);
        ShowModelList();
    }
    private void ClearUI()
    {
        modelName = "";
        hasModel = false;
        DeleteModel();
    }
    public override void OnClose()
    {
        base.OnClose();
        DeleteModel();
      //  GameObject.Destroy(_transform.gameObject);
    }
    public void LoadModel(string name)
    {
        uniFBXImport.setting.paths.urlModels = Constant.GetModelPath();
        uniFBXImport.setting.paths.urlTextures = Constant.GetModelTexPath();
        uniFBXImport.setting.paths.filename = name;
        uniFBXImport.Load();
        MonoHelper.StartCoroutine(WaitForLoad());
        ShowModelList();
    }
    private void ShowModelList()
    {
        var content = scrollView.content;
        if (DBInitController.GetInstance.DB.CheckTable<ModelData>())
        {
            var datas = DBInitController.GetInstance.DB.GetData<ModelData>();
            if (modelName!=null)
            {
                datas = datas.Where(x => x.modelPath != "");
                List<GameObject> templist = new List<GameObject>();
                while (scrollView.content.childCount > 0)
                {
                    var child = scrollView.content.GetChild(0);
                    child.gameObject.SetActive(false);
                    child.parent = null;
                    templist.Add(child.gameObject);
                }
                for (int i = 0; i < templist.Count; ++i)
                {
                    GameObject.Destroy(templist[i]);
                }
                templist.Clear();
                int num = 0;
                float btnHeight = _modelBtn.GetComponent<RectTransform>().sizeDelta.y;
                var verticalGroup = scrollView.content.gameObject.GetComponent<VerticalLayoutGroup>();
                foreach (var item in datas)
                {
                    var obj = GameObject.Instantiate(_modelBtn);
                    obj.transform.parent = scrollView.content;
                    obj.SetActive(true);
                    //  obj.GetComponent<Text>().text = item.modelName;
                    var datactrl = obj.GetComponent<ModelNameList>();
                    datactrl.Name.text = item.modelName;
                    datactrl.ModelName = item.modelName;
                    datactrl.OnShowModel = ViewModel;
                    //obj.gameObject.GetComponent<Button>().onClick.AddListener(ViewModel);
                    num++;
                }

                var contenttrans = scrollView.content.transform.gameObject.GetComponent<RectTransform>();
                contenttrans.sizeDelta = new Vector2(contenttrans.sizeDelta.x, num * (btnHeight + verticalGroup.spacing) + 5);
            }
           
        }
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
            model.AddComponent<OperateModel>();
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
    void ViewModel(string modelstr)
    {
        if (modelName==modelstr)
        {
            return;
        }
        if (modelName != "")
        {
            DeleteModel();
        }
        if (modelstr != "")
        {
            modelName = modelstr;
        }
        ViewModel();
    }
    void ViewModel()
    {
        string content = "";
        string modelResouseName;
        ModelType mtype;
        ModelDataManager.GetInstance.ShowModel(modelName, out content, out modelResouseName,out mtype);
        if (content == "")
        {
            modelContent.text = "暂未录入模型简介";
        }
        else
        {
            modelContent.text = content;
        }
        if (modelResouseName == "")
        {
            tips.gameObject.SetActive(true);
            tips.text = "暂未录入模型";
        }
        else
        {
            tips.gameObject.SetActive(false);
        }
        LoadModel(modelResouseName);
    }
    public void TopView()
    {
        if (model!=null)
        {
            if (!IsTop)
            {
                model.GetComponent<OperateModel>().TopView();
                IsTop = true;
                _topView.GetComponentInChildren<Text>().text = "俯视图";
            }
            else
            {
                model.GetComponent<OperateModel>().FrontView();
                _topView.GetComponentInChildren<Text>().text = "前视图";
                IsTop = false;
            }
        }
    }
    public void NightVision()
    {
        if (!IsNight)
        {
        _transform.GetComponent<night>().enabled = true;
            IsNight = true;
        }
        else
        {
            _transform.GetComponent<night>().enabled = false;
            IsNight = false;
        }
    }
}
