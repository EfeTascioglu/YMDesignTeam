using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeanVehicle : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    public GameObject Armature;
    public GameObject Vehicle;
    public Transform[] FrontTires;
    public float MaxAngle;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = Vehicle.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
        if (Input.GetMouseButtonDown(2))
        {
            Armature = FindObjectOfType<MinecraftPlayerMovement>().gameObject;
            
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Armature.transform.localScale = new Vector3(1, 1, 1);
            Armature.transform.position = Vehicle.transform.position + new Vector3(0, 2, 0);
            Armature.GetComponent<CapsuleCollider>().enabled = true;
            Armature = null;
        }

        if(Armature != null)
        {
            Armature.GetComponent<CapsuleCollider>().enabled = false;
            Armature.transform.position = Vehicle.transform.position;
            Armature.transform.localScale = new Vector3(0, 0, 0);

            if (Input.GetKey(KeyCode.A))
            {
                for (int i = 0; i < FrontTires.Length; i++)
                {
                    FrontTires[i].localEulerAngles = new Vector3(0, -MaxAngle, 0);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.D))
                {
                    for (int i = 0; i < FrontTires.Length; i++)
                    {
                        FrontTires[i].localEulerAngles = new Vector3(0, MaxAngle, 0);
                    }
                }
                
            }


            if (Input.GetKey(KeyCode.W))
            {
                for (int i = 0; i < FrontTires.Length; i++)
                {
                    Rigidbody ftrb = FrontTires[i].GetComponent<Rigidbody>();
                    ftrb.angularVelocity = new Vector3(speed, 0, 0);
                }
                //rb.velocity = new Vector3(FrontTires[0].transform.forward.x * speed, rb.velocity.y, FrontTires[0].transform.forward.z * speed);
            }
            else
            {
                if (Input.GetKey(KeyCode.S))
                {
                    for (int i = 0; i < FrontTires.Length; i++)
                    {
                        Rigidbody ftrb = FrontTires[i].GetComponent<Rigidbody>();
                        ftrb.angularVelocity = FrontTires[i].transform.right * -speed;
                    }
                    //rb.velocity = new Vector3(FrontTires[0].transform.forward.x * -speed, rb.velocity.y, FrontTires[0].transform.forward.z * -speed);
                }
            }
            
        }
        else
        {
            for (int i = 0; i < FrontTires.Length; i++)
            {
                Rigidbody ftrb = FrontTires[i].GetComponent<Rigidbody>();
                ftrb.angularVelocity = FrontTires[i].transform.right * speed;
            }
        }
      
    }
}
