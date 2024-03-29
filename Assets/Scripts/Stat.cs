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
    private float attackRange;

    // 방어
    private int hp;
    private int maxHp;
    private int damageReduction;

    // 기타
    private float moveSpeed;
    private float skillProgress;

    // get
    public float AttackSpeed { get { return attackSpeed; } }
    public float AttackRange { get { return attackRange; } }
    public float MoveSpeed { get { return moveSpeed; } }

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
        attackRange = data.attackRange;
        //hp = data.Hp;
        maxHp = data.MaxHp;
        damageReduction = data.DamageReduction;
        moveSpeed = data.MoveSpeed;
        //skillProgress = data.SkillProgress

        Init();
    }

    public int CalculAttackDamage()
    {
        return damage + Random.Range(minDamage, maxDamage + 1);
    }

    public bool UpdateHP(int change)
    {
        hp += change;
        Debug.Log(string.Format("{0} 데미지, 남은 체력: {1}", change, hp));

        if (hp < 1)
        {
            // Die
            return true;
        }

        return false;
    }

    // 체력이 0이 되면 return true
    public bool Attacked(int damage)
    {
        int finalDamage = damage - damageReduction;

        if (finalDamage > 0)
        {
            return UpdateHP(-1 * finalDamage);
        }

        return false;
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
            "attackRange: {6}\n" +
            "maxHp: {7}\n" +
            "damageReduction: {8}\n" +
            "moveSpeed: {9}\n",
            name, damage, minDamage, maxDamage, healPower,
            attackSpeed, attackRange, maxHp, damageReduction, moveSpeed));
    }    
}
