using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

public class ModelDeleteWnd:UIWindow
{
    public Button confirmDeleteBtn;
    public Button cancelBtn;
    public Button closeBtn;
   
    public override void OnOpen(params object[] datas)
    {
        base.OnOpen();
        string name = datas[0].ToString();
        confirmDeleteBtn = _transform.Find("DeletePanel/ComfirmBtn").GetComponent<Button>();
        cancelBtn = _transform.Find("DeletePanel/CancelBtn").GetComponent<Button>();
        closeBtn = _transform.Find("DeletePanel/CloseBtn").GetComponent<Button>();
        closeBtn.onClick.AddListener(OnClose);
        confirmDeleteBtn.onClick.AddListener(delegate { DeleteModel(name);});
        cancelBtn.onClick.AddListener(OnClose);
    }
    public override void OnClose()
    {
        base.OnClose();
    }
    public void DeleteModel(string name)
    {
        ModelDataManager.GetInstance.DeleteModel(name);
        OnClose();
    }
    
}

