using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelDataController : MonoBehaviour
{
    public Text Name;
    public Text Type;
    public Button Word;
    public Button Alter;
    public Button Delete;

    public string ModelName;
    public string WordPath;

    private void Awake()
    {
        Word.onClick.AddListener(OnWord);
        Alter.onClick.AddListener(OnAlter);
        Delete.onClick.AddListener(OnDelete);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnWord()
    {
        ;
    }

    void OnAlter()
    {
        UIManager.getInstance.Open<ModelAlter>(ModelName);
    }

    void OnDelete()
    {
        UIManager.getInstance.OpenWindow<ModelDeleteWnd>(ModelName);
    }
}
