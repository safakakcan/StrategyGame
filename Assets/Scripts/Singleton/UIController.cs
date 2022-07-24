using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Class for generic UI jobs
/// </summary>
public class UIController : Singleton<UIController>
{
    [SerializeField] private ScrollRect productionScrollRect;
    [SerializeField] private InformationPanel informationPanel;

    [Header("Prefabs")]
    [SerializeField] private ProductionEntry productionEntry;
    public ProductionEntry ProductionEntry { get { return productionEntry; } }

    public InformationPanel InformationPanel { get { return informationPanel; } }

    // Start is called before the first frame update
    void Start()
    {
        RefreshProductionMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Refresh all products in Production Menu (InfiniteScroll)
    /// </summary>
    public void RefreshProductionMenu()
    {
        ClearScrollRect(productionScrollRect);

        foreach (var prefab in GameManager.Instance.BuildingFactory.Prefabs)
        {
            var entry = Instantiate<GameObject>(productionEntry.gameObject, productionScrollRect.content);
            entry.GetComponent<ProductionEntry>().Init(prefab);
            entry.GetComponent<Button>().onClick.AddListener(() => entry.GetComponent<ISelectable>().Select());
        }

        foreach (var prefab in GameManager.Instance.UnitFactory.Prefabs)
        {
            var entry = Instantiate<GameObject>(productionEntry.gameObject, productionScrollRect.content);
            entry.GetComponent<ProductionEntry>().Init(prefab);
            entry.GetComponent<Button>().onClick.AddListener(() => entry.GetComponent<ISelectable>().Select());
        }

        productionScrollRect.GetComponent<UnityEngine.UI.Extensions.InfiniteScroll>().Init();
    }

    /// <summary>
    /// Clear a ScrollRect content
    /// </summary>
    /// <param name="scrollRect"></param>
    public void ClearScrollRect(ScrollRect scrollRect)
    {
        int lenght = scrollRect.content.childCount;

        for (int i = 0; i < lenght; i++)
        {
            Destroy(scrollRect.content.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// Check if mouse position is on specific layer
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    public bool IsPointerOverLayer(int layer)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        
        foreach (var result in raycastResults)
        {
            if (result.gameObject.layer == layer)
                return true;
        }

        return false;
    }
}
