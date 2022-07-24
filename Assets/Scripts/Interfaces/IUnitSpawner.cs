using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitSpawner
{
    public Vector2Int SpawnPointOffset { get; set; }

    public void SpawnUnit(Unit unit);
}
