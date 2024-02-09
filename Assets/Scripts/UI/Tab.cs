using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour
{
    private List<GameObject> views = new List<GameObject>();
    private int viewIndex = -1;

    public int AddTab(GameObject go)
    {
        views.Add(go);
        go.transform.SetParent(transform);
        go.SetActive(false);

        return views.Count - 1;
    }

    public void OpenTab(int index)
    {
        if (index < 0 || index >= views.Count)
        {
            return;
        }

        if (viewIndex != -1)
        {
            views[viewIndex].SetActive(false);
        }
        viewIndex = index;
        views[viewIndex].SetActive(true);
    }

    public GameObject GetTab(int index)
    {
        return views[index];
    }

    public void Previous()
    {
        if (viewIndex < 1)
        {
            return;
        }

        OpenTab(viewIndex - 1);
    }

    public void Next()
    {
        if (viewIndex > views.Count - 2)
        {
            return;
        }

        OpenTab(viewIndex + 1);
    }

    public void Clear()
    {
        foreach(GameObject go in views)
        {
            Destroy(go);
        }

        views.Clear();
        viewIndex = -1;
    }
}
