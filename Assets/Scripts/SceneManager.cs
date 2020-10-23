using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private GameObject[] players = GameObject.FindGameObjectsWithTag("Players");

    // all the shots in the scene
    private Dictionary<string, int> shots = new Dictionary<string, int>
    {
        {"shot 1", 0 },
        {"shot 2", 0 },
        {"shot 3", 0 },
        {"shot 4", 0 },
        {"shot 5", 0 },
        {"shot 6", 0 }
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generateSequences()
    {

    }
}
