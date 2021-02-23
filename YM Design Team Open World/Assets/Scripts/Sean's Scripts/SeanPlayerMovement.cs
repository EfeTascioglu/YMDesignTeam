using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeanPlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public Rigidbody rb;
    public Transform player_look;
    public LayerMask environment;
    public Transform player_camera;
    private float XRot;
    private float YRot;
    public List<int> max_min_viewX;
    public List<int> max_min_viewY;
    private Transform tf;
    // Start is called before the first frame update
    void Start()
    {
        tf = rb.GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = new Ray(rb.GetComponentInParent<Transform>().position, Vector3.down);
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, 5, environment))
        {
            tf.rotation = Quaternion.Lerp(tf.rotation, Quaternion.FromToRotation(Vector3.up, rayHit.normal), Time.deltaTime * 5);
        }

        if (XRot - Input.GetAxis("Mouse Y") * 4 < max_min_viewX[0] && XRot - Input.GetAxis("Mouse Y") * 4 > max_min_viewX[1])
        {
            XRot -= Input.GetAxis("Mouse Y") * 4;
        }
        
        if (YRot + Input.GetAxis("Mouse X") * 4 < max_min_viewY[0] && YRot + Input.GetAxis("Mouse X") * 4 > max_min_viewY[1])
        {
            YRot += Input.GetAxis("Mouse X") * 4;
        }

        player_look.rotation = Quaternion.Euler(new Vector3(XRot, YRot, tf.rotation.z));
       
     
        player_camera.rotation = Quaternion.Euler(XRot, player_camera.rotation.y, 0);
        

        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(player_look.forward.x * speed, rb.velocity.y, player_look.forward.z * speed);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(player_look.forward.x * -speed, rb.velocity.y, player_look.forward.z * -speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(player_look.right.x * speed, rb.velocity.y, player_look.right.z * speed);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(player_look.right.x * -speed, rb.velocity.y, player_look.right.z * -speed);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(ray, out rayHit, 1.01f, environment))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
        
        if (Input.anyKey == false)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
      
    }
}
