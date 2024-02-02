using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggro : MonoBehaviour
{
    private bool bStartedCombat;
    //private 

    private void Awake()
    {
        bStartedCombat = false;
    }

    private void Start()
    {
        
    }

    // 전투 시작, 종료
    public void StartCombat()
    {
        bStartedCombat = true;
        //StartCoroutine();
    }

    public void EndCombat()
    {
        bStartedCombat = false;
        //StopCoroutine();
    }

    IEnumerator CalculateAggro()
    {
        yield return new WaitForSeconds(0.25f);
        SortAggro();
    }

    public Character GetHighestAggro()
    {
        return null;
    }

    public void UpdateAggro()
    {

    }

    public void SortAggro()
    {

    }
}
