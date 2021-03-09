using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubemovement : MonoBehaviour
{
    
    void Start()
    {
        
    }

   
    void Update()
    {
    	//Input.GetAxis();
    	//changes rate to once per second instead of once per frame
       transform.Translate(1 * Time.deltaTime, 0, 0);
    }
}
