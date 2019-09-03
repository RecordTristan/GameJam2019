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
                if (Input.GetKeyDown(KeyCode.Joystick1Button16) || Input.GetKeyDown(KeyCode.S))//Intéraction
                {
                    Action("A");
                }else if(Input.GetKeyDown(KeyCode.Joystick1Button17) || Input.GetKeyDown(KeyCode.D)){
                    Action("B");
                    TargetImages[2].SetActive(true);
                }else if(Input.GetKeyDown(KeyCode.Joystick1Button18) || Input.GetKeyDown(KeyCode.Q)){
                    Action("X");
                    TargetImages[0].SetActive(true);
                }else if(Input.GetKeyDown(KeyCode.Joystick1Button19) || Input.GetKeyDown(KeyCode.Z)){
                    Action("Y");
                    TargetImages[1].SetActive(true);
                }
            break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Joystick2Button16) || Input.GetKeyDown(KeyCode.G))//Intéraction
                {
                    Action("A");
                    TargetImages[1].SetActive(true);
                }else if(Input.GetKeyDown(KeyCode.Joystick2Button17) || Input.GetKeyDown(KeyCode.H)){
                    Action("B");
                    TargetImages[2].SetActive(true);
                }else if(Input.GetKeyDown(KeyCode.Joystick2Button18) || Input.GetKeyDown(KeyCode.F)){
                    Action("X");
                }else if(Input.GetKeyDown(KeyCode.Joystick2Button19) || Input.GetKeyDown(KeyCode.T)){
                    Action("Y");
                    TargetImages[0].SetActive(true);
                }
            break;
            case 3:
                if (Input.GetKeyDown(KeyCode.Joystick3Button16))//Intéraction
                {
                    Action("A");
                    TargetImages[1].SetActive(true);
                }else if(Input.GetKeyDown(KeyCode.Joystick3Button17)){
                    Action("B");
                    TargetImages[2].SetActive(true);
                }else if(Input.GetKeyDown(KeyCode.Joystick3Button18)){
                    Action("X");
                    TargetImages[0].SetActive(true);
                }else if(Input.GetKeyDown(KeyCode.Joystick3Button19)){
                    Action("Y");
                }
            break;
            case 4:
                if (Input.GetKeyDown(KeyCode.Joystick4Button16))//Intéraction
                {
                    Action("A");
                    TargetImages[2].SetActive(true);
                }else if(Input.GetKeyDown(KeyCode.Joystick4Button17)){
                    Action("B");
                }else if(Input.GetKeyDown(KeyCode.Joystick4Button18)){
                    Action("X");
                    TargetImages[1].SetActive(true);
                }else if(Input.GetKeyDown(KeyCode.Joystick4Button19)){
                    Action("Y");
                    TargetImages[0].SetActive(true);
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
        HideArrows();
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
