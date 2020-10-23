using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<string> Shot_Sequence = new List<string>();
    [SerializeField] private float speed = 10.0f;
    private Rigidbody2D rb2d;
    private Vector2 moveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);
        transform.position = transform.position + moveInput * Time.deltaTime * speed;
    }

    void FixedUpdate()
    {
        //rb2d.MovePosition(rb2d.position + moveVelocity * Time.fixedDeltaTime);
    }
}