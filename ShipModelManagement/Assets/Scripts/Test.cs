using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Button Import;
    public string path;
    public string name;
    // Start is called before the first frame update
    void Start()
    {

        Import.onClick.AddListener(Click);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Click()
    {
        FileOperation.OpenSingleFile(out path, out name, "FBX");
        //FileOperation.OpenFilesPath(out path);
    }
}
