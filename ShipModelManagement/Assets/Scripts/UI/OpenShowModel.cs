using System;
using System.Collections.Generic;
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
        ;
    }
}

