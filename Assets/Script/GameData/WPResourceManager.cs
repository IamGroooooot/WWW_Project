using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPResourceManager : MonoBehaviour
{
    public static WPResourceManager instance = null;

    private Dictionary<string, Object> resourceData;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        instance = this;
        resourceData = new Dictionary<string, Object>();
    }

    // Resources.Load 대신 WPResourceManager.instance.GetResource를 쓰면 됩니다.
    public T GetResource<T>(string path) where T : Object
    {
        if (resourceData.ContainsKey(path))
        {
            WPGameCommon._WPDebug("해당 리소스 취득 : " + path);
            return resourceData[path] as T;
        }
        else
        {
            T newResource = Resources.Load<T>(path);
            if (newResource != null)
            {
                WPGameCommon._WPDebug("해당 리소스 추가 : " + path + " / " + newResource.GetType());
                resourceData.Add(path, newResource);
                return resourceData[path] as T;
            }
            else
            {
                WPGameCommon._WPDebug("해당 리소스 부재 : " + path);
                return null;
            }
        }
    }


}
