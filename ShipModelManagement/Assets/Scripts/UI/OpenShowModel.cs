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
        var data = ModelDataManager.GetInstance.GetModelData(ModelName);
        if (data != null)
        {
            if (data.wordpath!="")
            {
                var path=Constant.GetWordFullPath(data.wordpath);
                Application.OpenURL(path);
            }
        }

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
            var newPath = string.Format(@"{0}\{1}", path, ModelName);
            var copyfullpath = string.Format(@"{0}\{1}.FBX",newPath, data.modelPath);
            var copyWordPath = string.Format(@"{0}\{1}", newPath, data.wordpath);
            if (File.Exists(newPath))
            {
                if (data.modelPath!="")
                {
                    
                FileOperation.CopyFile(Constant.GetModelPath(data.modelPath)+".FBX", copyfullpath);
                    
                }
                if (data.wordpath!="")
                {
                    Debug.Log(Constant.GetWordPath(data.wordpath));
                    Debug.Log(newPath);
                    FileOperation.CopyFile(Constant.GetWordPath(data.wordpath), copyWordPath);

                }
            }
            else
            {
                Directory.CreateDirectory(newPath);
                if (data.modelPath != "")
                {
                FileOperation.CopyFile(Constant.GetModelPath(data.modelPath) + ".FBX", copyfullpath);
                }
                if (data.wordpath != "")
                {
                    Debug.Log(Constant.GetWordPath(data.wordpath));
                    Debug.Log(newPath);
                    FileOperation.CopyFile(Constant.GetWordPath(data.wordpath), copyWordPath);
                   
                }
            }
        }
    }
}

