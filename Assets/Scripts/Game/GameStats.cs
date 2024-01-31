using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStats : MonoBehaviour
{
    public TMP_Text goldText;
    public TMP_Text strengthText;
    public TMP_Text agilityText;
    public TMP_Text intText;
    public TMP_Text gameTimeText;
    public TMP_Text backpackText;

    private float gameTime;
    private float gameTimeUpdaterDelay;

    public static GameStats Instance;

    // Start is called before the first frame update
    void Start()
    {
        modifyPlayerGold(Player.Instance.subscribeToStat(Unit.StatTypes.Gold, modifyPlayerGold));
        modifyPlayerStrength(Player.Instance.unit.subscribeToStat(Unit.StatTypes.Strength, modifyPlayerStrength));
        modifyPlayerAgility(Player.Instance.unit.subscribeToStat(Unit.StatTypes.Agility, modifyPlayerAgility));
        modifyPlayerIntelligence(Player.Instance.unit.subscribeToStat(Unit.StatTypes.Intelligence, modifyPlayerIntelligence));

        modifyPlayerBackpack(Player.Instance.unit.subscribeToStat(Unit.StatTypes.BackpackCurrentCapacity, modifyPlayerBackpack));
        modifyPlayerBackpack(Player.Instance.unit.subscribeToStat(Unit.StatTypes.MaxBackpackCapacity, modifyPlayerBackpack));
    }

    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        gameTimeUpdaterDelay += Time.deltaTime;

        if(gameTimeUpdaterDelay > 1)
        {
            gameTimeUpdaterDelay = 0;

            int totalSeconds = Mathf.FloorToInt(gameTime);  // Drop the decimal points
            gameTimeText.text = string.Format("{0:00}:{1:00}", totalSeconds / 60, totalSeconds % 60);
        }
    }

    private void modifyPlayerGold(float gold)
    {
        goldText.text = gold + "";
    }

    private void modifyPlayerStrength(float strength)
    {
        strengthText.text = strength + "";
    }

    private void modifyPlayerAgility(float agility)
    {
        agilityText.text = agility + "";
    }

    private void modifyPlayerIntelligence(float intelligence)
    {
        intText.text = intelligence + "";
    }

    private void modifyPlayerBackpack(float dontMatta)
    {
        backpackText.text = Player.Instance.unit.backpackCurrentCapcity + " / " + Player.Instance.unit.maxBackpackCapacity;
    }
}
