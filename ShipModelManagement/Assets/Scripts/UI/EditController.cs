using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditController : MonoBehaviour
{
    public InputField modelName;
    public Dropdown modelType;
    public Button openModelBtn;
    public InputField modelContent;
    public Button confirmAddBtn;
   // public Button exportModelBtn;
    public Text tips;
    private ModelType type;
    public string path;
    public string name;
    // Start is called before the first frame update
    void Start()
    {
       openModelBtn.onClick.AddListener(OpenModelFile);
       confirmAddBtn.onClick.AddListener(AddModelMsg);
      // exportModelBtn.onClick.AddListener(ExportModelFile);

    }

    // Update is called once per frame
    void Update()
    {
        switch (modelType.value)
        {
            case 0:type = ModelType.TypeOne;
                break;
            case 1:
                type = ModelType.TypeTwo;
                break;
            case 2:
                type = ModelType.TypeTwo;
                break;
            default:
                break;
        }
    }
    public void AddModelMsg()
    {
        string tip;
        ModelDataManager.GetInstance.AddModel(modelName.text,modelContent.text, path, type,out tip);
        tips.text = tip;
        FileOperation.CopyFile(path,Constant.GetModelFullPath(name));
    }
    public void OpenModelFile()
    {
        FileOperation.OpenSingleFile(out path, out name, "FBX");

    }
    //public void ExportModelFile()
    //{
    //    FileOperation.OpenSingleFile(out path, out name, "FBX");
    //    FileOperation.CopyFile(Constant.GetModelFullPath(name),path);

    //}
}
