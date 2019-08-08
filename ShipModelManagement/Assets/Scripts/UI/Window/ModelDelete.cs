using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

public class ModelDelete:UIWindow
{
    public Button confirmDeleteBtn;
    public Button cancelBtn;
    public Button closeBtn;
    string name;
    public override void OnOpen()
    {
        base.OnOpen();
        confirmDeleteBtn = _transform.Find("ComfirmBtn").GetComponent<Button>();
        cancelBtn = _transform.Find("CancelBtn").GetComponent<Button>();
        closeBtn = _transform.Find("closeBtn").GetComponent<Button>();
        closeBtn.onClick.AddListener(OnClose);
        confirmDeleteBtn.onClick.AddListener(DeleteModel);
        cancelBtn.onClick.AddListener(OnClose);
    }
    public override void OnClose()
    {
        base.OnClose();
    }
    public void DeleteModel()
    {
        ModelDataManager.GetInstance.DeleteModel(name);
    }
    
}

