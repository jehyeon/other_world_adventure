using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// State 변화 시에도 유지되는 CharacterContext
public class CharacterContext
{
    protected Character character;
    // Get
    public Character Character { get { return character; } }

    // Target
    private PriorityListDict<int, Character> attackTargets;
    private PriorityListDict<int, Character> attackedTargets;

    public CharacterContext()
    {
        attackTargets = new PriorityListDict<int, Character>();
        attackedTargets = new PriorityListDict<int, Character>();
    }

    // Set
    // Character
    public void SetCharacter(Character c)
    {
        character = c;
    }

    // Target
    public void ClearTargetsInfo()
    {
        attackTargets.Clear();
        attackedTargets.Clear();
    }

    // Attack Targets
    public void TryAddAttackTarget(int id, Character character)
    {
        attackTargets.TryAdd(id, character);
    }

    public bool TryGetNextTarget(out Character target)
    {
        if (attackTargets.TryGetFront(out target))
        {
            return true;
        }

        return false;
    }

    public void RemoveAttackTarget(int id)
    {
        attackTargets.Remove(id);
    }

    // Attacked Targets
    public List<Character> GetAttackedTargets()
    {
        return attackedTargets.GetValueList();
    }

    public void TryAddAttackedTarget(int id, Character character)
    {
        attackedTargets.TryAdd(id, character);
    }
}
