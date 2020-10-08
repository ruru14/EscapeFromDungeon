using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Utility
{
    public static void ObjectVisibility(GameObject gameObject, bool flag)
    {
        try
        {
            float alpha = flag ? 1f : 0f;
            gameObject.GetComponent<CanvasGroup>().alpha = alpha;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = flag;
        }
        catch (MissingComponentException)
        {
            gameObject.AddComponent<CanvasGroup>();
            ObjectVisibility(gameObject, flag);
        }
    }

    public static string ScriptReader(string scriptFilePath)
    {
        TextAsset data = Resources.Load(scriptFilePath, typeof(TextAsset)) as TextAsset;
        if(data == null)
        {
            Debug.LogError("Cannot Find Script File : " + scriptFilePath);
            return "";
        }

        return data.ToString();

    }

    public static void Test(GameObject gameObject)
    {

    }

}
