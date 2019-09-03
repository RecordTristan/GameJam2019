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
    public GameObject[] TargetImages;
    private int _shieldMax;

    private PlayerController _target;
    private string _targetKey;
    private bool _isBlocking = false;

    private bool _isDead = false;

    void Awake(){
        HideArrows();
    }

    void Start(){
        GameManager.instance.AddNewPlayer(this);
        _shieldMax = GameManager.instance.rules.shieldMax;
        _health = GameManager.instance.rules.health;
        _shield = GameManager.instance.rules.shieldStart;
        healthDisplay.Init(_health);
        shieldDisplay.Init(_shield);
    }

    public void Update(){
        if(_isDead)
            return;
        if(!GameManager.instance.canPress)
            return;
        switch(id){
            case 1:
                if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.S))//Intéraction
                {
                    HideArrows();
                    Action("A");
                }else if(Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.D)){
                    HideArrows();
                    TargetImages[2].SetActive(true);
                    Action("B");
                }else if(Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Q)){
                    HideArrows();
                    TargetImages[0].SetActive(true);
                    Action("X");
                }else if(Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.Z)){
                    HideArrows();
                    TargetImages[1].SetActive(true);
                    Action("Y");
                }
            break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Joystick2Button0) || Input.GetKeyDown(KeyCode.G))//Intéraction
                {
                    HideArrows();
                    TargetImages[2].SetActive(true);
                    Action("A");
                }else if(Input.GetKeyDown(KeyCode.Joystick2Button1) || Input.GetKeyDown(KeyCode.H)){
                    HideArrows();
                    TargetImages[1].SetActive(true);
                    Action("B");
                }else if(Input.GetKeyDown(KeyCode.Joystick2Button2) || Input.GetKeyDown(KeyCode.F)){
                    HideArrows();
                    Action("X");
                }else if(Input.GetKeyDown(KeyCode.Joystick2Button3) || Input.GetKeyDown(KeyCode.T)){
                    HideArrows();
                    TargetImages[0].SetActive(true);
                    Action("Y");
                }
            break;
            case 3:
                if (Input.GetKeyDown(KeyCode.Joystick3Button0))//Intéraction
                {
                    HideArrows();
                    TargetImages[1].SetActive(true);
                    Action("A");
                }else if(Input.GetKeyDown(KeyCode.Joystick3Button1)){
                    HideArrows();
                    TargetImages[0].SetActive(true);
                    Action("B");
                }else if(Input.GetKeyDown(KeyCode.Joystick3Button2)){
                    HideArrows();
                    TargetImages[2].SetActive(true);
                    Action("X");
                }else if(Input.GetKeyDown(KeyCode.Joystick3Button3)){
                    HideArrows();
                    Action("Y");
                }
            break;
            case 4:
                if (Input.GetKeyDown(KeyCode.Joystick4Button0))//Intéraction
                {
                    HideArrows();
                    TargetImages[0].SetActive(true);
                    Action("A");
                }else if(Input.GetKeyDown(KeyCode.Joystick4Button1)){
                    HideArrows();
                    Action("B");
                }else if(Input.GetKeyDown(KeyCode.Joystick4Button2)){
                    HideArrows();
                    TargetImages[1].SetActive(true);
                    Action("X");
                }else if(Input.GetKeyDown(KeyCode.Joystick4Button3)){
                    HideArrows();
                    TargetImages[2].SetActive(true);
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

    public bool IsDead(){
        return _isDead;
    }

    public void Action(string Key){
        if(GameManager.instance.dieTime){
            Hit();
            return;
        }
        if(Key == key.ToString()){
            if(_shield > 0){
                TargetImages[3].SetActive(true);
                _isBlocking = true;
                _target = null;
                // GameManager.instance.Block(this);
                // _isBlocking = true;
                // _shield--;
                // shieldDisplay.Use();
            }else{
                TargetImages[3].SetActive(false);
            }
        }else{
            _target = GameManager.instance.GetPlayer(Key);
            if(_target == null){
                TargetImages[1].SetActive(false);
                HideArrows();
            }else{
                _isBlocking = false;
            }
            // _target = GameManager.instance.Attack(this,Key);
            // if(_shield < _shieldMax){
                // _shield++;
                // shieldDisplay.Init(_shield);
            // }
        }
    }

    public void Hit(){
        if(_isDead)
            return;
        _health--;
        healthDisplay.Use();
        if(_health == 0){
            Die();
        }
    }
    public void Die(){
        this.gameObject.SetActive(false);
        _isDead = true;
    }
    public void Block(){
        if(_isDead)
            return;
        _shield--;
        shieldDisplay.Use();
    }
    public void Attack(){
        if(_shield == 3){
            return;
        }
        _shield++;
        shieldDisplay.Init(_shield);
    }

    public void Init(){
        _isBlocking = false;
        _target = null;
        HideArrows();
    }

    public void HideArrows(){
        for(int i = TargetImages.Length;i-->0;){
            TargetImages[i].SetActive(false);
        }
    }
}
