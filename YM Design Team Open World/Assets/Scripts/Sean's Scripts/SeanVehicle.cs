using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //all this does is make this class able to be edited in the inspector
public class AxleInfo
{
    public WheelCollider left;
    public WheelCollider right;
    public bool can_steer;
    public bool powered;
}
public class SeanVehicle : MonoBehaviour
{
    //public GameObject Armature;
    //public GameObject Vehicle;

    public List<AxleInfo> AxleInfo;
    public float MaxAngle;
    public float MaxSpeed;
    public float current_angle;
    public float current_speed;

    public float acceleration;
    public float turn_speed;
    public bool brakes;

    private GameObject armature;
    private Rigidbody rb;
    public Transform car_origin;

    //public BoxCollider collider;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (armature == null)
        {
            return;
        }
        else
        {
            armature.transform.localScale = new Vector3(0, 0, 0);
            armature.transform.position = car_origin.position;

            if(rb != null)
            {
                rb.useGravity = false;
            }
            
        }
        brakes = false;
        if (Input.GetKey(KeyCode.Space))
        {
            armature.transform.position = car_origin.position + (car_origin.right * 1.5f);
            armature.transform.localScale = new Vector3(1, 1, 1);
            if (rb != null)
            {
                rb.useGravity = true;
                rb = null;
            }
            armature = null;
            brakes = true;
        }

        if (Input.GetKey(KeyCode.W))
        {
            current_speed = MaxSpeed;
        }
        else
        {
            if (Input.GetKey(KeyCode.S))
            {
                current_speed = -MaxSpeed;

            }
            else
            {
                brakes = true;
            }
        }
        

        if (Input.GetKey(KeyCode.D))
        {
            current_angle = MaxAngle;
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {                
                current_angle = -MaxAngle;
            }
            else
            {
                current_angle = 0;
            }
        }

        foreach (var axle in AxleInfo)
        {
            Transform left_t = axle.left.gameObject.transform;
            Transform right_t = axle.right.gameObject.transform;

            if (axle.can_steer)
            {
                axle.left.steerAngle = current_angle;
                axle.right.steerAngle = current_angle;

                Quaternion new_left_rot = Quaternion.Euler(0, current_angle, axle.left.rpm / 240 * Time.deltaTime);
                Quaternion new_right_rot = Quaternion.Euler(0, current_angle, axle.right.rpm / 240 * Time.deltaTime);
                print(new_left_rot);
                axle.left.gameObject.transform.localRotation = new_left_rot;
                axle.right.gameObject.transform.localRotation = new_right_rot;
            }

            if (axle.powered)
            {
                axle.left.motorTorque = current_speed;
                axle.right.motorTorque = current_speed;

                if(axle.can_steer == false)
                {
                    Quaternion new_left_rot = Quaternion.Euler(0, 0, axle.left.rpm / 240 * Time.deltaTime);
                    Quaternion new_right_rot = Quaternion.Euler(0, 0, axle.right.rpm / 240 * Time.deltaTime);

                    axle.left.gameObject.transform.localRotation = new_left_rot;
                    axle.right.gameObject.transform.localRotation = new_right_rot;
                }
               
            }


            

            if (brakes)
            {
                axle.left.wheelDampingRate = 100;
                axle.right.wheelDampingRate = 100;
                current_speed = 0;
            }
            else
            {
                axle.left.wheelDampingRate = 0.25f;
                axle.right.wheelDampingRate = 0.25f;
                current_speed = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<MinecraftPlayerMovement>())
        {
            armature = other.gameObject;
            rb = armature.GetComponent<Rigidbody>();
        }
    }

}
