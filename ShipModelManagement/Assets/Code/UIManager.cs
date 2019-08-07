using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    static UIManager _instance = null;
    public static UIManager getInstance
    {
        get
        {
            if (_instance == null)
                _instance = new UIManager();
            return _instance;
        }
    }
    Dictionary<string, UIPanel> _UIlist = new Dictionary<string, UIPanel>();

    Stack<UIPanel> _backstack = new Stack<UIPanel>();

    public T Open<T>() where T : UIPanel, new()
    {
        var uiname = typeof(T).Name;
        if (!_UIlist.ContainsKey(uiname))
        {
            var uipanel = new T();

            uipanel.OnInit(uiname);

            uipanel.OnOpen();

            _UIlist[uiname] = uipanel;

            if (_backstack.Count > 0)
            {
                var lastpanel = _backstack.Peek();
                lastpanel.OnClose();
            }
            _backstack.Push(uipanel);

            return uipanel;
        }
        var retpanel = _UIlist[uiname];
        retpanel.OnOpen();

        if(_backstack.Count > 0)
        {
            var lastpanel = _backstack.Peek();
            lastpanel.OnClose();
        }
        _backstack.Push(retpanel);

        return retpanel as T;
    }

    public void Close<T>()
    {
        var uiname = typeof(T).Name;
        if(_UIlist.ContainsKey(uiname))
        {
            _UIlist[uiname].OnClose();
        }

        _backstack.Pop();
    }

    public void Back()
    {
        if (_backstack.Count > 1)
        {
            var curpanel = _backstack.Pop();
            curpanel.OnClose();
            var lastpanel = _backstack.Peek();
            lastpanel.OnOpen();
        }
    }
}
