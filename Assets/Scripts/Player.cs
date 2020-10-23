using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<string> Shot_Sequence = new List<string>(); //The name of the differents shots the player has to drink to win the game

    [SerializeField] private Transform feets; //Determines where are the feet of the player
    [SerializeField] private float speed = 10.0f; //Determines the player's speed
    private Rigidbody2D rb2d; //Player's rigidbody2D
    private Vector2 moveVelocity;
    private GameObject nearestLocation; //Store the nearest shot location
    

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(nearestLocation.name);
        }

    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + moveVelocity * Time.fixedDeltaTime);
    }

    //Detects nearest shot
    void OnTriggerStay2D(Collider2D collider)
    {
        if(nearestLocation == null || Vector3.Distance(collider.gameObject.transform.position,feets.position) <= Vector3.Distance(nearestLocation.transform.position, feets.position))
        {
            nearestLocation = collider.gameObject;
        }
    }
}