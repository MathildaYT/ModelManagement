#region 描述
//-----------------------------------------------------------------------------
// 类 名 称: ModelListView
// 作    者：zhangfan
// 创建时间：2019/8/1 10:37:38
// 描    述：
// 版    本：
//-----------------------------------------------------------------------------
// Copyright (C) 2017-2019 零境科技有限公司
//-----------------------------------------------------------------------------
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ModelListEdit : UIPanel
{
    InputField InputName;
    Dropdown DropModelType;
    Toggle HasModel;
    Toggle HasTxt;
    ScrollRect ScrollView;

    bool _hasModel;
    bool _hasTxt;
    ModelType _selectModelType;
    string _findname;

    public override void OnOpen(params object[] datas)
    {
        base.OnOpen();
        //-----------------------------------------
        ScrollView = _transform.Find("Scroll View").GetComponent<ScrollRect>();
        InputName = _transform.Find("InputName").GetComponent<InputField>();
        DropModelType = _transform.Find("ModelType").GetComponent<Dropdown>();
        HasModel = _transform.Find("HasModel").GetComponent<Toggle>();
        HasTxt = _transform.Find("HasTxt").GetComponent<Toggle>();
        //-----------------------------------------
        DropModelType.ClearOptions();
        var names = Enum.GetNames(typeof(ModelType));
        var namelist = new List<string>();
        foreach(var n in names)
        {
            namelist.Add(n);
        }
        DropModelType.AddOptions(namelist);
        //-----------------------------------------
        ShowModelList();
        //-----------------------------------------
        InputName.onValueChanged.AddListener(OnChangeName);

        DropModelType.onValueChanged.AddListener(OnChangeType);

        HasModel.onValueChanged.AddListener(OnEnableModelName);

        HasTxt.onValueChanged.AddListener(OnEnableText);
    }

    public override void OnClose()
    {
        base.OnClose();
    }

    override public void SendMessage(params object[] objects)
    {
        if(objects.Length == 0)
        {
            if(objects[0].GetType() == typeof(string))
            {
                var funcname = objects[0].ToString();
                if (funcname == "update")
                {
                    ShowModelList();
                }
            }
        }
    }

    private void ShowModelList()
    {
        var content = ScrollView.content;

        if (DBInitController.GetInstance.DB.CheckTable<ModelData>())
        {
            var datas = DBInitController.GetInstance.DB.GetData<ModelData>();
            var res = datas.Where((x => (_hasModel ^ x.modelName.Contains(_findname) && x.modelType == _selectModelType)));
            //datas1.Where();
            foreach (var r in res)
            {
                Debug.Log(r.ToString());
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
}
