using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatData", menuName = "ScriptableObject/Stat", order = int.MaxValue)]
public class StatData : ScriptableObject
{
    // 공격
    public int Damage;
    public int MinDamage;
    public int MaxDamage;
    public int HealPower;
    public float AttackSpeed;

    // 방어
    public int Hp;
    public int MaxHp;
    public int DamageReduction;

    // 기타
    public float MoveSpeed;
    public float SkillProgress;
}
