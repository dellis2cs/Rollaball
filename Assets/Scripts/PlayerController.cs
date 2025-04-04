using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float jumpForce = 5f;
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private int count;
    private bool isGrounded = true;
    private int numOfJumps = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }


    
    void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX,0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")){
            isGrounded = true;
            numOfJumps = 2;
        }
    }
    void OnJump(InputValue jumpValue){
        if(jumpValue.isPressed && (isGrounded || numOfJumps > 0)){
            rb.AddForce(new Vector3(0,jumpForce,0), ForceMode.Impulse);
            isGrounded = false;
            numOfJumps -= 1;
        }
    }

    void OnTriggerEnter (Collider other) 
   {
       if (other.gameObject.CompareTag("PickUp")) 
       {
           other.gameObject.SetActive(false);
           count = count + 1;
           SetCountText();
       }
   }
   void SetCountText(){
    countText.text =  "Count: " + count.ToString();
    if (count >= 12)
       {
           winTextObject.SetActive(true);
       }
   }
}
