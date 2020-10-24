using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    public List<string> Shot_Sequence = new List<string>(); //The name of the differents shots the player has to drink to win the game

    [SerializeField] private Transform feets; //Determines where are the feet of the player
    [SerializeField] private float speed = 10.0f; //Determines the player's speed
    private Rigidbody2D rb2d; //Player's rigidbody2D
    private Vector2 moveVelocity;
    private GameObject nearestLocation; //Store the nearest shot location

    public Image TimeBar;
    public GameObject Canvas;

    [SerializeField]
    private int index_in_sequence = 0;

    public Vector3 moveInput;

    private bool isDrinking = false;
    [SerializeField]
    private float drinkTimer = 1f;
    private float drinkTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //playerIndex = playerIndex++;
        rb2d = GetComponent<Rigidbody2D>();
    }

    public int GetPlayerIndex()
    {
        return 1; //playerIndex;
    }

    // Update is called once per frame
    void Update()
    {

        moveVelocity = moveInput.normalized * speed;

        if (isDrinking)
        {
            drinkTime += Time.deltaTime;
            TimeBar.fillAmount = (drinkTimer - drinkTime) / drinkTimer;
            if (drinkTime >= drinkTimer)
            {
                isDrinking = false;
                drinkTime = 0;
                Canvas.SetActive(false);
            }
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

    public GameObject getNearestLocation()
    {
        return nearestLocation;
    }

    public void Drinking()
    {
        Canvas.SetActive(true);
        TimeBar.color = nearestLocation.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().color;
        isDrinking = true;
    }

    public int GetIndexInSequence() { return index_in_sequence; }
    public void IncrementIndexInSequence() { ++index_in_sequence; }


}