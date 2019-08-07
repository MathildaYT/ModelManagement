using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DBInitController.GetInstance.DB.CreateTable<TestData>();

        var data = new TestData();
        data.Name = "abc";
        data.ID = 1;
        DBInitController.GetInstance.DB.CreateData<TestData>(data);

        data = new TestData();
        data.Name = "yt";
        data.ID = 2;
        data = DBInitController.GetInstance.DB.CreateData<TestData>(data);

        data = new TestData();
        data.Name = "annie";
        data.ID = 3;
        data = DBInitController.GetInstance.DB.CreateData<TestData>(data);

        data = new TestData();
        data.Name = "annie2";
        data.ID = 11;
        data = DBInitController.GetInstance.DB.CreateData<TestData>(data);


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            var datas = DBInitController.GetInstance.DB.GetData<TestData>();
            foreach(var d in datas)
            {
                Debug.Log(d.ToString());
            }
            
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            var datas = DBInitController.GetInstance.DB.GetData<TestData>();
            var d = datas.Where(x => x.Name == "annie");
            foreach (var findd in d)
            {
                Debug.Log(findd.ToString());
            }
            
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            var datas = DBInitController.GetInstance.DB.GetData<TestData>();
            var d = datas.Where(x => x.Name == "annie1");

           //var d2= DBInitController.GetInstance.DB.IsExistData<TestData>("annie1");
          // Debug.Log(d2.ToString());
        }



    }
}
