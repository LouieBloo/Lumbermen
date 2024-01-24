using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AllUnitPrefabs;

public class Market : MonoBehaviour
{
    [Serializable]
    public class MarketSectionUI
    {
        [SerializeField]
        public string header;

        [SerializeField]
        public MarketItem[] items;
    }

    [SerializeField]
    public Transform sectionSpawnPoint;

    [SerializeField]
    public GameObject sectionPrefab;

    [SerializeField]
    public MarketSectionUI[] sections;

    void Start()
    {
        foreach(MarketSectionUI marketSection in sections)
        {
            GameObject section = Instantiate(sectionPrefab, sectionSpawnPoint);
            section.GetComponent<MarketSection>().setup(marketSection);
        }
    }
}
