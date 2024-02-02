using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Stat stat;

    private void Awake()
    {
        if (stat == null)
        {
            stat = gameObject.AddComponent<Stat>();
        }
    }

    private void Start()
    {
        Managers.Instance.Stat.Load(stat, "test");
        stat.PrintStatLog();
    }
}
