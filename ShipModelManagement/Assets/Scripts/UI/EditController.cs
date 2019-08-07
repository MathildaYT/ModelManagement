using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditController : MonoBehaviour
{
    public InputField modelName;
    public Dropdown modelType;
    public Button addModelBtn;
    public InputField modelContent;
    public Button confirmAddBtn;
    public Button importModelBtn;
    public Text tips;
    private ModelType type;
    // Start is called before the first frame update
    void Start()
    {
        
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
    public void Brower()
    {

    }

}
