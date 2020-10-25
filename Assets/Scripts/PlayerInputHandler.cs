using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private Player player;
    private PlayerInput playerInput;
    public GameObject playerPrefab;
    public GameObject CanvasPrefab;
    private GameObject myCanvas;
    public GameObject CanvasManagerMainScene;

    private InputAction buttonAction1;
    private InputAction buttonAction2;

    //Sounds
    public AudioClip PlayerJoin;
    public AudioClip PlayerReady;
    public AudioClip PlayerDrink;

    public GameObject SceneManager;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().clip = PlayerJoin;
        GetComponent<AudioSource>().Play();
        CanvasManagerMainScene = GameObject.FindGameObjectWithTag("CanvasManagerMainScene");
        CanvasManagerMainScene.GetComponent<CanvasManagerMainScene>().canvasPositions[CanvasManagerMainScene.GetComponent<CanvasManagerMainScene>().playersConnected].GetChild(0).gameObject.SetActive(false);
        myCanvas = Instantiate(CanvasPrefab, CanvasManagerMainScene.GetComponent<CanvasManagerMainScene>().canvasPositions[CanvasManagerMainScene.GetComponent<CanvasManagerMainScene>().playersConnected]);
        CanvasManagerMainScene.GetComponent<CanvasManagerMainScene>().playersConnected++;

        playerInput = GetComponent<PlayerInput>();
        player = Instantiate(playerPrefab).GetComponent<Player>();
        player.SetPIHandler(this);
        playerInput.actionEvents.ElementAt(0).AddListener(playerInput.GetComponent<PlayerInputHandler>().OnMove);


        buttonAction1 = new InputAction();
        buttonAction1 = playerInput.actions.ElementAt(1);
        buttonAction1.Enable();

        buttonAction2 = new InputAction();
        buttonAction2 = playerInput.actions.ElementAt(2);
        buttonAction2.Enable();

    }

    // Update is called once per frame
    void Update()
    {
        if (buttonAction1 != null && buttonAction1.triggered)
        {
            OnUse();
        }
        if (buttonAction2 != null && buttonAction2.triggered)
        {
            GetReady();
        }
        if (player.isImmobilized)
        {
            playerInput.DeactivateInput();
        } 
        else if (!player.isDrinking)
        {
            reactivateInputs();
        }

    }


    public void OnMove(CallbackContext context)
    {
         if (player.hasInvertedControls)
        {
            player.moveInput = -context.ReadValue<Vector2>();
        } else if (player != null)
        {
            player.moveInput = context.ReadValue<Vector2>();
        }
    }

    public void OnUse()
    {
        if (player != null && player.getNearestLocation() != null)
        {
            if (player.getNearestLocation().CompareTag("Location"))
            {
                if (player.Shot_Sequence[player.GetIndexInSequence()] == player.getNearestLocation().transform.GetChild(0).GetComponent<Shot>().label
                    && player.GetIndexInSequence() <= player.Shot_Sequence.Count)
                {
                    playerInput.DeactivateInput();
                    GetComponent<AudioSource>().loop = true;
                    GetComponent<AudioSource>().clip = PlayerDrink;
                    GetComponent<AudioSource>().Play();
                    player.Drinking();
                    player.IncrementIndexInSequence();
                    player.getNearestLocation().transform.GetChild(0).GetComponent<Shot>().SelfDestruct();
                }
            }

            else if (player.getNearestLocation().CompareTag("Ardoise")){
                player.ShowArdoise(true);
            }
            else if(player.GetIndexInSequence() == player.Shot_Sequence.Count && player.getNearestLocation().CompareTag("El Bermillon"))
            {
                player.hasWon = true;
                Debug.Log("A player has won");
                SceneManager.GetComponent<SceneManager>().endGame();
            }
        }
    }

    public void GetReady()
    {
        myCanvas.transform.GetChild(2).gameObject.SetActive(!myCanvas.transform.GetChild(2).gameObject.activeInHierarchy);
        if (myCanvas.transform.GetChild(2).gameObject.activeInHierarchy)
        {
            GetComponent<AudioSource>().clip = PlayerReady;
            GetComponent<AudioSource>().Play();
            CanvasManagerMainScene.GetComponent<CanvasManagerMainScene>().playersReady++;
        }
        else
        {
            CanvasManagerMainScene.GetComponent<CanvasManagerMainScene>().playersReady--;
        }
        CanvasManagerMainScene.GetComponent<CanvasManagerMainScene>().playerPressedStart();
    }


        public void reactivateInputs()
    {
        if(GetComponent<AudioSource>().clip == PlayerDrink)
        {
            GetComponent<AudioSource>().Stop();
        }
        playerInput.ActivateInput();
    }
}
