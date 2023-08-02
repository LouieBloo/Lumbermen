using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceHaver : MonoBehaviour
{
    public float totalExperience = 0f;
    public AudioSource experienceGainedAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void gainExperience(float experience)
    {
        totalExperience += experience;

        if(experienceGainedAudioSource != null )
        {
            experienceGainedAudioSource.Play();
        }
    }
}
