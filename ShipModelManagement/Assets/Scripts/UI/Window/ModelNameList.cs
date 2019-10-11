using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelNameList : MonoBehaviour
{
    public Text Name;
    public string ModelName;

    public Action<string> OnShowModel;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(OnClickModel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClickModel()
    {
        OnShowModel?.Invoke(ModelName);
    }
}
