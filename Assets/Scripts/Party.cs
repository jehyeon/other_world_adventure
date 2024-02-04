using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartyDirection
{
    Default,
    Top,
    Bottom,
    Left,
    Right
};

public class Party : MonoBehaviour
{
    //private List<Character> members = new List<Character>();
    private Character[] members;
    private PartyFormation formation;

    [Header("Temp")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float offset;

    private void Awake()
    {
        // temp
        members = transform.GetComponentsInChildren<Character>();

        if (formation == null)
        {
            formation = gameObject.AddComponent<PartyFormation>();
        }
        formation.SetOffset(offset);
        formation.SetPartyMoveSpeed(moveSpeed);
        formation.SetMembers(members);
    }
}
