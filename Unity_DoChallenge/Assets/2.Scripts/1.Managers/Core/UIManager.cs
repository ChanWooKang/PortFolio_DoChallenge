using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class UIManager
{
    int _order = 10;
    Stack<UI_Popup> _popStack = new Stack<UI_Popup>();

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UIs");
            if (root == null)
                root = new GameObject { name = "@UIs" };
            return root;
        }
    }
    
    public void SetCanvan(GameObject go , bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = sort;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T MakeWorldSpace<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        //GameObject go = Managers._resource.Instantiate($"UI/WorldSpace/{name}");
        GameObject go = PoolingManager._pool.InstantiateAPS(name);
        if (parent != null)
            go.transform.SetParent(parent);
        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return Util.GetOrAddComponent<T>(go);
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        //GameObject go = Managers._resource.Instantiate($"UI/Popup/{name}");
        GameObject go = PoolingManager._pool.InstantiateAPS(name);
        T popup = Util.GetOrAddComponent<T>(go);
        _popStack.Push(popup);

        go.transform.SetParent(Root.transform);

        return popup;
    }

    public void ClosePopupUI()
    {
        if (_popStack.Count == 0)
            return;

        UI_Popup popup = _popStack.Pop();
        //Managers._resource.Destroy(popup.gameObject);
        popup.gameObject.DestroyAPS();

        popup = null;
        _order--;
    }
    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popStack.Count == 0)
            return;

        if (_popStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed");
            return;
        }

        ClosePopupUI();
    }

    public void CloseAllPopupUI()
    {
        while (_popStack.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();
    }
}
