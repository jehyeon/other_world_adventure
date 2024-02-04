using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friendly : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // temp 임시 스탯 로드
        Managers.Instance.Stat.Load(stat, "friendlyTest");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
