using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainStop : MonoBehaviour
{
    public List<Crane> cranes = new List<Crane>();
    public List<LogDropoff> logDropoffs = new List<LogDropoff>();

    public static TrainStop Instance;

    private int upgradeLevel = 0;

    void Start()
    {
        Instance= this;
    }

    public void upgrateDropoffCount()
    {
        Debug.Log("CALLED");
        upgradeLevel++;
        cranes[upgradeLevel].gameObject.SetActive(true);
        logDropoffs[upgradeLevel].gameObject.SetActive(true);
    }

    public void trainArrived()
    {
        foreach(Crane crane in cranes)
        {
            crane.trainReady();
        }
    }

    public void trainLeftStation()
    {
        foreach (Crane crane in cranes)
        {
            crane.trainNotReady();
        }
    }



}
