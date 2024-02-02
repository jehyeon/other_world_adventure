using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public void Load(Stat targetStat, string name)
    {
        StatData statData = Resources.Load<StatData>(
            string.Format("ScriptableObjects/Stat/{0}", name));

        targetStat.Load(statData);
    }
}
