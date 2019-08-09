﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ModelDaTaCache
{
    public string modelName { get; set; }
    public string modelContent { get; set; }
    public ModelType modelType { get; set; }
    public string modelPath { get; set; }
    public ModelDaTaCache()
    {
        modelName = "";
        modelContent = "";
        modelType = ModelType.NUll;
        modelPath = "";
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
        tips = "录入成功";
    }
    public void ShowModel(string modelname, out string modelMsg, out ModelType modelType)
    {
        modelMsg = "";
        modelType = ModelType.NUll;
        var check = DBInitController.GetInstance.DB.CheckTable<ModelData>();
        if (!check)
        {
            return;
        }
        var datas = DBInitController.GetInstance.DB.GetData<ModelData>();
        var d = datas.Where(x => x.modelName == modelname).FirstOrDefault();
        if (d!=null)
        {
            modelMsg = d.modelContent;
            modelType = d.modelType;
        }
       
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
        var d = datas.Where(x => x.modelName == data.modelName).FirstOrDefault();
        if (d != null)
        {
            d.modelName = data.modelName;
            if(data.modelContent != "")
                d.modelContent = data.modelContent;
            if (data.modelType != ModelType.NUll)
                d.modelType = data.modelType;
            if (data.modelPath != "")
                d.modelPath = data.modelPath;
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

    internal void ShowModel(string name, Text modelContent)
    {
        throw new NotImplementedException();
    }
}
