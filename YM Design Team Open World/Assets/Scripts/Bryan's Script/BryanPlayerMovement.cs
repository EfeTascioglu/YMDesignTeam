using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BryanPlayerMovement : MonoBehaviour
{
    public CharacterController cController;


    public float pSpeed;
    public float jForce;
    public float gScale;

    private Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector3(Input.GetAxis("Horizontal") * pSpeed, moveDir.y, Input.GetAxis("Vertical") * pSpeed);

        if (cController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                moveDir.y = jForce;
            }
        }

        moveDir.y = moveDir.y + Physics.gravity.y * gScale * 1f/55f;
        cController.Move(moveDir * Time.deltaTime);
    }
}
