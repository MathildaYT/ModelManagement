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

    public T Open<T>(params object[] datas) where T : UIPanel, new()
    {
        var uiname = typeof(T).Name;
        if (!_UIlist.ContainsKey(uiname))
        {
            var uipanel = new T();

            uipanel.OnInit(uiname);

            uipanel.OnOpen(datas);

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
        retpanel.OnOpen(datas);

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
    public UIPanel CurrentPanel()
    {
        if (_backstack.Count > 0)
        {
            return _backstack.Peek();
        }

        return null;
    }
    //---------------------------------------------------
    protected Dictionary<string, UIBar> _bars = new Dictionary<string, UIBar>();
    UIBar _currentbar;
    public T OpenBar<T>() where T : UIBar, new()
    {
        var uiname = typeof(T).Name;
        if (!_bars.ContainsKey(uiname))
        {
            var bar = new T();

            bar.OnInit(uiname);
            bar.OnOpen();

            _bars.Add(uiname, bar);

            _currentbar = bar;
            return bar;
        }
        var ret = _bars[uiname];
        ret.OnOpen();

        _currentbar = ret;
        return ret as T;
    }

    public void CloseBar<T>()
    {
        var uiname = typeof(T).Name;
        if (_bars.ContainsKey(uiname))
        {
            _bars[uiname].OnClose();
            _currentbar = null;
        }
    }

    public UIBar CurrentBar()
    {
        return _currentbar;
    }
    //---------------------------------------------------
    public T OpenWindow<T>(params object[] datas) where T : UIWindow, new()
    {
        var curpanel = CurrentPanel();
        if (curpanel != null)
        {
            return curpanel.OpenWindow<T>(datas);
        }

        return null;
    }

    public void CloseWindow<T>()
    {
        var curpanel = CurrentPanel();
        if (curpanel != null)
        {
            curpanel.CloseWindow<T>();
        }
    }

    public void ClearAll()
    {
        //bar
        _bars.Clear();
        //panel
        _UIlist.Clear();

        _backstack.Clear();
    }
}
