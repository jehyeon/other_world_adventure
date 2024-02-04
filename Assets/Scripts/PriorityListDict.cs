using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityListDict<T1, T2>
{
    protected struct Priority
    {
        public T2 Value;
        public int Prior;

        public Priority(T2 v, int p)
        {
            Value = v;
            Prior = p;
        }
    };

    protected List<T1> list;
    protected Dictionary<T1, Priority> dict;

    public PriorityListDict()
    {
        Clear();
    }

    public void Clear()
    {
        list = new List<T1>();
        dict = new Dictionary<T1, Priority>();
    }

    public bool IsExist(T1 key)
    {
        if (dict.ContainsKey(key))
        {
            return true;
        }

        return false;
    }

    // Get
    public bool TryGetFront(out T2 value)
    {
        Priority p;
        if (list.Count == 0 || !dict.TryGetValue(list[0], out p))
        {
            value = default(T2);

            return false;
        }

        value = p.Value;

        return true;
    }

    public List<T1> GetList()
    {
        return list;
    }

    public List<T2> GetValueList()
    {
        List<T2> ret = new List<T2>();

        foreach (T1 key in list)
        {
            ret.Add(dict[key].Value);
        }

        return ret;
    }

    // Set
    // Add
    public void Add(T1 key, T2 value)
    {
        list.Add(key);
        // 새로 추가된 element의 우선 가중치는 0
        dict.Add(key, new Priority(value, 0));
    }

    public void TryAdd(T1 key, T2 value)
    {
        if (IsExist(key))
        {
            return;
        }

        Add(key, value);
    }

    // Remove
    public void Remove(T1 key)
    {
        dict.Remove(key);
        list.Remove(key);
    }

    // Update
    public void UpdatePriority(T1 key, int amount)
    {
        Priority p;
        if (!dict.TryGetValue(key, out p))
        {
            Debug.LogError("Can't find key in UpdatePriority (PriorityListDict.cs)");
            return;
        }

        p.Prior += amount;

        // 우선 순위 가중치가 수정되었으므로 정렬
        // TODO: 가중치 업데이트마다 정렬하면 문제가 될 수 있음
        // 문제 시 수정
        Sort();
    }

    // Sort
    public void Sort()
    {
        // TODO: 테스트 필요 (어그로 시스템 추가 할 때 확인)
        list.Sort(delegate (T1 key1, T1 key2)
        {
            Priority p1, p2;
            dict.TryGetValue(key1, out p1);
            dict.TryGetValue(key2, out p2);

            if (p1.Prior < p2.Prior)
            {
                return 1;
            }

            return 0;
        });
    }
}
