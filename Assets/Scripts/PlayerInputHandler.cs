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

    private InputAction buttonAction1;



    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        player = Instantiate(playerPrefab).GetComponent<Player>();
        player.SetPIHandler(this);
        playerInput.actionEvents.ElementAt(0).AddListener(playerInput.GetComponent<PlayerInputHandler>().OnMove);


        buttonAction1 = new InputAction();
        buttonAction1 = playerInput.actions.ElementAt(1);
        buttonAction1.Enable();

    }

    // Update is called once per frame
    void Update()
    {
        if (buttonAction1.triggered)
        {
            OnUse();
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
                    player.Drinking();
                    player.IncrementIndexInSequence();
                    player.getNearestLocation().transform.GetChild(0).GetComponent<Shot>().SelfDestruct();
                }
            }

            else if (player.getNearestLocation().CompareTag("Ardoise")){
                Debug.Log("Ardoise");
            }
        }
    }


    public void reactivateInputs()
    {
        playerInput.ActivateInput();
    }
}
