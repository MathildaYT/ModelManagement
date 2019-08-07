using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModelDataManager 
{
    private static ModelDataManager _instance;
    public static ModelDataManager GetInstance
    {
        get
        {
            if (_instance==null)
            {
                _instance = new ModelDataManager();
            }
            return _instance;
        }
    }

    public void AddModel(string model, string modelMsg,ModelType modelType, out string tips)
    {
        var check = DBInitController.GetInstance.DB.CheckTable<ModelData>();
        if (!check)
        {
            DBInitController.GetInstance.DB.CreateTable<ModelData>();
        }
        var datas = DBInitController.GetInstance.DB.GetData<ModelData>();
        var d = datas.Where(x => x.modelName == model).FirstOrDefault();
        if (d!=null)
        {
            tips = "该模型已经存在";
            return;
        }
        var data = new ModelData();
        data.modelName = model;
        data.modelContent = modelMsg;
        data.modelType = modelType;
        DBInitController.GetInstance.DB.CreateData(data);
        tips = "";
    }
    public void DeleteModel(string model)
    {
        var check = DBInitController.GetInstance.DB.CheckTable<ModelData>();
        if (!check)
        {
            return;
        }
        var datas = DBInitController.GetInstance.DB.GetData<ModelData>();
        var d = datas.Where(x => x.modelName == model).FirstOrDefault();
        if (d != null)
        {
            DBInitController.GetInstance.DB.DeleteData<ModelData>(d);
            return;
        }

    }
    public void PrintAllModel()
    {
        var datas = DBInitController.GetInstance.DB.GetData<ModelData>();
        foreach (var d in datas)
        {
            Debug.Log(d.ToString());
        }
    }

}
