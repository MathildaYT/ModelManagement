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
}

