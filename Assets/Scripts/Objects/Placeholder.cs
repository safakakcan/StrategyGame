using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Placeholder : MonoBehaviour, ILocator
{
    [SerializeField] private ISelectable prefab;

    [SerializeField] private Vector2Int position = Vector2Int.zero;
    [SerializeField] private Vector2Int size = Vector2Int.one;

    public Vector2Int Position
    {
        get { return position; }
        set
        {
            position = value;
            transform.position = new Vector3((Size.x / 2f) - 0.5f + position.x, (Size.y / 2f) - 0.5f + position.y, 0);
        }
    }
    public Vector2Int Size { get { return size; } }

    // Update is called once per frame
    void Update()
    {
        if (prefab != null)
        {
            var pos = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(10);

            Position = new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)); // Calculate the grid position

            if (GameManager.Instance.GridMap.CanBePlaced(Position, Size))
            {
                GetComponent<SpriteRenderer>().color = Color.white;

                if (Input.GetMouseButtonDown(0)) // Left Click; Place the selected prefab
                {
                    Place(Position.x, Position.y);
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }

    /// <summary>
    /// Initialize placeholder with an object
    /// </summary>
    /// <param name="prefab"></param>
    public void Init(ISelectable prefab)
    {
        this.prefab = prefab;
        GetComponent<SpriteRenderer>().sprite = prefab.Information.sprite;
        size = prefab.Information.size;

        this.gameObject.SetActive(true);
    }


    /// <summary>
    /// Method to place the selected prefab
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void Place(int x, int y)
    {
        Entity newEntity = null;

        if (prefab is Building)
        {
            newEntity = GameManager.Instance.BuildingFactory.GetNewInstance((prefab as Object).name);
        }
        else if (prefab is Unit)
        {
            newEntity = GameManager.Instance.UnitFactory.GetNewInstance((prefab as Object).name);
            (newEntity as Unit).Destination = new Vector2Int(x, y);
        }

        if (newEntity != null)
        {
            newEntity.Position = new Vector2Int(x, y);
            this.gameObject.SetActive(false);
        }
    }
}
