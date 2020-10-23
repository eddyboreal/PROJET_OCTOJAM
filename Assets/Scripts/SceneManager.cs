using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private GameObject[] players;
    private int sequences_length;

    // all the shots in the scene
    private Dictionary<string, int> shots = new Dictionary<string, int>
    {
        {"type 1", 0 },
        {"type 2", 0 },
        {"type 3", 0 },
        {"type 4", 0 },
        {"type 5", 0 },
        {"type 6", 0 }
    };

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Players");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generateSequences()
    {
        int nb_players = players.Length;
        if(nb_players > 0)
        {

        }
    }
}
