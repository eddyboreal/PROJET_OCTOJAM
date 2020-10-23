using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<string> Shot_Sequence = new List<string>();

    [SerializeField] private Transform feets;
    [SerializeField] private float speed = 10.0f;
    private Rigidbody2D rb2d;
    private Vector2 moveVelocity;
    private GameObject nearestLocation;
    

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

    void OnTriggerStay2D(Collider2D collider)
    {
        if(nearestLocation == null || Vector3.Distance(collider.gameObject.transform.position,feets.position) <= Vector3.Distance(nearestLocation.transform.position, feets.position))
        {
            nearestLocation = collider.gameObject;
        }
    }
}