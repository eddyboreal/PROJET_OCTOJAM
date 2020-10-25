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
    private float showerTimer;
    public bool isInTheShower = false;
    private float showerEffectTime = 0.5f;

    public bool hasWon = false;
    // Start is called before the first frame update
    void Start()
    {
        //playerIndex = playerIndex++;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ArdoiseCanvas.GetComponent<Canvas>().enabled = false;
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

        if (isInTheShower && pimentometer > 0)
        {
            showerTimer += Time.deltaTime;
        }
        if (showerTimer >= showerEffectTime)
        {
            showerTimer = 0;
            pimentometer--;
            UpdatePimentometerEffects();
        }
        if (isInTheShower && pimentometer == 0)
        {
                isInTheShower = false;
                GameObject.Find("DoucheZone").GetComponent<SpriteRenderer>().sprite = GameObject.Find("DoucheZone").GetComponent<ShowerChangingAppearance>().showerNoWater;
            GameObject.Find("DoucheZone").GetComponent<Animator>().enabled = false;
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
                ArdoiseCanvas.transform.GetChild(index_in_sequence).GetComponent<Image>().gameObject.SetActive(false);
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
        if (collider.gameObject.tag == "Douche" && pimentometer > 0)
        {
            //collider.gameObject.GetComponent<AudioSource>().Play(collider.GetComponent<ShowerChangingAppearance>().ShowerSound);
            isInTheShower = true;
            collider.GetComponent<SpriteRenderer>().sprite = collider.GetComponent<ShowerChangingAppearance>().showerWater;
            collider.GetComponent<Animator>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        nearestLocation = null;
        if (collision.gameObject.tag == "Douche")
        {
            isInTheShower = false;
            collision.GetComponent<SpriteRenderer>().sprite = collision.GetComponent<ShowerChangingAppearance>().showerNoWater;
            collision.GetComponent<Animator>().enabled = false;
        }
        if(collision.gameObject.CompareTag("Ardoise")) ShowArdoise(false);
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

    public void collorizeSequenceInArdoise()
    {
        for(int i = 0; i < Shot_Sequence.Count; ++i)
        {
            switch (Shot_Sequence[i])
            {
                case "El Verdito":
                    ArdoiseCanvas.transform.GetChild(i+1).GetComponent<Image>().color = Color.green;
                    break;
                case "El Naranjo":
                    ArdoiseCanvas.transform.GetChild(i + 1).GetComponent<Image>().color = new Color(0.96f, 0.54f, 0f);
                    break;
                case "La Rosita":
                    ArdoiseCanvas.transform.GetChild(i + 1).GetComponent<Image>().color = new Color(0.96f, 0.22f, 0.95f);
                    break;
                case "La Negrita":
                    ArdoiseCanvas.transform.GetChild(i + 1).GetComponent<Image>().color = Color.black;
                    break;
                case "El Bermillon":
                    ArdoiseCanvas.transform.GetChild(i + 1).GetComponent<Image>().color = new Color(0.70f, 0.22f, 0.07f);
                    break;
                case "El Grisito":
                    ArdoiseCanvas.transform.GetChild(i + 1).GetComponent<Image>().color = Color.gray;
                    break;
                default:
                    Color c = Color.white;
                    break;
            }
        }
    }

    public void ShowArdoise(bool b)
    {
        ArdoiseCanvas.GetComponent<Canvas>().enabled = b;
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
        } else if (pimentometer < 4)
        {
            hasInvertedControls = false; 
        }
    }

  
}