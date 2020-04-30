using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsSpwn : MonoBehaviour
{
    public GameObject obsSpawner;
    //public Transform spwnPosition;
    
    void Start() 
    {
        SpawnObstacle();
    }
    void SpawnObstacle()
    {
        Instantiate(obsSpawner, this.transform);
    }
}
