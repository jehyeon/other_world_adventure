using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    protected override void Start()
    {
        base.Start();

        // temp 임시 스탯 로드
        Managers.Instance.Stat.Load(stat, "enemyTest");
    }
}
