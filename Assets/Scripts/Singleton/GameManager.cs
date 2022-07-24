using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Game Manager Class
/// </summary>
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GridMap gridMap;

    [Header("References")]
    [SerializeField] private BuildingFactory buildingFactory;
    [SerializeField] private UnitFactory unitFactory;
    
    public GridMap GridMap { get { return gridMap; } }
    public BuildingFactory BuildingFactory { get { return buildingFactory; } }
    public UnitFactory UnitFactory { get { return unitFactory; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
