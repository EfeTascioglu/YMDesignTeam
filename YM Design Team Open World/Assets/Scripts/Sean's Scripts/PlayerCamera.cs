using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera cam;
    private Animator CamAnim;
    // Start is called before the first frame update
    void Start()
    {
        CamAnim = cam.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CamAnim.SetTrigger("Switch");
        }
    }
}
