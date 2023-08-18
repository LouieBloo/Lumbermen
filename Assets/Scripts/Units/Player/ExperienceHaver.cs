using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceHaver : MonoBehaviour
{
    public int totalExperience = 0;
    public int currentLevel = 0;
    public AudioSource experienceGainedAudioSource;
    public AudioSource levelUpAudioSource;


    public ExperienceLevel[] levels;

    [System.Serializable]
    public class ExperienceLevel
    {
        public int experienceNeeded = 50;
        public int strengthGain = 1;
        public int agilityGain = 1;
        public int intelligenceGain = 1;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void gainExperience(int experience)
    {
        totalExperience += experience;

        if(experienceGainedAudioSource != null )
        {
            //experienceGainedAudioSource.Play();
        }

        checkLevel();
    }

    void checkLevel()
    {
        if(currentLevel + 1 < levels.Length)
        {
            if (levels[currentLevel + 1].experienceNeeded <= totalExperience)
            {
                levelUp();
            }
        }
    }

    void levelUp()
    {
        currentLevel++;
        levelUpAudioSource.Play();
        LevelUpHandler.Instance.leveledUp(checkLevel, levels[currentLevel]);
    }
}
