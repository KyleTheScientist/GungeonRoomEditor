using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class TileDatabase
{
    public virtual Dictionary<string, string> Entries { get; set; }
    public string spriteDirectory;

    public string GetGUID(string enemyID)
    {
        return Entries[enemyID];
    }

    public string GetID(string guid)
    {
        foreach (var key in Entries.Keys)
        {
            if (Entries[key].Equals(guid))
                return key;
        }
        Debug.LogError($"EnemyDatabase: Could not find enemy with GUID {guid}");
        return null;
    }
}
