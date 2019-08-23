using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlterPsd : UIWindow
{
    public Button submitBtn;
    public InputField inputPsd;
    public Button closeBtn;
    public Text tips;
    bool isValid = false;
    public Transform showImg;
    Button showPsdBtn;
    bool isShow = false;
    public override void OnOpen(params object[] datas)
    {
        base.OnOpen(datas);
        submitBtn = _transform.Find("Root/Submit").GetComponent<Button>();
        inputPsd = _transform.Find("Root/AlterPanel/InputpassWord").GetComponent<InputField>();
        closeBtn = _transform.Find("Root/CloseBtn").GetComponent<Button>();
       showPsdBtn = _transform.Find("Root/AlterPanel/ShowPsd").GetComponent<Button>();
        showImg = showPsdBtn.transform.Find("showImg").transform;
        tips = _transform.Find("Root/tips").GetComponent<Text>();
        submitBtn.onClick.AddListener(Submit);
        closeBtn.onClick.AddListener(OnClose);
        inputPsd.onEndEdit.AddListener(delegate { AlterPassWord(); } );
        showPsdBtn.onClick.AddListener(ShowPassword);
    }
    public override void OnClose()
    {
        base.OnClose();
    }

    public void AlterPassWord()
    {
        if (!string.IsNullOrEmpty(inputPsd.text))
        {
            if (inputPsd.text.Length<6)
            {
                tips.text = "密码不能小于6个字符！";
                tips.color = Color.red;
            }
            else if (CommonMethod.checkString(inputPsd.text))
            {
                tips.text = "密码包含不支持的特殊字符，请重新输入！";
                tips.color = Color.black;
            }
            else
            {
            UserManager.Instance.AlterPassword(inputPsd.text);
            isValid = true;
            }

        }
    }
    public void Submit()
    {
        if (isValid)
        {
            UIManager.getInstance.OpenWindow<SkipWnd>();
            UIManager.getInstance.CloseWindow<AlterPsd>();
        }
    }
    public void ShowPassword()
    {

        if (!isShow)
        {
            showPsdBtn.GetComponent<Image>().enabled=false;
            showImg.gameObject.SetActive(true);
            inputPsd.contentType = InputField.ContentType.Standard;
            inputPsd.ActivateInputField();
            isShow = true;
        }
        else
        {
            showPsdBtn.GetComponent<Image>().enabled=true;
            showImg.gameObject.SetActive(false);
            inputPsd.contentType = InputField.ContentType.Password;
            inputPsd.ActivateInputField();
            isShow = false;

        }
    }
}
