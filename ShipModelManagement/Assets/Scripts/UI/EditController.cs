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
    public Button exportModelBtn;
    public Text tips;
    private ModelType type;
    public string path;
    public string name;
    // Start is called before the first frame update
    void Start()
    {
       openModelBtn.onClick.AddListener(OpenModelFile);
        //confirmAddBtn.onClick.AddListener();
        //exportModelBtn.onClick.AddListener();

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
        ModelDataManager.GetInstance.AddModel(modelName.text,modelContent.text,type,out tip);
        tips.text = tip;
    }
    public void OpenModelFile()
    {
        FileOperation.OpenSingleFile(out path, out name, "FBX");

        FileOperation.CopyFile(path,Constant.GetModelFullPath(name));
    }

}
