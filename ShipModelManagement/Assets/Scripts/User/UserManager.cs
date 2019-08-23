using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public enum UserType
{
    Normal,
    Administrator,
}
public class UserManager
{
    private static UserManager _instance;
    public static UserManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UserManager();
            }
            return _instance;
        }
    }
    //-------------------------------------------
    public UserType Type { get; set; }


    //-------------------------------------------
    public void Init()
    {
        var check = DBInitController.GetInstance.DB.CheckTable<MsgData>();
        if (!check)
        {
            DBInitController.GetInstance.DB.CreateTable<MsgData>();
            InitData();
        }


        PrintAllUser();
    }

    public bool LogIn(string _userName, string _passWord, out string log, UserType type = UserType.Administrator)
    {
        log = "";
        Type = type;
        if (type == UserType.Administrator)
        {
            var check = DBInitController.GetInstance.DB.CheckTable<MsgData>();
            if (!check)
            {
                log = "没有用户信息";
                return false;
            }
            var datas = DBInitController.GetInstance.DB.GetData<MsgData>();
            var d = datas.Where(x => (string.Compare(x.Name, _userName, true) == 0)).FirstOrDefault();
            if (d != null && (string.Compare(d.Password, _passWord, true) == 0))
            {
                log = "登录成功!";

                return true;
            }
            else
            {
                log = "用户名或密码错误，请重新输入!";

            }

        }


        return false;

    }
    public void Register(string _userName, string _passWord, out string log)
    {
        var check = DBInitController.GetInstance.DB.CheckTable<MsgData>();
        if (!check)
        {
            DBInitController.GetInstance.DB.CreateTable<MsgData>();
        }

        var datas = DBInitController.GetInstance.DB.GetData<MsgData>();
        var d = datas.Where(x => x.Name == _userName).FirstOrDefault();
        if (d != null)
        {
            log = "该用户名已经存在";
            return;
        }
        var data = new MsgData();
        data.Name = _userName;
        data.Password = _passWord;
        DBInitController.GetInstance.DB.CreateData(data);
        log = "";
    }
    public void SaveData(string userName, string passWord)
    {
        var data = new MsgData();
        data.Name = userName;
        data.Password = passWord;
        DBInitController.GetInstance.DB.CreateData<MsgData>(data);
    }
    public void PrintAllUser()
    {
        var datas = DBInitController.GetInstance.DB.GetData<MsgData>();
        foreach (var d in datas)
        {
            Debug.Log(d.ToString());
        }
    }
    public void DeleteAllUser()
    {
        var check = DBInitController.GetInstance.DB.CheckTable<MsgData>();
        if (!check)
        {
            return;
        }
        var datas = DBInitController.GetInstance.DB.GetData<MsgData>();
        foreach (var item in datas)
        {
            DBInitController.GetInstance.DB.DeleteData<MsgData>(item);
        }
        InitData();
    }
    public void AlterPassword(string Password)
    {

        var check = DBInitController.GetInstance.DB.CheckTable<MsgData>();
        if (!check)
        {
            return;
        }
        var datas = DBInitController.GetInstance.DB.GetData<MsgData>();
        var d = datas.Where(x => x.Name == "Admin").FirstOrDefault();
        if (d != null)
        {
            d.Password = Password;
        }
        DBInitController.GetInstance.DB.Update(d);

    }
    public void ResetPassword()
    {

        var check = DBInitController.GetInstance.DB.CheckTable<MsgData>();
        if (!check)
        {
            return;
        }
        var datas = DBInitController.GetInstance.DB.GetData<MsgData>();
        var d = datas.Where(x => x.Name == "Admin").FirstOrDefault();
        if (d != null)
        {
            d.Password = "admin2019";
        }
        DBInitController.GetInstance.DB.Update(d);

    }
    public void InitData()
    {
        var datas = DBInitController.GetInstance.DB.GetData<MsgData>();
        var d = datas.Where(x => (string.Compare(x.Name, "Admin", true) == 0)).FirstOrDefault();
        if (d == null)
        {
            var data = new MsgData();
            data.Name = "Admin";
            data.Password = "admin2019";
            DBInitController.GetInstance.DB.CreateData<MsgData>(data);
        }
    }
}
