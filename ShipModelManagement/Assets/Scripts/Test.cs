using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Annie");
        ModelDataManager.GetInstance.PrintAllModel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            ModelDataManager.GetInstance.DeleteAll();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            UserManager.Instance.DeleteAllUser();
        }
    }

}
