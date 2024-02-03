using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// State 변화 시에도 유지되는 CharacterContext
public class CharacterContext
{
    // Stat
    protected Stat currentStat;
    public Stat CurrentStat { get { return currentStat; } }

    // Target
    private PriorityListDict<int, Character> attackTargets;
    private PriorityListDict<int, Character> attackedTargets;

    public CharacterContext()
    {
        attackTargets = new PriorityListDict<int, Character>();
        attackTargets = new PriorityListDict<int, Character>();
    }

    // Set
    // Stat
    public void SetStat(Stat stat)
    {
        currentStat = stat;
    }

    // Target
    public void ClearTargetsInfo()
    {
        attackTargets.Clear();
        attackedTargets.Clear();
    }

    public void TryAddAttackTarget(int id, Character character)
    {
        attackTargets.TryAdd(id, character);
    }
}
