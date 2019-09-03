using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int id;
    private int _health;
    private int _shield;
    public KeyGamepad key;
    public CounterDisplay healthDisplay;
    public CounterDisplay shieldDisplay;
    public TargetImage[] targetImages;
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
        if(GameManager.instance.choosePlayer)
            return;
        if(_isDead)
            return;
        if(!GameManager.instance.canPress && !GameManager.instance.duelPress)
            return;
        if (Input.GetKeyDown("joystick "+id+" button 0"))//Intéraction
        {
            HideArrows();
            Action("A");
        }else if(Input.GetKeyDown("joystick "+id+" button 1")){
            HideArrows();
            Action("B");
        }else if(Input.GetKeyDown("joystick "+id+" button 2")){
            HideArrows();
            Action("X");
        }else if(Input.GetKeyDown("joystick "+id+" button 3")){
            HideArrows();
            Action("Y");
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
            HideArrows();
            return;
        }
        if(Key == key.ToString()){
            if(_shield > 0){
                targetImages[3].gameObject.SetActive(true);
                _isBlocking = true;
                _target = null;
                // GameManager.instance.Block(this);
                // _isBlocking = true;
                // _shield--;
                // shieldDisplay.Use();
            }else{
                targetImages[3].gameObject.SetActive(false);
            }
        }else{
            targetImages.SingleOrDefault(g => g.key.ToString() == Key).gameObject.SetActive(true);
            _target = GameManager.instance.GetPlayer(Key);
            _isBlocking = false;
            if(_target == null){
                HideArrows();
            }else{
                if(GameManager.instance.duelActive){
                    _target.Hit();
                    HideArrows();
                }
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

    public void DuelMode(){
        _health = 1;
        _shield = 0;
        healthDisplay.Init(_health);
        shieldDisplay.Init(_shield);
    }

    public void HideArrows(){
        for(int i = targetImages.Length;i-->0;){
            targetImages[i].gameObject.SetActive(false);
        }
    }
    public void Reload(){
        this.gameObject.SetActive(true);
        _isDead = false;
        _health = GameManager.instance.rules.health;
        _shield = GameManager.instance.rules.shieldStart;
        healthDisplay.Init(_health);
        shieldDisplay.Init(_shield);
    }
}
