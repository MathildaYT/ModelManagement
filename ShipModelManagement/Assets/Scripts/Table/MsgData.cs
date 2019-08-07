using SQLite4Unity3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgData 
{
    //[AutoIncrement]
    //public int ID { get; set; }
    [PrimaryKey]
    public string Name { get; set; }
    public string Password { get; set;}

    public override string ToString()
    {
        return string.Format("[Data: Name={0},Password={1}]", Name,Password);
    }
}
