using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsSpwn : MonoBehaviour
{
    public GameObject obsSpawner;
    public Transform spwnPosition;
    // Start is called before the first frame update
    void SpawnObstacle()
    {
        Instantiate(obsSpawner, spwnPosition);
    }
}
