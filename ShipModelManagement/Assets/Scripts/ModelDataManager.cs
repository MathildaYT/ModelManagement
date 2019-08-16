using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ModelDaTaCache
{
    public int ID { get; set; }
    public string modelName { get; set; }
    public string modelContent { get; set; }
    public ModelType modelType { get; set; }
    public string modelPath { get; set; }
    public string wordpath { get; set; }
    public ModelDaTaCache()
    {
        modelName = "";
        modelContent = "";
        modelType = ModelType.NUll;
        modelPath = "";
        wordpath = "";
    }
}
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
    public void Init()
    {
        var check = DBInitController.GetInstance.DB.CheckTable<ModelData>();
        if (!check)
        {
            DBInitController.GetInstance.DB.CreateTable<ModelData>();
        }
    }

    public void AddModel(string model, string modelMsg, string modelpath,string wordPath,ModelType modelType, out string tips)
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
        data.ID = 1;
        data.modelName = model;
        data.modelContent = modelMsg;
        data.modelType = modelType;
        data.modelPath = modelpath;
        data.Wordpath = wordPath;
        DBInitController.GetInstance.DB.CreateData(data);
        tips = "录入成功";
    }
    public bool IsHasModel(string model,out string tips)
    {
        tips = "";
        var check = DBInitController.GetInstance.DB.CheckTable<ModelData>();
        if (!check)
        {
            DBInitController.GetInstance.DB.CreateTable<ModelData>();
        }
        var datas = DBInitController.GetInstance.DB.GetData<ModelData>();
        var d = datas.Where(x => x.modelName == model).FirstOrDefault();
        if (d != null)
        {
            tips = "该模型已经存在";
            return true;
        }
        return false;
    }
    public int ShowModel(string modelname, out string modelMsg, out string modelpath,out string WordPath, out ModelType modelType)
    {
        modelMsg = "";
        modelpath = "";
        WordPath = "";
        modelType = ModelType.NUll;
        var check = DBInitController.GetInstance.DB.CheckTable<ModelData>();
        if (!check)
        {
            return -1;
        }
        var datas = DBInitController.GetInstance.DB.GetData<ModelData>();
        var d = datas.Where(x => x.modelName == modelname).FirstOrDefault();
        if (d!=null)
        {
            modelMsg = d.modelContent;
            modelType = d.modelType;
            modelpath = d.modelPath;
            WordPath = d.Wordpath;
            return d.ID;
        }
        return -1;
    }
    public void ShowModel(string modelname, out string modelMsg, out string modelpath,  out ModelType modelType)
    {
        modelMsg = "";
        modelpath = "";
        modelType = ModelType.NUll;
        var check = DBInitController.GetInstance.DB.CheckTable<ModelData>();
        if (!check)
        {
            return;
        }
        var datas = DBInitController.GetInstance.DB.GetData<ModelData>();
        var d = datas.Where(x => x.modelName == modelname).FirstOrDefault();
        if (d != null)
        {
            modelMsg = d.modelContent;
            modelType = d.modelType;
            modelpath = d.modelPath;
        }

    }
    public ModelDaTaCache GetModelData(string modelname)
    {
        var data = new ModelDaTaCache();

        data.modelContent = "";
        data.modelPath = "";
        data.modelType = ModelType.NUll;
        data.wordpath = "";
        var check = DBInitController.GetInstance.DB.CheckTable<ModelData>();
        if (!check)
        {
            return null;
        }
        var datas = DBInitController.GetInstance.DB.GetData<ModelData>();
        var d = datas.Where(x => x.modelName == modelname).FirstOrDefault();
        if (d != null)
        {
            data.modelName = modelname;
            data.modelContent = d.modelContent;
            data.modelType = d.modelType;
            data.modelPath = d.modelPath;
            data.wordpath = d.Wordpath;
        }

        return data;
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
    public void DeleteAll()
    {
        var check = DBInitController.GetInstance.DB.CheckTable<ModelData>();
        if (!check)
        {
            return;
        }
        var datas = DBInitController.GetInstance.DB.GetData<ModelData>();
        foreach (var item in datas)
        {
        DBInitController.GetInstance.DB.DeleteData<ModelData>(item);
            DBInitController.GetInstance.DB.Connection.DeleteAll<ModelData>();
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
    public void AlterModel(string modelname,string modelMsg,ModelType modelType)
    {
        var check = DBInitController.GetInstance.DB.CheckTable<ModelData>();
        if (!check)
        {
            DBInitController.GetInstance.DB.CreateTable<ModelData>();
        }
        var datas = DBInitController.GetInstance.DB.GetData<ModelData>();
        var d = datas.Where(x => x.modelName == modelname).FirstOrDefault();
        if (d != null)
        {
            d.modelName = modelname;
            d.modelContent = modelMsg;
            d.modelType = modelType;
            DBInitController.GetInstance.DB.Update(d);
        }
        
    }
    public void AlterModel(ModelDaTaCache data)
    {
        var check = DBInitController.GetInstance.DB.CheckTable<ModelData>();
        if (!check)
        {
            DBInitController.GetInstance.DB.CreateTable<ModelData>();
        }
        var datas = DBInitController.GetInstance.DB.GetData<ModelData>();
        Debug.Log("id:" + data.ID);
        var d = datas.Where(x => x.ID == data.ID).FirstOrDefault();
        if (d != null)
        {
            d.modelName = data.modelName;
            if(data.modelContent != "")
                d.modelContent = data.modelContent;
            if (data.modelType != ModelType.NUll)
                d.modelType = data.modelType;
            if (data.modelPath != "")
                d.modelPath = data.modelPath;
            if (data.wordpath!="")
            {
                d.Wordpath = data.wordpath;
            }
            DBInitController.GetInstance.DB.Update(d);
        }
        //var m = new ModelDaTaCache();
        //m.modelName = "55";
        //AlterModel(m);
    }
    public void ShowModelContent(string name,out string content)
    {
        content = "";
        var check = DBInitController.GetInstance.DB.CheckTable<ModelData>();
        if (!check)
        {
            return;
        }
        var datas = DBInitController.GetInstance.DB.GetData<ModelData>();
        var d = datas.Where(x => x.modelName == name).FirstOrDefault();
        if (d != null)
        {
            content = d.modelContent;
        }
    }

    
}
