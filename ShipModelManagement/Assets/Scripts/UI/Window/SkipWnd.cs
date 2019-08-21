using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkipWnd:UIWindow
{
    public Button resetBtn;
    public override void OnOpen(params object[] datas)
    {
        base.OnOpen(datas);
        resetBtn = _transform.Find("Root/Refesh").GetComponent<Button>();
        resetBtn.onClick.AddListener(Reset); 
    }
    public override void OnClose()
    {
        base.OnClose();
    }
    public void Reset()
    {
        SceneManager.LoadScene(Constant.LoginSceneName);
    }
}

