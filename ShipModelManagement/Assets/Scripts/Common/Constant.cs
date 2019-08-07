using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant
{
    public const string LoginSceneName = "LoginScene";
    //public const string ViverSceneName = "Viewer";
    public const string ModelRegisterName ="ModelRegister";
    public const string ModelLibraryName ="ModelLibrary";
    public const string ModelViewerName ="ModelShow";

    //public const string LoginSceneName = "SampleScene";
    public static string GetModelPath()
    {
        return Application.streamingAssetsPath + "/model/";
    }
    public static string GetModelFullPath(string filename)
    {
        return string.Format("{0}/{1}.FBX", Application.streamingAssetsPath + "/model/",filename) ;
    }

    public static string GetModelTexPath()
    {
        return "file:///" + Application.streamingAssetsPath + "/model/tex/";
    }
}
