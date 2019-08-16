using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CommonMethod
{
  
    public static bool IsAllChinese(string str)
    {
        bool hasChinese = false;
        if (str == null)
        {
            return hasChinese;
        }
        char[] c = str.ToCharArray();
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i]< 0x4E00 && c[i] >0x29FA5)
            {
                hasChinese = false;
                return hasChinese;
            }
            
        }
        return hasChinese;
    }
    public static bool isChinese(char c)
    {
        return c >= 0x4E00 && c <= 0x9FA5;
    }

    public static bool checkString(string str)
    {
        char[] ch = str.ToCharArray();
        if (str != null)
        {
            for (int i = 0; i < ch.Length; i++)
            {
                if (isChinese(ch[i]))
                {
                    return true;
                }
            }
        }
        return false;
    }
}

