using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building Info", menuName = "Scriptable Objects/Building Info", order = 0)]
public class BuildingInfo : Information
{
    public Unit[] units;
}
