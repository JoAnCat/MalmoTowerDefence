using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dwarf", menuName = "Card")]
public class DwarfData : ScriptableObject
{
    public int strength;
    public int health;
    public int reach;
}
