using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Barrack : Building, IUnitSpawner
{
    [SerializeField] private Vector2Int spawnPointOffset = Vector2Int.down;
    public Vector2Int SpawnPointOffset { get { return spawnPointOffset; } set { spawnPointOffset = value; } }

    public override void Select()
    {
        base.Select();

        if (Information != null && Information is BuildingInfo)
            UIController.Instance.InformationPanel.AddToProduction((Information as BuildingInfo).units); // Barrack can spawn their own units and list them.
    }

    /// <summary>
    /// Spawn an unit on barrack's spawn-point.
    /// </summary>
    /// <param name="unit"></param>
    public void SpawnUnit(Unit unit)
    {
        var spawnPoint = Position + SpawnPointOffset;

        if (GameManager.Instance.GridMap.CanBePlaced(spawnPoint, unit.Size))
        {
            GameManager.Instance.GridMap.Placeholder.Init(unit);
            GameManager.Instance.GridMap.Placeholder.Place(spawnPoint.x, spawnPoint.y);
        }
        else
        {
            Debug.LogWarning("The unit could not be spawned. Spawn point is not suitable.");
        }
    }
}
