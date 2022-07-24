using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sprite))]
public abstract class Entity : MonoBehaviour, ILocator, ISelectable
{
    [SerializeField] private Information information;
    private int health = 10;
    private Vector2Int size = Vector2Int.one;
    private Vector2Int position = Vector2Int.zero;
    private IPublisher<int> healthPublisher = new Publisher<int>();

    public IPublisher<int> HealthPublisher { get { return healthPublisher; } }
    public Information Information { get { return information; } }
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            healthPublisher.PublishData(health);

            if (health <= 0)
            {
                Die();
            }
        }
    }

    /// <summary>
    /// Assigning position updates the GridMap
    /// </summary>
    public Vector2Int Position
    {
        get { return position; }
        set
        {
            position = value;
            transform.position = new Vector3((Size.x / 2f) - 0.5f + position.x, (Size.y / 2f) - 0.5f + position.y, 0);

            GameManager.Instance.GridMap.RefreshTileMap();
        }
    }
    public Vector2Int Size
    {
        get { return size; }
    }


    protected virtual void OnEnable()
    {
        health = information.maxHealth;
        size = information.size;
    }

    protected virtual void Update() { }

    public virtual void Select()
    {
        UIController.Instance.InformationPanel.Init(this);
    }

    /// <summary>
    /// This method causes the DESTROY object "DIRECTLY"
    /// </summary>
    public virtual void Die()
    {
        if (GameManager.Instance.GridMap.Selection == (this as ISelectable)) // Deselect the object
        {
            GameManager.Instance.GridMap.Selection = null;
            UIController.Instance.InformationPanel.Init(null);
        }

        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.GridMap.RefreshTileMap(); // Update GridMap on destroy
    }
}
