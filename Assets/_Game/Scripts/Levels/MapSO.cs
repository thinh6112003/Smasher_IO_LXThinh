using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapSO", menuName = "ScriptableObjects/MapSO")]
public class MapSO : ScriptableObject 
{
    public List<Map> Maps= new List<Map>();
    public Map GetMapByID(int iD)
    {
        return Maps[iD-1];
    }
}
