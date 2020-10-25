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
    private Animator animator;
    private Vector2 moveVelocity;
    [SerializeField]
    private GameObject nearestLocation; //Store the nearest shot location
    private PlayerInputHandler pIHandler;

    public Image TimeBar;
    public GameObject Canvas;

    // self ardoise
    public Canvas ArdoiseCanvas;

    [SerializeField]
    private int index_in_sequence = 0;

    public Vector3 moveInput;

    public bool isDrinking = false;
    [SerializeField]
    private float drinkTimer = 1f;
    private float drinkTime = 0f;

    [SerializeField] private int pimentometer = 0;
    private float immobilizedTimer;
    public bool isImmobilized = false;
    public bool hasInvertedControls = false;
    private float immobilizedMaxTime = 6f;

    // Start is called before the first frame update
    void Start()
    {
        //playerIndex = playerIndex++;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public int GetPlayerIndex()
    {
        return 1; //playerIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (isImmobilized)
        {
            immobilizedTimer += Time.deltaTime;
        }
        if (immobilizedTimer >= immobilizedMaxTime)
        {
            immobilizedTimer = 0;
            isImmobilized = false;
            pimentometer = 3;
            hasInvertedControls = false;
            UpdatePimentometerEffects();
            this.gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        moveVelocity = moveInput.normalized * speed;

        if(moveVelocity.magnitude != 0 && !isDrinking)
        {
            animator.SetBool("walking", true);
        }
        else if(!isDrinking)
        {
            animator.SetBool("walking", false);
        }

        if (isDrinking)
        {
            //GetComponent<AudioSource>().Play(A)
            drinkTime += Time.deltaTime;
            TimeBar.fillAmount = (drinkTimer - drinkTime) / drinkTimer;
            animator.SetBool("drinking", true);
            if (drinkTime >= drinkTimer)
            {
                pIHandler.reactivateInputs();
                animator.SetBool("drinking", false);
                isDrinking = false;
                drinkTime = 0;
                Canvas.SetActive(false);
                pimentometer += Random.Range(1, 3);
                UpdatePimentometerEffects();
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
        if (nearestLocation == null || Vector3.Distance(collider.gameObject.transform.position, feets.position) <= Vector3.Distance(nearestLocation.transform.position, feets.position))
        {
            if(collider.gameObject) nearestLocation = collider.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        nearestLocation = null;
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

    public void SetPIHandler(PlayerInputHandler pih)
    {
        pIHandler = pih;
    }

    private void collorizeSequenceInArdoise()
    {
        //ArdoiseCanvas.transform.
    }

    private void UpdatePimentometerEffects()
    {
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1-(pimentometer * 0.16f), 1-(pimentometer * 0.16f), 1);
        speed = 10 - (pimentometer * 1); 
        if (pimentometer >= 6)
        {
            isImmobilized = true;
            this.gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        } else if (pimentometer >= 4)
        {
            hasInvertedControls = true;
        }
    }

  
}