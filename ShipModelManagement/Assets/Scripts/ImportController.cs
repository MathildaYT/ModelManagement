using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using CSUE.UniFBX;

public class ImportController : MonoBehaviour
{
    private static ImportController _instance;
    public static ImportController Instance
    {
        get
        {
            return _instance;
        }
    }
    public UniFBXImport uniFBXImport;
    public string _baseModelPath = "";
    public string _baseTexturePath = "";
    public string modelname = "x";

    void Start()
    {
        _instance = this;
        _baseModelPath = "file:///" + Application.streamingAssetsPath + "/model/";
        _baseTexturePath = "file:///" + Application.streamingAssetsPath + "/model/tex/";
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            uniFBXImport.setting.paths.urlModels = _baseModelPath;
            uniFBXImport.setting.paths.urlTextures = _baseTexturePath;

            uniFBXImport.setting.paths.filename = modelname;

            uniFBXImport.Load();
        }
    }
    public void Import(string modelname)
    {
        uniFBXImport.setting.paths.urlModels = _baseModelPath;
        uniFBXImport.setting.paths.urlTextures = _baseTexturePath;

        uniFBXImport.setting.paths.filename = modelname;

        uniFBXImport.Load();
    }
}
