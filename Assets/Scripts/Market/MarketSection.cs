using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using static Market;

public class MarketSection : MonoBehaviour
{
    [SerializeField]
    public GameObject buttonPrefab;

    [SerializeField]
    public Transform buttonSpawnLocation;

    [SerializeField]
    public TextMeshProUGUI headerText;

    private MarketSectionUI marketSection;

    // Start is called before the first frame update
    void Start()
    {
        foreach (MarketItem item in marketSection.items)
        {
            GameObject itemButton = Instantiate(buttonPrefab, buttonSpawnLocation);
            itemButton.GetComponent<MenuButton>().setup(item, buttonClicked);
        }
    }


    public void setup(MarketSectionUI marketSection)
    {
        headerText.text = marketSection.header;
        this.marketSection = marketSection;
    }


    void buttonClicked(MarketItem item)
    {
        item.buy(Player.Instance);
    }
}
