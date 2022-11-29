using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] int jump;
    [SerializeField] float gravityDefault;
    [SerializeField] float gravityFall;

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private float hinput;
    private bool jinput;

    void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Checking for Horizontal Input (A, D)
        hinput = Input.GetAxisRaw("Horizontal");

        //Checking for Jump Input
        if(Input.GetButtonDown("Jump")){
            jinput = true;
        }

        //Variable Jump. If jumpbutton was released and you're still gaining momentum, stop gaining that momentum.
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0){
            rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    void FixedUpdate()
    {

        //Moves us left and right
        rb.position = new Vector3(rb.position.x + hinput * Time.fixedDeltaTime * speed, rb.position.y,0);

        //Jump
        if(jinput){
            rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            jinput = false;
        }

        //Fastfall, which makes us fall faster than the speed of our ascent to jump.
        //For more info check the "documentation" for fastFall.
        fastFall();

        //Flips us depending on what direction we moved last
        if(hinput > 0){
            sr.flipX = false;
        }
        else if(hinput < 0){
            sr.flipX = true;
        }
    }

    //Code used for fastfalling.
    //To put it simply, when you are jumping and going up you have a y velocity thats positive. 
    //When you are not jumping, you have a y velocity of 0, and want normal gravity.
    //When you are falling, you have a negative velocity.
    //This changes your gravity scale to depend on your velocity.
    private void fastFall(){
        if(rb.velocity.y >= 0){
            rb.gravityScale = gravityDefault;
        }
        else if(rb.velocity.y < 0){
            rb.gravityScale = gravityFall;
        }
    }


}
