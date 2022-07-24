using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanel : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Image image;
    [SerializeField] private Image hpBarFill;
    [SerializeField] private GameObject productionPanel;
    [SerializeField] private ScrollRect productionScrollRect;

    private Subscriber<int> healthSubscriber;
    private ISelectable selection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Initialize Information Panel
    /// </summary>
    /// <param name="selection"></param>
    public void Init(ISelectable selection)
    {
        if (selection == null || selection.Information == null)
        {
            this.gameObject.SetActive(false);
            return;
        }
        else
        {
            gameObject.SetActive(true);

            this.selection = selection;
            nameText.text = selection.Information.displayName;
            image.sprite = selection.Information.sprite;

            UIController.Instance.ClearScrollRect(productionScrollRect); // Clear production ScrollRect
            productionPanel.SetActive(false);

            if (selection is Entity)
            {
                hpBarFill.fillAmount = (float)(selection as Entity).Health / (float)selection.Information.maxHealth; // Set current health

                if (healthSubscriber != null && healthSubscriber.Publisher != null) // Unsubscribe from old Publisher
                    healthSubscriber.Publisher.DataPublisher -= SetHealth;

                healthSubscriber = new Subscriber<int>((selection as Entity).HealthPublisher); // Subscribe to new Publisher
                healthSubscriber.Publisher.DataPublisher += SetHealth;
            }
            else
            {
                hpBarFill.fillAmount = 1f;
            }
        }
    }

    /// <summary>
    /// Add production item to Information Panel
    /// </summary>
    /// <param name="entities"></param>
    public void AddToProduction(params Entity[] prefabs)
    {
        foreach (var entity in prefabs)
        {
            var entry = Instantiate<GameObject>(UIController.Instance.ProductionEntry.gameObject, productionScrollRect.content);
            entry.GetComponent<ProductionEntry>().Init(entity);

            Vector2Int spawnPosition = (selection as ILocator).Position +
                new Vector2Int((selection as IUnitSpawner).SpawnPointOffset.x, (selection as IUnitSpawner).SpawnPointOffset.y);

            entry.GetComponent<Button>().onClick
                .AddListener(() => (selection as IUnitSpawner).SpawnUnit(entity as Unit));
        }

        productionPanel.SetActive(productionScrollRect.content.childCount > 0);
    }

    void SetHealth(object sender, MessageArgument<int> e)
    {
        hpBarFill.fillAmount = (float)e.Message / (float)selection.Information.maxHealth;
    }

    private void OnDisable()
    {
        if (healthSubscriber != null && healthSubscriber.Publisher != null) // Unsubscribe from old Publisher
            healthSubscriber.Publisher.DataPublisher -= SetHealth;
    }
}
