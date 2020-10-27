using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputUI : MonoBehaviour
{

    private InputAction buttonAction2;
    PlayerInput playerInput;
    public PlayerControls controls;
    public GameObject CanvasManager;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        
        buttonAction2 = new InputAction();
        buttonAction2 = playerInput.actions.ElementAt(3);
        buttonAction2.Enable();
/*
        for(int i =0; i< playerInput.actions.Count(); i++)
        {
            Debug.Log(playerInput.actions.ElementAt(i).name);
        }*/
    }

    // Update is called once per frame
    void Update()
    {

        if (buttonAction2.triggered)
        {
            OnMenuClick();
        }
    }


    public void OnMenuClick()
    {
        CanvasManager.GetComponent<CanvasManager>().OnMainMenuClick();
    }
}
