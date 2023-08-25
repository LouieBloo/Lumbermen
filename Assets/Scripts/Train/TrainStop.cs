using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainStop : MonoBehaviour
{
    public List<Crane> cranes = new List<Crane>();
    public List<LogDropoff> logDropoffs = new List<LogDropoff>();

    void Start()
    {
        
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
