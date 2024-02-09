using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tab))]
public class AnimationView : MonoBehaviour
{
    private Tab types;
    public Tab Tab { get { return types; } }

    private float intervalDist = 1.0f;
    private int intervalCount = 1;

    private void Awake()
    {
        types = GetComponent<Tab>();
    }

    public void SetInterval(int count, float dist)
    {
        intervalCount = count;
        intervalDist = dist;
    }

    public void AddView(List<GameObject> objs)
    {
        GameObject go = new GameObject("view");
        go.transform.SetParent(transform);

        //Vector3 startPos = new Vector3(
        //    intervalCount / 2 * intervalDist,
        //    Mathf.Ceil(objs.Count / intervalCount) * -intervalDist,
        //    0f);
        Vector3 startPos = Vector3.zero;

        for (int i = 0; i < objs.Count; i++)
        {
            objs[i].transform.SetParent(go.transform);
            objs[i].transform.position = startPos + new Vector3(intervalDist * i, 0f, 0f);
        }

        types.AddTab(go);

        // temp (default)
        types.OpenTab(0);
    }
}
