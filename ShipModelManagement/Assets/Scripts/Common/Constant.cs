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
    public const string ModelViewSceneName = "ModelViewScene";

    //public const string LoginSceneName = "SampleScene";
    public static string GetModelPath()
    {
        return "file:///" + Application.streamingAssetsPath + "/model/";
    }
    public static string GetModelPath(string name)
    {
        return Application.streamingAssetsPath + "/model/"+name;
    }
    public static string GetModelFullPath(string filename)
    {
        return string.Format("{0}/{1}.FBX", Application.streamingAssetsPath + "/model/",filename) ;
    }
    public static string GetWordPath(string filename)
    {
        return string.Format("{0}/{1}", Application.streamingAssetsPath + "/Word", filename);
    }
    public static string GetWordFullPath(string filename)
    {
        return string.Format("file:///{0}/{1}", Application.streamingAssetsPath + "/Word", filename);
    }
    public static string GetModelTexPath()
    {
        return "file:///" + Application.streamingAssetsPath + "/model/tex/";
    }
}
