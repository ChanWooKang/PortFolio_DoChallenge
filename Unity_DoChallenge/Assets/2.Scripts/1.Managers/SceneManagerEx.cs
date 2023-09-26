using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Define;


public class SceneManagerEx
{ 
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }
    Dictionary<eCursor, Texture2D> dict_Cursor;

    public void Init()
    {
        dict_Cursor = new Dictionary<eCursor, Texture2D>();
    }

    public IEnumerator LoadCoroutine(eScene scene)
    {
        Managers.Clear();
        string sceneName = Util.ConvertEnum(scene);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            yield return null;
        }
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }

    public void AddCursor()
    {
        if(CurrentScene.CurrScene == eScene.MainScene)
        {
            if(CurrentScene.ListCursor.Count > 0)
            {
                for(int i = 0; i < CurrentScene.ListCursor.Count; i++)
                {
                    dict_Cursor.Add((eCursor)i, CurrentScene.ListCursor[i]);
                }
            }
        }
    }
    
    public Texture2D LoadCursor(eCursor type)
    {
        if (dict_Cursor.TryGetValue(type, out Texture2D cursor) == false)
        {
            Debug.Log($"SceneManager : Failed To Load Cursor TYPE({type})");
            return null;
        }
        else
            return cursor;
    }
}
