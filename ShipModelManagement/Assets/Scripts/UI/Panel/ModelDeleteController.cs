using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

public class ModelDeleteController:UIWindow
{
    public Button confirmDeleteBtn;
    public Button cancelBtn;
    string name;
    public override void OnOpen()
    {
        base.OnOpen();
        confirmDeleteBtn = _transform.Find("ComfirmBtn").GetComponent<Button>();
        cancelBtn = _transform.Find("CancelBtn").GetComponent<Button>();
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

