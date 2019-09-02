using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health;
    public int shield;
    public KeyGamepad key;
    private int shieldMax;

    void Start(){
        GameManager.instance.AddNewPlayer(this);
        shieldMax = GameManager.instance.rules.shieldMax;
        health = GameManager.instance.rules.health;
    }

    public void Update(){
        if (Input.GetKey(KeyCode.JoystickButton2))//Intéraction
        {

        }else if(Input.GetKey(KeyCode.JoystickButton2)){

        }else if(Input.GetKey(KeyCode.JoystickButton2)){
            
        }else if(Input.GetKey(KeyCode.JoystickButton2)){
            
        }
    }
}
