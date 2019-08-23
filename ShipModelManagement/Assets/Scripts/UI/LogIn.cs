using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{
    public InputField _user;
    public InputField _Psd;
    public Button _ManagerlogInBTn;
    //  public Button _RegisterBtn;
    public Button _FinishBtn;
    public Button _UserLogInBtn;
    public Button showPsdBtn;
    public InputField _NewUser;
    public InputField _NewPsd;
    public GameObject _logInPanel;
    public GameObject _RegisterPanel;
    public Text Tips;
    public Button resetBtn;
    int value = 0;
    public UserType userType;
    public Sprite showImg;
    public Sprite hideImg;

    bool isShow = false;
    // Start is called before the first frame update
    void Start()
    {
        //DBInitController.GetInstance.DB.CreateTable<MsgData>();
        _ManagerlogInBTn.onClick.AddListener(ManagerlogIn);
        _UserLogInBtn.onClick.AddListener(_userLogIn);
        // _RegisterBtn.onClick.AddListener(Register);
        _FinishBtn.onClick.AddListener(Finish);
        resetBtn.onClick.AddListener(ResetPassWord);

        showPsdBtn.onClick.AddListener(ShowPassword);
        UserManager.Instance.PrintAllUser();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ManagerlogIn()
    {
        userType = UserType.Administrator;
        string log;
        var ret = UserManager.Instance.LogIn(_user.text, _Psd.text, out log);
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
    public void _userLogIn()
    {
        userType = UserType.Normal;
        UserManager.Instance.LogIn(_user.text, _Psd.text, out string log, userType);
        SceneManager.LoadScene(Constant.ModelViewSceneName);
    }
    public void Register()
    {
        _logInPanel.SetActive(false);
        _RegisterPanel.SetActive(true);
    }
    public void Finish()
    {
        string log;
        UserManager.Instance.Register(_NewUser.text, _NewPsd.text, out log);
        Tips.text = log;
        _RegisterPanel.SetActive(false);
        _logInPanel.SetActive(true);
    }
    public void ResetPassWord()
    {
        Debug.Log("reset");
        if (value == 5)
        {
            UserManager.Instance.ResetPassword();
            UserManager.Instance.PrintAllUser();
        }
        else
        {
            value++;
        }
    }

    public void ShowPassword()
    {
       
        if (!isShow)
        {
            showPsdBtn.GetComponent<Image>().sprite = showImg;
            _Psd.contentType = InputField.ContentType.Standard;
            _Psd.ActivateInputField();
            isShow = true;
        }
        else
        {
            showPsdBtn.GetComponent<Image>().sprite = hideImg;
            _Psd.contentType = InputField.ContentType.Password;
            _Psd.ActivateInputField();
            isShow = false;

        }
    }
}
