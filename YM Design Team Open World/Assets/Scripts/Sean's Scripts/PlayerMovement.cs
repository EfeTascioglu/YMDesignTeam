using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Animator PlayerAnim;
    public GameObject player;
    public Transform GroundDetector;
    public Transform head;
    public LayerMask can_jump_on;
    public float walk_speed;
    private float crouch_speed;
    private float run_speed;
    public float jump_speed;
    private float XRot;
    private float YRot;
    private float YRotBody;
    private float RunActivationTime;
    private float EndActivationTime;
    private string RunActivationKey;
    private bool SnapToHead;
    public int MoveMode;
    public List<float> max_min_viewX;
    public List<float> max_min_viewY;
    public List<float> d_max_min_viewY;
    private float smoothlerp_start;
    private float current_body_smoothlerp_v;

    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        PlayerAnim = player.GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        d_max_min_viewY[0] = max_min_viewY[0];
        d_max_min_viewY[1] = max_min_viewY[1];
        crouch_speed = walk_speed * 0.3034f;
        run_speed = walk_speed * 1.3f;
        SnapToHead = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 x_z_forward = new Vector3(head.transform.forward.x, 0, head.transform.forward.z);
        Vector3 x_z_right = new Vector3(head.transform.right.x, 0, head.transform.right.z);
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 new_v = rb.velocity;
            if (MoveMode == 2)
            {
                new_v = x_z_forward * run_speed;
                PlayerAnim.SetBool("Stop", true);
                PlayerAnim.SetTrigger("Run");
                PlayerAnim.SetBool("Stop", false);
            }
            else
            {
                if (MoveMode == 1)
                {
                    new_v = x_z_forward * walk_speed;
                    PlayerAnim.SetBool("Stop", true);
                    PlayerAnim.SetTrigger("Walk");
                    PlayerAnim.SetBool("Stop", false);
                }
                else
                {
                    //new_v = x_z_forward * walk_speed;
                    PlayerAnim.SetBool("Stop", true);
                    PlayerAnim.SetBool("Crouching", true);
                    PlayerAnim.SetBool("Stop", false);
                }
                
            }
            rb.velocity = new Vector3(new_v.x, rb.velocity.y, new_v.z);
            SnapToHead = true;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (CanRun("W"))
            {
                MoveMode = 2;
            }
            else
            {
                MoveMode = 1;
                RunActivationKey = "W";
                RunActivationTime = Time.time;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector3 new_v = rb.velocity;
            if (MoveMode == 2)
            {
                new_v = x_z_forward * -run_speed;
                PlayerAnim.SetBool("Stop", true);
                PlayerAnim.SetTrigger("Run");
                PlayerAnim.SetBool("Stop", false);
            }
            else
            {
                if (MoveMode == 1)
                {
                    new_v = x_z_forward * -walk_speed;
                    PlayerAnim.SetBool("Stop", true);
                    PlayerAnim.SetTrigger("Walk");
                    PlayerAnim.SetBool("Stop", false);
                }
                else
                {
                    //new_v = x_z_forward * -walk_speed;
                    PlayerAnim.SetBool("Stop", true);
                    PlayerAnim.SetBool("Crouching", true);
                    PlayerAnim.SetBool("Stop", false);
                }
            }

            rb.velocity = new Vector3(new_v.x, rb.velocity.y, new_v.z);
            SnapToHead = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 new_v = rb.velocity;
            if (MoveMode == 2)
            {
                new_v = x_z_right * -run_speed;
                PlayerAnim.SetBool("Stop", true);
                PlayerAnim.SetTrigger("Run");
                PlayerAnim.SetBool("Stop", false);
            }
            else
            {
                if (MoveMode == 1)
                {
                    new_v = x_z_right * -walk_speed;
                    PlayerAnim.SetBool("Stop", true);
                    PlayerAnim.SetTrigger("Walk");
                    PlayerAnim.SetBool("Stop", false);
                }
                else
                {
                    //new_v = x_z_right * -walk_speed;
                    PlayerAnim.SetBool("Stop", true);
                    PlayerAnim.SetBool("Crouching", true);
                    PlayerAnim.SetBool("Stop", false);
                }
            }

            rb.velocity = new Vector3(new_v.x, rb.velocity.y, new_v.z);
            SnapToHead = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 new_v = rb.velocity;
            if (MoveMode == 2)
            {
                new_v = x_z_right * run_speed;
                PlayerAnim.SetBool("Stop", true);
                PlayerAnim.SetTrigger("Run");
                PlayerAnim.SetBool("Stop", false);
            }
            else
            {
                if (MoveMode == 1)
                {
                    new_v = x_z_right * walk_speed;
                    PlayerAnim.SetBool("Stop", true);
                    PlayerAnim.SetTrigger("Walk");
                    PlayerAnim.SetBool("Stop", false);
                }
                else
                {
                    //new_v = x_z_right * walk_speed;
                    PlayerAnim.SetBool("Stop", true);
                    PlayerAnim.SetBool("Crouching", true);
                    PlayerAnim.SetBool("Stop", false);
                }
            }

            rb.velocity = new Vector3(new_v.x, rb.velocity.y, new_v.z);
            SnapToHead = true;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //MoveMode = 0;
            //PlayerAnim.SetBool("Stop", false);
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Collider[] collisions = Physics.OverlapSphere(GroundDetector.position, 0.3f, can_jump_on, QueryTriggerInteraction.Collide);
            if (collisions.Length > 0)
            {
                print(collisions[0]);
                rb.velocity = new Vector3(rb.velocity.x, jump_speed, rb.velocity.z);
            }
                  
        }

        if (Input.anyKey == false)
        {
            PlayerAnim.SetBool("Stop", true);
            if (MoveMode == 2)
            {
                EndActivationTime = Time.time;
            }
            MoveMode = 1;          
        }

        if (XRot - Input.GetAxis("Mouse Y") * 4 < max_min_viewX[0] && XRot - Input.GetAxis("Mouse Y") * 4 > max_min_viewX[1])
        {
            XRot -= Input.GetAxis("Mouse Y") * 4;
        }

        max_min_viewY[0] = d_max_min_viewY[0] + player.transform.localEulerAngles.y;
        max_min_viewY[1] =  d_max_min_viewY[1] + player.transform.localEulerAngles.y;

        if (YRot < 0)
        {
            YRot += 360;
        }
        else
        {
            if (YRot > 360)
            {
                YRot -= 360;
            }
        }
        if (YRotBody < 0)
        {
            YRotBody += 360;
        }
        else
        {
            if (YRotBody > 360)
            {
                YRotBody -= 360;
            }
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
        //Debug.Log(YRot.ToString() + " " + YRotBody.ToString());

        if (SnapToHead == true)
        {
            if (smoothlerp_start == 0)
            {
                smoothlerp_start = Time.time;
            }

            YRotBody = Mathf.SmoothDampAngle(YRotBody, YRot, ref current_body_smoothlerp_v, 1, 100, (Time.time - smoothlerp_start));
        }
        else
        {
            smoothlerp_start = 0;
        }

        player.transform.rotation = Quaternion.Euler(new Vector3(player.transform.rotation.x, (float)YRotBody, player.transform.rotation.z));
        SnapToHead = false;
    }

    bool CanRun(string key_pressed)
    {
        if (RunActivationKey == key_pressed && Time.time - RunActivationTime < 0.25 && Time.time - EndActivationTime > 0.25)
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(GroundDetector.position, 0.3f);
    }
}
