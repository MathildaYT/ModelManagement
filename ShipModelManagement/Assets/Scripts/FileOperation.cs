using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.IO;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]

public class OpenDialogFile
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;
    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;
    public String file = null;
    public int maxFile = 0;
    public String fileTitle = null;
    public int maxFileTitle = 0;
    public String initialDir = null;
    public String title = null;
    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;
    public String defExt = null;
    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;
    public String templateName = null;
    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenDialogDir
{
    public IntPtr hwndOwner = IntPtr.Zero;
    public IntPtr pidlRoot = IntPtr.Zero;
    public String pszDisplayName = null;
    public String lpszTitle = null;
    public UInt32 ulFlags = 0;
    public IntPtr lpfn = IntPtr.Zero;
    public IntPtr lParam = IntPtr.Zero;
    public int iImage = 0;
}

public class DllOpenFileDialog
{
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] OpenDialogFile ofn);
    //public static bool GetOpenFileName1([In, Out] OpenDialogFile ofn)

    //{
    //    return GetOpenFileName(ofn);
    //}

    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetSaveFileName([In, Out] OpenDialogFile ofn);

    [DllImport("shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern IntPtr SHBrowseForFolder([In, Out] OpenDialogDir ofn);

    [DllImport("shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool SHGetPathFromIDList([In] IntPtr pidl, [In, Out] char[] fileName);

}


public class FileOperation
{
    public static void OpenFilesPath(out string exportPath)
    {
        OpenDialogDir ofn2 = new OpenDialogDir();

        ofn2.pszDisplayName = new string(new char[2000]); ;     // 存放目录路径缓冲区    
        ofn2.lpszTitle = "Open Project";// 标题    
        //ofn2.ulFlags = BIF_NEWDIALOGSTYLE | BIF_EDITBOX; // 新的样式,带编辑框    
        IntPtr pidlPtr = DllOpenFileDialog.SHBrowseForFolder(ofn2);

        char[] charArray = new char[2000];
        for (int i = 0; i < 2000; i++)
            charArray[i] = '\0';

        DllOpenFileDialog.SHGetPathFromIDList(pidlPtr, charArray);
        string fullDirPath = new String(charArray);

        fullDirPath = fullDirPath.Substring(0, fullDirPath.IndexOf('\0'));

        Debug.Log(fullDirPath);//这个就是选择的目录路径。 

        exportPath = fullDirPath;
    }

    public static void OpenSingleFile(out string filepath, out string filename, string ext)
    {
        bool flag = false;

        filepath = "";

        filename = "";

        OpenDialogFile ofn = new OpenDialogFile();

        ofn.structSize = Marshal.SizeOf(ofn);

        var str = string.Format("files(*.{0})\0*.{1}\0files(*.{2})\0*.{3}\0", ext, ext, ext, ext);

        ofn.filter = str;

        ofn.file = new string(new char[256]);

        ofn.maxFile = ofn.file.Length;

        ofn.fileTitle = new string(new char[64]);

        ofn.maxFileTitle = ofn.fileTitle.Length;

        ofn.initialDir = "c:\\"; //默认路径

        ofn.defExt = ext;

        //注意 一下项目不一定要全选 但是0x00000008项不要缺少
        ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;    //OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR


        if (DllOpenFileDialog.GetOpenFileName(ofn))
        {

            //StartCoroutine(WaitLoad(ofn.file));//加载图片到panle

            Debug.Log("Selected file with full path: {0}" + ofn.file);
            flag = true;
        }
        if (flag == true)
        {
            //System.Diagnostics.Process.Start(ofn.file);
            Debug.Log(ofn.file);

            filepath = ofn.file;

            Debug.Log(ofn.fileTitle);

            if (ofn.fileTitle != null)
            {
                string[] names = ofn.fileTitle.Split('.');
                if (names.Length > 0)
                {
                    filename = names[0];
                }
            }
        }
    }

    public static string[] GetFileNames(string path, string extension = "")
    {
        string[] files = Directory.GetFiles(path);

        List<string> findnames = new List<string>();

        foreach (var name in files)
        {
			#if UNITY_STANDALONE_WIN
            string filename = name.Substring(name.LastIndexOf("\\") + 1);
            string[] names = filename.Split('.');
            
            if(names.Length > 1 && names[1] == extension)
            {
                findnames.Add(filename);
            }
			#elif UNITY_STANDALONE_OSX
			string filename = name.Substring(name.LastIndexOf("/") + 1);
			string[] names = filename.Split('.');

			if(names.Length > 1 && names[1] == extension)
			{
				findnames.Add(filename);
			}
			#endif
        }

        return findnames.ToArray();
    }
}
