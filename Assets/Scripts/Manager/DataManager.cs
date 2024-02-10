using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public void LoadStatAtTarget(Stat targetStat, string name)
    {
        StatData statData = Resources.Load<StatData>(
            string.Format("ScriptableObjects/Stat/{0}", name));

        targetStat.Load(statData);
    }
}
