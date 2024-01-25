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
    private GameObject marketUI;

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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == Player.Instance.gameObject)
        {
            open();
        }
    }

    public void open()
    {
        Time.timeScale = 0f;
        marketUI.SetActive(true);
    }

    public void close()
    {
        Time.timeScale = 1f;
        marketUI.SetActive(false);
    }
}
