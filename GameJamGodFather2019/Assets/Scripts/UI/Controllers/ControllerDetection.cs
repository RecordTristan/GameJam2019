using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string[] temp = Input.GetJoystickNames();
        
        if(temp.Length > 0)
        {
            for(int i =0; i < temp.Length; ++i)
            {
                if(!string.IsNullOrEmpty(temp[i]))
                {
                    Debug.Log("Controller " + i + " is connected using: " + temp[i]);
                }
                else
                {
                    Debug.Log("Controller: " + i + " is disconnected.");
                }
            }
        }  
    }
}
