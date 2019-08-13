using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class OpenShowModel : MonoBehaviour
{
    public Text Name;
    public Text Type;
    public Button Word;
    public Button browse;
    public Button export;
    string path;
    string name;
    public string ModelName;
    public string WordPath;


    private void Awake()
    {
        Word.onClick.AddListener(OnWord);
        browse.onClick.AddListener(OnBrowse);
        export.onClick.AddListener(OnExport);
    }
    void OnWord()
    {
        ;
    }

    void OnBrowse()
    {
        UIManager.getInstance.OpenWindow<ModelShow>(ModelName);

    }

    void OnExport()
    {
        var data = ModelDataManager.GetInstance.GetModelData(ModelName);
        if (data != null)
        {

            FileOperation.OpenFilesPath(out path);
            var newPath = string.Format("{0}/{1}/", path, ModelName);
            if (File.Exists(newPath))
            {
                FileOperation.CopyFile(Constant.GetModelPath(data.modelPath)+".FBX", newPath);
            }
            else
            {
                Directory.CreateDirectory(newPath);
                FileOperation.CopyFile(Constant.GetModelPath(data.modelPath) + ".FBX", newPath);
            }
        }
    }
}

