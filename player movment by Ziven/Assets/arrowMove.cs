using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowMove : MonoBehaviour
{

    
	public float moveSpeed;
    private float jumpSpeed = 5;
    private Rigidbody rigidBody;
    private bool onGround = true;
    private const int MAX_JUMP = 2;
    private int currentJump = 0;



    // Start is called before the first frame update
    void Start()
    {
    	moveSpeed = 5f;
        rigidBody = GetComponent<Rigidbody>();



    }

    // Update is called once per frame
    void Update(){

    	//moves it left and right with arrows
      	transform.Translate(moveSpeed*Input.GetAxis("Horizontal")*Time.deltaTime,0f,moveSpeed*Input.GetAxis("Vertical")*Time.deltaTime); 

        if(Input.GetKeyDown("space") && (onGround || MAX_JUMP > currentJump))
        {
            rigidBody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            onGround = false;
            currentJump++;

        }

    }


    

    void OnCollisionEnter(Collision collision)
    {
        onGround = true;
        currentJump = 0;

    }



    
}
