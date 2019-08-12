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
    void OnWord()
    {
        ;
    }

    void OnAlter()
    {
     
    }

    void OnDelete()
    {
        ;
    }
}

