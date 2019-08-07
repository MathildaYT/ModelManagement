using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject modelShowPanel;
    public GameObject allModelsPanel;
    private List<Button> ModelList = new List<Button>();
    public Button originObject;
    public Transform parentTransForm;
    public Button backBtn;
    void Start()
    {
        InitList();
        backBtn.onClick.AddListener(Back);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddList();
        }
    }
    public void OpenUI(string modelName)
    {
        allModelsPanel.SetActive(false);
        modelShowPanel.SetActive(true);
        ImportController.Instance.Import(modelName);
    }
    public void AddList()
    {
        var model=  GameObject.Instantiate(originObject, parentTransForm);
        model.onClick.AddListener(delegate { OpenUI(model.name); });
        ModelList.Add(model);
    }
    public void InitList()
    {
        foreach (Button btn in parentTransForm.GetComponentsInChildren<Button>())
        {
            ModelList.Add(btn);
            Debug.Log(btn.name);
        }
        foreach (var item in ModelList)
        {
            item.onClick.AddListener(delegate { OpenUI(item.name); });
        }
    }
    public void Back()
    {
        allModelsPanel.SetActive(true);
        modelShowPanel.SetActive(false);
    }
        
}
