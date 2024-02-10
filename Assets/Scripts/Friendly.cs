using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Friendly : Character
{
    protected Camp camp;
    protected Job job;
    protected int jobType;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // temp 임시 스탯 로드
        Managers.Instance.DataManager.LoadStatAtTarget(stat, "friendlyTest");


        // temp 임시
        camp = Camp.Human;
        job = Job.Archer;
        jobType = 1;

        animator.runtimeAnimatorController =
            Managers.Instance.DataManager.LoadAnimator(camp, job, jobType);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
