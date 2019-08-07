using SQLite4Unity3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestData
{
    [AutoIncrement]
    public int ID { get; set; }
    [PrimaryKey]
    public string Name { get; set; }

    public override string ToString()
    {
        return string.Format("[Data: Id={0}, Name={1}]", ID, Name);
    }
}
