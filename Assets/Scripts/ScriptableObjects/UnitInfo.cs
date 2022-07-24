using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Info", menuName = "Scriptable Objects/Unit Info", order = 1)]
public class UnitInfo : Information
{
    public int damage;
    public int attackDistance;
    public int attackSpeed;
    public int movementSpeed;
}
