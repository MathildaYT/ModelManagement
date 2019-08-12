using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBInitController : MonoBehaviour
{
    public string DBName = "Lingjing.db";
    public DataService DB { get; private set; }

    static DBInitController _instance;
    public static DBInitController GetInstance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _instance = this;

        StartSync();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartSync()
    {
        DB = new DataService(DBName);
    }

}
