using SQLite4Unity3d;
using UnityEngine;
using System;
using System.IO;
using static SQLite4Unity3d.SQLiteConnection;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService  {

	private SQLiteConnection _connection;
    public SQLiteConnection Connection
    {
        get
        {
            return _connection;
        }
    }

    public DataService(string DatabaseName){

#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
        var dbPathPersistentDataPath = string.Format(Application.persistentDataPath + "/{0}", DatabaseName);

        //if (!File.Exists(dbPathPersistentDataPath))
        //{
        //    File.Copy(dbPath, dbPathPersistentDataPath);
        //}
        Debug.Log("Final PATH: " + dbPath);
        try
        {
            //构造数据库连接 打开数据库
            _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void CloseConnection()
    {
        //销毁Connection
        if (_connection != null)
        {
            _connection.Close();
        }
        _connection = null;
    }

    public bool CheckTable<T>()
    {
        var data = _connection.GetTableInfo(typeof(T).ToString());

        if(data != null && data.Count > 0)
        {
            return true;
        }
        return false;
    }

    public void CreateTable<T>()
    {
        _connection.DropTable<T>();
        _connection.CreateTable<T>();
    }

    public T CreateData<T>(T data) where T : new()
    {
        _connection.Insert(data);

        return data;
    }
    public T DeleteData<T>(T data)where T:new()
    {
        _connection.Delete(data);
        return data;
    }
    public IEnumerable<T> GetData<T>() where T : new()
    {
        return _connection.Table<T>();
    }

    public void Update(object obj)
    {
        _connection.Update(obj);
    }

    //public void test()
    //{
    //    var d = _connection.Table<per>();

    //    d.Where(x => x.Name == "Roberto");
    //}
}

//class per
//{
//    [PrimaryKey, AutoIncrement]
//    public string Name { get; set; }
//    public per()
//    {
//        ;
//    }
//}
