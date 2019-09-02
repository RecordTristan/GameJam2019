using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int id;
    private int _health;
    private int _shield;
    public KeyGamepad key;
    public CounterDisplay healthDisplay;
    public CounterDisplay shieldDisplay;
    private int _shieldMax;

    private PlayerController _target;
    private bool _isBlocking = false;

    void Start(){
        GameManager.instance.AddNewPlayer(this);
        _shieldMax = GameManager.instance.rules.shieldMax;
        _health = GameManager.instance.rules.health;
        _shield = GameManager.instance.rules.shieldStart;
        healthDisplay.Init(_health);
        shieldDisplay.Init(_shield);
    }

    public void Update(){
        switch(id){
            case 1:
                if (Input.GetKeyDown(KeyCode.Joystick1Button16) || Input.GetKeyDown(KeyCode.S))//Intéraction
                {
                    Action("A");
                }else if(Input.GetKeyDown(KeyCode.Joystick1Button17) || Input.GetKeyDown(KeyCode.D)){
                    Action("B");
                }else if(Input.GetKeyDown(KeyCode.Joystick1Button18) || Input.GetKeyDown(KeyCode.Q)){
                    Action("X");
                }else if(Input.GetKeyDown(KeyCode.Joystick1Button19) || Input.GetKeyDown(KeyCode.Z)){
                    Action("Y");
                }
            break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Joystick2Button16) || Input.GetKeyDown(KeyCode.G))//Intéraction
                {
                    Action("A");
                }else if(Input.GetKeyDown(KeyCode.Joystick2Button17) || Input.GetKeyDown(KeyCode.H)){
                    Action("B");
                }else if(Input.GetKeyDown(KeyCode.Joystick2Button18) || Input.GetKeyDown(KeyCode.F)){
                    Action("X");
                }else if(Input.GetKeyDown(KeyCode.Joystick2Button19) || Input.GetKeyDown(KeyCode.T)){
                    Action("Y");
                }
            break;
            case 3:
                if (Input.GetKeyDown(KeyCode.Joystick3Button16))//Intéraction
                {
                    Action("A");
                }else if(Input.GetKeyDown(KeyCode.Joystick3Button17)){
                    Action("B");
                }else if(Input.GetKeyDown(KeyCode.Joystick3Button18)){
                    Action("X");
                }else if(Input.GetKeyDown(KeyCode.Joystick3Button19)){
                    Action("Y");
                }
            break;
            case 4:
                if (Input.GetKeyDown(KeyCode.Joystick4Button16))//Intéraction
                {
                    Action("A");
                }else if(Input.GetKeyDown(KeyCode.Joystick4Button17)){
                    Action("B");
                }else if(Input.GetKeyDown(KeyCode.Joystick4Button18)){
                    Action("X");
                }else if(Input.GetKeyDown(KeyCode.Joystick4Button19)){
                    Action("Y");
                }
            break;
        }
    }

    public PlayerController GetTarget(){
        return _target;
    }
    public bool IsBlock(){
        return _isBlocking;
    }

    public void Action(string Key){
        if(Key == key.ToString()){
            GameManager.instance.Block(this);
            _isBlocking = true;
        }else{
            _target = GameManager.instance.Attack(this,Key);
        }
    }

    public void Hit(){
        _health--;
    }

    public void Init(){
        _isBlocking = false;
        _target = null;
    }
}
