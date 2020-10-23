using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    private Transform[] shotLocations;
    public GameObject[] ShotsOnTheTable;


    // Start is called before the first frame update
    void Start()
    {
        Transform[] shotLocations = new Transform[6];

        for (int i = 0; i < transform.childCount; i++)
        {
            shotLocations[i] = transform.GetChild(i);
            ShotsOnTheTable[i].transform.SetParent(shotLocations[i]);
            ShotsOnTheTable[i].transform.localPosition = new Vector3(0,0,-0.3f);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
