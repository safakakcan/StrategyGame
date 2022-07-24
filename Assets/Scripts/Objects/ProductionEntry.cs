using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionEntry : MonoBehaviour, ISelectable
{
    [SerializeField]
    private ISelectable prefab;
    [SerializeField]
    private UnityEngine.UI.Image image;

    public Information Information { get { return prefab.Information; } }

    /// <summary>
    /// Initialize this production
    /// </summary>
    /// <param name="prefab"></param>
    public void Init(Entity prefab)
    {
        this.prefab = prefab;
        image.sprite = prefab.Information.sprite;
    }

    /// <summary>
    /// Send this Production to Placeholder
    /// </summary>
    public void Select()
    {
        GameManager.Instance.GridMap.Placeholder.Init(prefab as ISelectable);
    }
}
