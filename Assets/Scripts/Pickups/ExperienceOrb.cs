using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceOrb : Pickup
{
    public float experience = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void pickedUp(GameObject pickerUpper)
    {
        pickerUpper.GetComponent<ExperienceHaver>().gainExperience(experience);
        Destroy(this.gameObject);
    }
}
