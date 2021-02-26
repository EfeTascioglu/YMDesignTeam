using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Animator PlayerAnim;
    public GameObject player;
    public Transform head;
    public float walk_speed;
    public float jump_speed;
    public float XRot;
    public float YRot;
    private float YRotBody;
    public bool SnapToHead;
    public List<float> max_min_viewX;
    public List<float> max_min_viewY;
    public List<float> d_max_min_viewY;

    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        PlayerAnim = player.GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        d_max_min_viewY[0] = max_min_viewY[0];
        d_max_min_viewY[1] = max_min_viewY[1];
        SnapToHead = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 x_z_forward = new Vector3(head.transform.forward.x, 0, head.transform.forward.z);
        Vector3 x_z_right = new Vector3(head.transform.right.x, 0, head.transform.right.z);
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = x_z_forward * walk_speed;

            PlayerAnim.SetTrigger("Walk");
            PlayerAnim.SetBool("Stop", false);
            SnapToHead = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = x_z_forward * -walk_speed;
            PlayerAnim.SetTrigger("Walk");
            PlayerAnim.SetBool("Stop", false);
            SnapToHead = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = x_z_right * walk_speed * -1;
            PlayerAnim.SetTrigger("Walk");
            PlayerAnim.SetBool("Stop", false);
            SnapToHead = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = x_z_right * walk_speed;
            PlayerAnim.SetTrigger("Walk");
            PlayerAnim.SetBool("Stop", false);
            SnapToHead = true;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            PlayerAnim.SetBool("Stop", false);
            PlayerAnim.SetTrigger("Crouch");
            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(rb.velocity.x, jump_speed, rb.velocity.z);
            
        }

        if (Input.anyKey == false)
        {
            PlayerAnim.SetBool("Stop", true);
        }

        if (XRot - Input.GetAxis("Mouse Y") * 4 < max_min_viewX[0] && XRot - Input.GetAxis("Mouse Y") * 4 > max_min_viewX[1])
        {
            XRot -= Input.GetAxis("Mouse Y") * 4;
        }

        max_min_viewY[0] = d_max_min_viewY[0] + player.transform.localEulerAngles.y;
        max_min_viewY[1] =  d_max_min_viewY[1] + player.transform.localEulerAngles.y;
        if (YRot < 0)
        {
            YRot = 360 + YRot;
        }
        if (YRot + Input.GetAxis("Mouse X") * 4 < max_min_viewY[0] && YRot + Input.GetAxis("Mouse X") * 4 > max_min_viewY[1])
        {
            YRot += Input.GetAxis("Mouse X") * 4;
        }
        else
        {
            YRot += Input.GetAxis("Mouse X") * 4;
            YRotBody += Input.GetAxis("Mouse X") * 4;
        }

        head.rotation = Quaternion.Euler(new Vector3(XRot, YRot, head.rotation.z));
        if(SnapToHead == false)
        {
            player.transform.rotation = Quaternion.Euler(new Vector3(player.transform.rotation.x, YRotBody, player.transform.rotation.z));
        }
        else
        {
            player.transform.rotation = Quaternion.Euler(new Vector3(player.transform.rotation.x, YRot, player.transform.rotation.z));
        }
        SnapToHead = false;
    }

}
