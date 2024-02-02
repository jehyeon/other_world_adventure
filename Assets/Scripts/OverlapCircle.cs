using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class OnDetectTargetEventArgs : EventArgs
{
    public int TargetID;
    public Character Character;
}

public class OverlapCircle : MonoBehaviour
{
    [Header("Debug Mode")]
    [SerializeField]
    private bool DebugMode = false;
    [SerializeField]
    private Color gizmosColor = Color.red;

    public event EventHandler<OnDetectTargetEventArgs> OnDetectTarget;

    [Header("Default")]
    [SerializeField]
    private float detectRange;
    [SerializeField]
    private LayerMask targetLayer;

    private Collider2D[] targetCols;

    private void Awake()
    {
        if (DebugMode)
        {
            OverlapCircleForDebug debug = 
                gameObject.AddComponent<OverlapCircleForDebug>();

            debug.Set(gizmosColor, detectRange);
        }
    }

    public void StartFindTarget()
    {
        StartCoroutine("FindTarget", targetLayer);
    }

    public void StopFindTarget()
    {
        StopCoroutine("FindTarget");
    }

    IEnumerator FindTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);

            targetCols = Physics2D.OverlapCircleAll(transform.position, detectRange, targetLayer);
            
            // need to optimization
            for (int i = 0; i < targetCols.Length; i++)
            {
                int id = targetCols[i].GetInstanceID();
                OnDetectTarget?.Invoke(this, new OnDetectTargetEventArgs { 
                    TargetID = id, Character = targetCols[i].GetComponent<Character>()});
            }
        }
    }

    private void Update()
    {
    }

    // set
    public void Set(float range, LayerMask layer)
    {
        detectRange = range;
        targetLayer = layer;
    }
}
