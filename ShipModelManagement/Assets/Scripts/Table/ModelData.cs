using SQLite4Unity3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModelType
{
    TypeOne,
    TypeTwo,
    TypeThree
}
public class ModelData 
{
    [PrimaryKey]
    public string modelName { get; set; }
    public string modelContent { get; set; }
    public ModelType modelType { get; set; }
    public override string ToString()
    {
        return string.Format("[Data: modelName={0},modelContent={1}]", modelName,modelContent);
    }
}
