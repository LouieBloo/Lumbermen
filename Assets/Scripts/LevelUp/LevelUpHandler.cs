using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpHandler : MonoBehaviour
{
    public GameObject levelUpChooserUI;
    public static LevelUpHandler Instance { get; private set; }

    private Action levelUpFinishedCallback;
    private ExperienceHaver.ExperienceLevel currentLevelUp;

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

    public void leveledUp(Action callback, ExperienceHaver.ExperienceLevel experienceLevel)
    {
        levelUpFinishedCallback = callback;
        currentLevelUp = experienceLevel;
        Time.timeScale = 0f;
        levelUpChooserUI.SetActive(true);
    }

    public void close()
    {
        Time.timeScale = 1.0f;
        levelUpChooserUI.SetActive(false);
        levelUpFinishedCallback();
    }

    public void strengthChoosen()
    {
        Player.Instance.levelUp(Player.StatTypes.Strength, currentLevelUp.strengthGain);
        close();
    }

    public void agilityChoosen()
    {
        Player.Instance.levelUp(Player.StatTypes.Agility, currentLevelUp.agilityGain);
        close();
    }

    public void intelligenceChoosen()
    {
        Player.Instance.levelUp(Player.StatTypes.Intelligence, currentLevelUp.intelligenceGain);
        close();
    }
}
