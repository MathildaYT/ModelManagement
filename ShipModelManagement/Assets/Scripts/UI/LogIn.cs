using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{
    public InputField _user;
    public InputField _Psd;
    public Button _logInBTn;
    public Button _RegisterBtn;
    public Button _FinishBtn;
    public InputField _NewUser;
    public InputField _NewPsd;
    public GameObject _logInPanel;
    public GameObject _RegisterPanel;
    public Text Tips;
    // Start is called before the first frame update
    void Start()
    {
        //DBInitController.GetInstance.DB.CreateTable<MsgData>();
        _logInBTn.onClick.AddListener(logIn);
        _RegisterBtn.onClick.AddListener(Register);
        _FinishBtn.onClick.AddListener(Finish);

        UserManager.Instance.PrintAllUser();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void logIn()
    {
        string log;
        var ret=UserManager.Instance.LogIn(_user.text,_Psd.text,out log);
        //if (CommonMethod.IsAllChinese(_user.text))
        //{
        //    if (ret)
        //    {
        //        SceneManager.LoadScene(Constant.ModelViewSceneName);

        //    }
        //} 
        Tips.text = log;
        //跳转
        if (ret)
        {
            SceneManager.LoadScene(Constant.ModelViewSceneName);
        }
    }
    public void Register()
    {
        _logInPanel.SetActive(false);
        _RegisterPanel.SetActive(true);
    }
    public void Finish()
    {
        string log;
        UserManager.Instance.Register(_NewUser.text,_NewPsd.text,out log);
        Tips.text = log;
        _RegisterPanel.SetActive(false);
        _logInPanel.SetActive(true);
    }
    
}
