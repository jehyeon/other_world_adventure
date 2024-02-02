using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class OverlapCircle : MonoBehaviour
{
    [Header("Debug Mode")]
    [SerializeField]
    private bool DebugMode = false;
    [SerializeField]
    private Color gizmosColor = Color.red;

    public event EventHandler OnDetectTarget;

    [Header("Default")]
    [SerializeField]
    private float detectRange;
    [SerializeField]
    private LayerMask targetLayer;
    
    private void Awake()
    {
        if (DebugMode)
        {
            OverlapCircleForDebug debug = 
                gameObject.AddComponent<OverlapCircleForDebug>();

            debug.Set(gizmosColor, detectRange);
        }
    }

    private void Update()
    {
        Collider2D[] target = Physics2D.OverlapCircleAll(transform.position, detectRange, targetLayer);
        if (target != null)
        {
            OnDetectTarget?.Invoke(this, EventArgs.Empty);
            //Debug.Log(target.name);
            for (int i = 0; i < target.Length; i++)
            {
                Debug.Log(target[i].name);
            }
        }
    }

    // set
    public void Set(float range, LayerMask layer)
    {
        detectRange = range;
        targetLayer = layer;
    }
}
