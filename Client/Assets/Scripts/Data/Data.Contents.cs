using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Stat
[Serializable]
public class Stat
{
    public int level;
    public int hp;
    public int attack;
}
[Serializable]
public class StatData : ILoader<int, Stat>
{
    public List<Stat> stats = new List<Stat>();

    public Dictionary<int, Stat> MakeDict()
    {   // ToDictionary() 사용해보기    
        Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
        foreach (Stat stat in stats)
            dict.Add(stat.level, stat);
        return dict;
    }
}
#endregion