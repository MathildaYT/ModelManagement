using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelOperation : MonoBehaviour
{
    public string name;
    public string type;
    public Button wordBtn;
    public Button alterBtn;
    public Button deleteBtn;
    public Button browseBtn;
    public Button exportBtn;
        
    // Start is called before the first frame update
    void Start()
    {
        alterBtn.onClick.AddListener(AlterModel);
        deleteBtn.onClick.AddListener(DeleteModel);
        browseBtn.onClick.AddListener(BrowseModel);
        exportBtn.onClick.AddListener(ExportModel);
        wordBtn.onClick.AddListener(HasWord);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AlterModel()
    {

    }
    public void DeleteModel()
    {

    }
    public void BrowseModel()
    {

    }
    public void ExportModel()
    {

    }
    public void HasWord()
    {

    }
}
