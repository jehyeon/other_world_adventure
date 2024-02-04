using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyFormation : MonoBehaviour
{
    private PartyDirection partyDir = PartyDirection.Default;

    private Character[] partyMembers;

    private Vector3 position;
    private Vector3 top;
    private Vector3 bottom;
    private Vector3 right;
    private Vector3 left;

    private Vector3[] HBBT = new Vector3[4];    // Head, Body, Body, Tail
    private Vector3[] diffs = new Vector3[4];

    private float partyMoveSpeed = 1f;
    private float memberPosOffset = 1f;       // 각 파티원이 위치할 중심으로부터의 거리

    private void Awake()
    {
        partyDir = PartyDirection.Default;

        SetOffset(memberPosOffset);      // default
    }

    private void Start()
    {
        Rotate(PartyDirection.Right);
    }

    private void Update()
    {
        if (partyMembers == null)
        {
            return;
        }

        // temp
        // 회전
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Rotate(PartyDirection.Top);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Rotate(PartyDirection.Right);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Rotate(PartyDirection.Bottom);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Rotate(PartyDirection.Left);
        }

        // temp
        // 포메이션 위치로 이동 + 파티 이동
        // 파티 이동
        Vector2 dir;
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        dir = new Vector2(h, v);
        dir = dir.normalized;

        if (h != 0 || v != 0)
        {
            position += (Vector3)dir * Time.deltaTime * partyMoveSpeed;
        }

        // 파티원 목적지를 포메이션 위치로 
        for (int i = 0; i < 4; i++)
        {
            if (!partyMembers[i].IsDead)
            {
                partyMembers[i].SetDest(position + HBBT[i]);
            }
        }
    }

    public void SetOffset(float offset)
    {
        memberPosOffset = offset;
        Vector3 horizontal = new Vector3(1f, 0f);
        Vector3 vertical = new Vector3(0f, 1f);
        top = vertical * memberPosOffset;
        bottom = -1 * vertical * memberPosOffset;
        right = horizontal * memberPosOffset;
        left = -1 * horizontal * memberPosOffset;
    }

    public void SetPartyMoveSpeed(float moveSpeed)
    {
        partyMoveSpeed = moveSpeed;
    }

    public void SetMembers(Character[] members)
    {
        partyMembers = members;
    }

    public void Rotate(PartyDirection dir)
    {
        if (partyDir == dir)
        {
            return;
        }

        partyDir = dir;
        switch (partyDir)
        {
            case PartyDirection.Top:
                HBBT[0] = top;
                HBBT[1] = left;
                HBBT[2] = right;
                HBBT[3] = bottom;
                break;
            case PartyDirection.Bottom:
                HBBT[0] = bottom;
                HBBT[1] = right;
                HBBT[2] = left;
                HBBT[3] = top;
                break;
            case PartyDirection.Left:
                HBBT[0] = left;
                HBBT[1] = bottom;
                HBBT[2] = top;
                HBBT[3] = right;
                break;
            case PartyDirection.Right:
                HBBT[0] = right;
                HBBT[1] = top;
                HBBT[2] = bottom;
                HBBT[3] = left;
                break;
        }
    }
}
