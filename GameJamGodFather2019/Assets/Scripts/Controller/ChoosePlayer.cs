using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePlayer : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.choosePlayer)
            return;
        if (Input.GetKeyDown("joystick 1 button 0"))//Intéraction
        {
            Choose(1,KeyGamepad.A);
        }else if(Input.GetKeyDown("joystick 1 button 1")){
            Choose(1,KeyGamepad.B);
        }else if(Input.GetKeyDown("joystick 1 button 2")){
            Choose(1,KeyGamepad.X);
        }else if(Input.GetKeyDown("joystick 1 button 3")){
            Choose(1,KeyGamepad.Y);
        }else if (Input.GetKeyDown("joystick 2 button 0"))//Intéraction
        {
            Choose(2,KeyGamepad.A);
        }else if(Input.GetKeyDown("joystick 2 button 1")){
            Choose(2,KeyGamepad.B);
        }else if(Input.GetKeyDown("joystick 2 button 2")){
            Choose(2,KeyGamepad.X);
        }else if(Input.GetKeyDown("joystick 2 button 3")){
            Choose(2,KeyGamepad.Y);
        }else if (Input.GetKeyDown("joystick 3 button 0"))//Intéraction
        {
            Choose(3,KeyGamepad.A);
        }else if(Input.GetKeyDown("joystick 3 button 1")){
            Choose(3,KeyGamepad.B);
        }else if(Input.GetKeyDown("joystick 3 button 2")){
            Choose(3,KeyGamepad.X);
        }else if(Input.GetKeyDown("joystick 3 button 3")){
            Choose(3,KeyGamepad.Y);
        }else if (Input.GetKeyDown("joystick 4 button 0"))//Intéraction
        {
            Choose(4,KeyGamepad.A);
        }else if(Input.GetKeyDown("joystick 4 button 1")){
            Choose(4,KeyGamepad.B);
        }else if(Input.GetKeyDown("joystick 4 button 2")){
            Choose(4,KeyGamepad.X);
        }else if(Input.GetKeyDown("joystick 4 button 3")){
            Choose(4,KeyGamepad.Y);
        }
    }

    public void Choose(int joystick, KeyGamepad key){
        GameManager.instance.Choice(joystick,key);
    }
}
