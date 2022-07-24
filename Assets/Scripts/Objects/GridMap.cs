using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector2Int gridSize = new Vector2Int(25, 25);
    [SerializeField] private ISelectable selection = null;

    [Header("References")]
    [SerializeField] private Placeholder placeholder;
    [SerializeField] private Transform selectionMark;

    [Header("Prefabs")]
    [SerializeField] private GameObject tilePrefab;

    private bool[,] tileMap;
    private PathFind.Grid grid;

    public ISelectable Selection
    {
        get { return selection; }
        set { selection = value; }
    }
    public bool[,] TileMap { get { return tileMap; } }
    public PathFind.Grid Grid { get { return grid; } }
    public Placeholder Placeholder { get { return placeholder; } }

    // Start is called before the first frame update
    void Start()
    {
        tileMap = new bool[gridSize.x, gridSize.y];
        grid = new PathFind.Grid(gridSize.x, gridSize.y, tileMap);

        CreateTiles(gridSize.x, gridSize.y);
        Camera.main.transform.position = new Vector3(gridSize.x / 2, gridSize.y / 2, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UIController.Instance.IsPointerOverLayer(5)) // LEFT CLICK  <-------- (UI Layer Excluded)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            ISelectable target;
            if (hit && hit.collider.gameObject.TryGetComponent<ISelectable>(out target)) // Select an object
            {
                Selection = target;
                Selection.Select();
            }
            else // Deselection; Close the information panel
            {
                Selection = null;
                UIController.Instance.InformationPanel.Init(null);
            }
        }
        else if (Input.GetMouseButtonDown(1) && !UIController.Instance.IsPointerOverLayer(5)) // RIGHT CLICK  <-------- (UI Layer Excluded)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var point = ray.GetPoint(10);
            var pos = new Vector2Int(Mathf.RoundToInt(point.x), Mathf.RoundToInt(point.y));

            if (pos.x >= 0 && pos.x < gridSize.x && pos.y >= 0 && pos.y < gridSize.y) // Mouse Click in Grid Bounds
            {
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (Selection != null) // Has a selection
                {
                    if (Selection is IMovable)
                    {
                        (Selection as IMovable).Destination = pos; // Move object to destination point
                    }

                    if (Selection is IAttacker)
                    {
                        Entity target;
                        if (hit && hit.collider.gameObject.TryGetComponent<Entity>(out target))
                        {
                            (Selection as IAttacker).Target = target; // Set a target to selection
                        }
                        else
                        {
                            (Selection as IAttacker).Target = null;
                        }
                    }
                }
            }
        }

        // Show Selection Mark on selected object
        if (Selection != null && Selection is ILocator)
        {
            selectionMark.position = (Selection as MonoBehaviour).transform.position;
            selectionMark.GetComponent<SpriteRenderer>().size = (Selection as ILocator).Size;
            selectionMark.gameObject.SetActive(true);
        }
        else
        {
            selectionMark.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Create tiles by size
    /// </summary>
    /// <param name="sizeX"></param>
    /// <param name="sizeY"></param>
    private void CreateTiles(int sizeX, int sizeY)
    {
        GameObject parent = new GameObject("Tiles");

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                var tile = ObjectPooler.Instance.GetPooledObject("Tile");
                tile.transform.SetParent(parent.transform);
                tile.transform.position = new Vector3(x, y, 0);
                tile.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Calculate TileMap's walkable areas by all Locators
    /// </summary>
    public void RefreshTileMap()
    {
        var entities = FindObjectsOfType<MonoBehaviour>().OfType<ILocator>();
        var newTileMap = new bool[gridSize.x, gridSize.y];

        foreach (var entity in entities)
        {
            for (int x = 0; x < entity.Size.x; x++)
            {
                for (int y = 0; y < entity.Size.y; y++)
                {
                    newTileMap[entity.Position.x + x, entity.Position.y + y] = true;
                }
            }
        }

        tileMap = newTileMap;
        grid = new PathFind.Grid(gridSize.x, gridSize.y, tileMap);
    }

    /// <summary>
    /// Checks the area on TileMap. Returns True, if area is suitable.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public bool CanBePlaced(Vector2Int position, Vector2Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                var pos = position + new Vector2Int(x, y);

                if (pos.x < 0 || pos.y < 0 ||
                    pos.x >= gridSize.x || pos.y >= gridSize.y ||
                    tileMap[pos.x, pos.y])
                    return false;
            }
        }

        return true;
    }
}
