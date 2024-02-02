using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapCircleForDebug : MonoBehaviour
{
    private Color gizmosColor;
    private float detectRange;

    public void Set(Color color, float range)
    {
        gizmosColor = color;
        detectRange = range;
    }

    // for Debug
    public void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
