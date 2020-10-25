using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManagerMainScene : MonoBehaviour
{
    public List<Transform> canvasPositions =  new List<Transform>();
    public int playersConnected = 0;
    public int playersReady = 0;

    public SceneManager SceneManager;
    private bool gameLauched = false;
    public GameObject Camera;
    public Canvas mainCanva;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playerPressedStart()
    {
        if(playersReady == playersConnected && !gameLauched)
        {
            gameLauched = true;
            SceneManager.LauchGame();
            mainCanva.worldCamera = null;
            mainCanva.gameObject.SetActive(false);
            Camera.GetComponent<Transform>().position = new Vector3(0,0, -50);
        }
    }

}
