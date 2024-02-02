using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    // 공격
    private int damage;
    private int minDamage;
    private int maxDamage;
    private int healPower;
    private float attackSpeed;

    // 방어
    private int hp;
    private int maxHp;
    private int damageReduction;

    // 기타
    private float moveSpeed;
    private float skillProgress;

    // 현재 체력 및 스킬 진행도 초기화
    public void Init()
    {
        hp = maxHp;
        skillProgress = .0f;
    }

    public void Load(StatData data)
    {
        damage = data.Damage;
        minDamage = data.MinDamage;
        maxDamage = data.MaxDamage;
        healPower = data.HealPower;
        attackSpeed = data.AttackSpeed;
        //hp = data.Hp;
        maxHp = data.MaxHp;
        damageReduction = data.DamageReduction;
        moveSpeed = data.MoveSpeed;
        //skillProgress = data.SkillProgress

        Init();
    }

    // for test
    public void PrintStatLog()
    {
        // 스탯 업데이트 시 아래도 수정 부탁
        Debug.Log(string.Format(
            "{0} Stat\n" +
            "damage: {1}\n" +
            "minDamage: {2}\n" +
            "maxDamage: {3}\n" +
            "healPower: {4}\n" +
            "attackSpeed: {5}\n" +
            "maxHp: {6}\n" +
            "damageReduction: {7}\n" +
            "moveSpeed: {8}\n",
            name, damage, minDamage, maxDamage, healPower,
            attackSpeed, maxHp, damageReduction, moveSpeed));
    }    
}
