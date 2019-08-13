using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterController : MonoBehaviour
{
    IEnumerator Start()
    {
        while (DBInitController.GetInstance.DB == null)
        {
            yield return 0;
        }
        UserManager.Instance.Init();
        ModelDataManager.GetInstance.Init();
        SceneManager.LoadScene(Constant.LoginSceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
