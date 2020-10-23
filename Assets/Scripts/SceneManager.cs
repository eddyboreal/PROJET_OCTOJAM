using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private GameObject[] players;
    private int sequences_length = 6;
    [SerializeField]
    private int nb_additional_shots = 6;

    // all the shots in the scene
    private Dictionary<string, int> shots = new Dictionary<string, int>
    {
        {"El Verdito", 0 },
        {"El Rojito", 0 },
        {"La Rosita", 0 },
        {"La Negrita", 0 },
        {"El Bermillon", 0 },
        {"El Blancito", 0 }
    };

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        generateSequences();
        generateAdditionalShots();
    }

    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (KeyValuePair<string, int> kvp in shots) Debug.Log(kvp.Key + "/" + kvp.Value);
        }    
    } */

    private void generateSequences()
    {
        int nb_players = players.Length;
        
        if(nb_players > 0)
        {
            foreach(GameObject p in players)
            {
                for(int i = 0; i < sequences_length; ++i)
                {
                    int rd_shot = Random.Range(0, shots.Count);
                    string shot_key = shots.ElementAt(rd_shot).Key;
                    p.GetComponent<Player>().Shot_Sequence.Add(shot_key);
                    if (shots.ContainsKey(shot_key)) shots[shot_key] += 1;
                }
            }
        }
    }

    private void generateAdditionalShots()
    {
        for(int i = 0; i < nb_additional_shots; ++i)
        {
            int rd_shot = Random.Range(0, shots.Count);
            string shot_key = shots.ElementAt(rd_shot).Key;
            if (shots.ContainsKey(shot_key)) shots[shot_key] += 1;
        }
    }
}
