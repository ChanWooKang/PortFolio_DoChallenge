using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Util
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        if (go.TryGetComponent<T>(out T component) == false)
            component = go.AddComponent<T>();
        return component;
    }

    public static string ConvertEnum<T>(T value)
    {
        return Enum.GetName(typeof(T), value);
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if(recursive == false)
        {
            for(int i = 0; i < go.transform.childCount; i++)
            {
                Transform child = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || child.name == name)
                {
                    if (child.TryGetComponent<T>(out T component))
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInParent<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        return null;
    }

    public static GameObject FindChild(this GameObject go, string name = null, bool recursive = false)
    {
        Transform tr = FindChild<Transform>(go, name, recursive);
        if (tr == null)
            return null;
        return tr.gameObject;
    }
}
