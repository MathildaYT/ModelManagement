using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ModelEdit : UIPanel
{
    InputField InputName;
    Dropdown DropModelType;
    Toggle HasModel;
    Toggle HasTxt;
    ScrollRect ScrollView;

    GameObject _ModelDataPrefab;

    bool _hasModel = true;
    bool _hasTxt = true;
    ModelType _selectModelType = ModelType.NUll;
    string _findname = "";

    public override void OnBegin()
    {
        base.OnBegin();

        //-----------------------------------------
        ScrollView = _transform.Find("Root/Scroll View").GetComponent<ScrollRect>();
        InputName = _transform.Find("Root/InputName").GetComponent<InputField>();
        DropModelType = _transform.Find("Root/ModelType").GetComponent<Dropdown>();
        HasModel = _transform.Find("Root/HasModel").GetComponent<Toggle>();
        HasTxt = _transform.Find("Root/HasTxt").GetComponent<Toggle>();

        _ModelDataPrefab = ScrollView.content.GetChild(0).gameObject;
        _ModelDataPrefab.SetActive(false);
        _ModelDataPrefab.transform.parent = null;
        //-----------------------------------------
        DropModelType.ClearOptions();
        var names = Enum.GetNames(typeof(ModelType));
        var namelist = new List<string>();
        foreach (var n in names)
        {
            namelist.Add(n);
        }
        DropModelType.AddOptions(namelist);

    }

    public override void OnOpen(params object[] datas)
    {
        base.OnOpen();

        //-----------------------------------------
        //init
        ClearUI();
        //-----------------------------------------
        ShowModelList();
        //-----------------------------------------
        InputName.onValueChanged.AddListener(OnChangeName);

        DropModelType.onValueChanged.AddListener(OnChangeType);

        HasModel.onValueChanged.AddListener(OnEnableModelName);

        HasTxt.onValueChanged.AddListener(OnEnableText);
    }

    private void ClearUI()
    {
        InputName.text = "";
        DropModelType.value = 0;
        HasModel.isOn = true;
        HasTxt.isOn = true;

        _hasModel = true;
        _hasTxt = true;
        _selectModelType = ModelType.NUll;
        _findname = "";
    }

    public override void OnClose()
    {
        base.OnClose();
    }

    private void ShowModelList()
    {
        var content = ScrollView.content;

        if (DBInitController.GetInstance.DB.CheckTable<ModelData>())
        {
            var datas = DBInitController.GetInstance.DB.GetData<ModelData>();
            if (_findname != null)
            {
                //var res = datas.Where((x => (_hasModel ^ x.modelName.Contains(_findname) && x.modelType == _selectModelType)));
                //IEnumerable<ModelData> res;
                if (_hasModel)
                {
                    datas = datas.Where(x => x.modelPath != "");
                }
                if (_findname != "")
                {
                    datas = datas.Where(x => x.modelName.Contains(_findname));
                }

                if (_selectModelType != ModelType.NUll)
                {
                    datas = datas.Where(x => x.modelType == _selectModelType);
                }

                if (_hasTxt)
                {
                    datas = datas.Where(x => x.Wordpath != "");
                }
                //datas1.Where();
                //delete
                List<GameObject> templist = new List<GameObject>();
                while (ScrollView.content.childCount > 0)
                {
                    var child = ScrollView.content.GetChild(0);

                    //GameObject.Destroy(child);
                    child.gameObject.SetActive(false);

                    child.parent = null;
                    templist.Add(child.gameObject);
                }

                for (int i = 0; i < templist.Count; ++i)
                {
                    GameObject.Destroy(templist[i]);
                }
                templist.Clear();

                int numdata = 0;
                float everyheight = _ModelDataPrefab.GetComponent<RectTransform>().sizeDelta.y;
                var verticalgroup = ScrollView.content.gameObject.GetComponent<VerticalLayoutGroup>();
                //add
                foreach (var r in datas)
                {
                    Debug.Log(r.ToString());
                    //ScrollView.content
                    var obj = GameObject.Instantiate(_ModelDataPrefab);
                    obj.transform.parent = ScrollView.content;
                    obj.SetActive(true);

                    var datactrl = obj.GetComponent<ModelDataController>();
                    datactrl.Name.text = r.modelName;
                    datactrl.ModelName = r.modelName;
                    if (r.Wordpath == "")
                    {
                        datactrl.Word.interactable = false;
                        datactrl.Word.GetComponentInChildren<Text>().text = "无";
                    }
                    datactrl.Type.text = Enum.GetName(typeof(ModelType), r.modelType);
                    //datactrl.WordPath = r.

                    numdata++;
                }

                var contenttrans = ScrollView.content.transform.gameObject.GetComponent<RectTransform>();
                contenttrans.sizeDelta = new Vector2(contenttrans.sizeDelta.x, numdata * (everyheight+ verticalgroup.spacing) + 5);
            }
        }
    }

    private void OnChangeName(string newname)
    {
        _findname = newname;
        ShowModelList();
    }

    private void OnChangeType(int type)
    {
        _selectModelType = (ModelType)type;
        ShowModelList();
    }

    private void OnEnableModelName(bool enable)
    {
        _hasModel = enable;
        ShowModelList();
    }

    private void OnEnableText(bool enable)
    {
        _hasTxt = enable;
        ShowModelList();
    }
    override public void SendMessage(params object[] objects)
    {
        if (objects.Length != 0)
        {
            if (objects[0].GetType() == typeof(string))
            {
                var funcname = objects[0].ToString();
                if (funcname == "update")
                {
                    ShowModelList();
                }
            }
        }
    }
}
