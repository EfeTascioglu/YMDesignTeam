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
    private float XRot;
    private float YRot;
    public List<int> max_min_viewX;
    public List<int> max_min_viewY;
    public List<int> turn_viewY;

    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        PlayerAnim = player.GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            BodyTurnAsHead();
            rb.velocity = rb.transform.forward * walk_speed;
            PlayerAnim.SetTrigger("Walk");
            PlayerAnim.SetBool("Stop", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            BodyTurnAsHead();
            rb.velocity = rb.transform.forward * -walk_speed;
            PlayerAnim.SetTrigger("Walk");
            PlayerAnim.SetBool("Stop", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            BodyTurnAsHead();
            rb.velocity = rb.transform.right * walk_speed * -1;
            PlayerAnim.SetTrigger("Walk");
            PlayerAnim.SetBool("Stop", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            BodyTurnAsHead();
            rb.velocity = rb.transform.right * walk_speed;
            PlayerAnim.SetTrigger("Walk");
            PlayerAnim.SetBool("Stop", false);
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
            
        }

        if (YRot + Input.GetAxis("Mouse X") * 4 < max_min_viewY[0] && YRot + Input.GetAxis("Mouse X") * 4 > max_min_viewY[1])
        {
            
        }
        XRot -= Input.GetAxis("Mouse Y") * 4;
        YRot += Input.GetAxis("Mouse X") * 4;
        head.rotation = Quaternion.Euler(new Vector3(XRot, YRot, head.rotation.z));

    }

    void BodyTurnAsHead()
    {
        player.transform.rotation = Quaternion.Euler(new Vector3(player.transform.rotation.x, head.rotation.eulerAngles.y, player.transform.rotation.z));
    }
}
