using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // list of players spawned in the scene
    private GameObject[] players;
    // list of locations in the scene
    private GameObject[] locations;
    // number of shots in one player sequence
    private int sequences_length = 6;
    // number of shots added arbitrary in the scene
    [SerializeField]
    private int nb_additional_shots = 6;
    // list of shot objects spawned in the scene
    [SerializeField]
    private List<GameObject> _shots = new List<GameObject>();

    private List<Transform> spawners = new List<Transform>();

    private float timer = 3f;
    private float time = 0f;

    // all the shots in the scene
    private Dictionary<string, int> shots = new Dictionary<string, int>
    {
        {"El Verdito", 0 },
        {"El Naranjo", 0 },
        {"La Rosita", 0 },
        {"La Negrita", 0 },
        {"El Bermillon", 0 },
        {"El Grisito", 0 }
    };

    void Start()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time >= timer)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            locations = GameObject.FindGameObjectsWithTag("Location");
            generateSequences();
            generateAdditionalShots();
            createShots();
            locateShots();
            time = -99999999f;
        }  
    } 

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

    // creating and spawning all the shots needed 
    private void createShots()
    {
        foreach(KeyValuePair<string, int> kvp in shots)
        {
            for (int i = 0; i < kvp.Value; ++i)
            {
                GameObject shot = Instantiate(Resources.Load("Shot")) as GameObject;
                shot.GetComponent<Shot>().SetLabel(kvp.Key);
                shot.GetComponent<Shot>().ApplyColor();
                _shots.Add(shot); 
            }
        }
    }

    // locate all shots
    private void locateShots()
    {
        List<GameObject> _locationsL = new List<GameObject>(locations);
        foreach (GameObject s in _shots)
        {
            int index = Random.Range(0, _locationsL.Count);
            s.transform.parent = _locationsL[index].transform;
            s.transform.localPosition = new Vector3(0, 0, -0.3f);
            _locationsL.RemoveAt(index);
        }
        sequenceInArdoise();
    }

    // colorize Player sequence in ardoize
    private void sequenceInArdoise()
    {
        foreach (GameObject p in players) p.GetComponent<Player>().collorizeSequenceInArdoise();
    }
}
