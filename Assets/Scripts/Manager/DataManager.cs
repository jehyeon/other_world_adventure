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

    public AnimatorOverrideController LoadAnimator(Camp camp, Job job, int jobType)
    {

        AnimatorOverrideController controller = Resources.Load<AnimatorOverrideController>(
            string.Format("Animations/{0}/{1}/{2}/animator", 
                camp.ToString(), 
                job.ToString(), 
                jobType));

        return controller;
    }
}
