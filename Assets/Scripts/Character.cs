using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Stat stat;

    [SerializeField]
    private OverlapCircle detect;

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

        detect.OnDetectTarget += OnDetectTarget;
    }

    private void OnDetectTarget(object sender, EventArgs eventArgs)
    {
        Debug.Log("타겟 탐지");
    }
}
