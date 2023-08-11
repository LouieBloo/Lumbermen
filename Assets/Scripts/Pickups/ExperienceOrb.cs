using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceOrb : Pickup
{
    public int experience = 10;

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
