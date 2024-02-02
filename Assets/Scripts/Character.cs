using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Need to assign")]
    [SerializeField]
    private OverlapCircle targetDetect;

    protected Stat stat;

    private List<Character> attackTargets;
    private Character attackedTarget;

    public int ID
    {
        set { }
        get
        {
            if (ID == 0)
            {
                ID = gameObject.GetInstanceID();
            }

            return ID;
        }
    }

    private void Awake()
    {
        if (stat == null)
        {
            stat = gameObject.AddComponent<Stat>();
        }
    }

    private void Update()
    {
        
    }

    private void Start()
    {
        Managers.Instance.Stat.Load(stat, "test");
        stat.PrintStatLog();

        targetDetect.OnDetectTarget += OnDetectTarget;
        targetDetect.StartFindTarget();
    }

    private void OnDetectTarget(
        object sender, OnDetectTargetEventArgs eventArgs)
    {
        if (attackTargets.Find(target => 
            target.ID == eventArgs.TargetID) != null)
        {
            attackTargets.Add(eventArgs.Character);
        }   
    }
}
