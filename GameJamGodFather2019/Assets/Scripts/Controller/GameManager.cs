using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyGamepad
{
    X,
    Y,
    B,
    A
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public RulesController rules;
    public bool canPress = false;
    private KeyController _inputRules;
    private Dictionary<string,PlayerController> dicPlayers = new Dictionary<string, PlayerController>();
    private bool startTimer = true;

    void Awake(){
        if(GameManager.instance != null){
            if(GameManager.instance != this){
                Destroy(this.gameObject);
            }
        }else{
            instance = this;
        }

        if(rules == null){
            rules = GetComponent<RulesController>();
        }
        if(_inputRules == null){
            _inputRules = GetComponent<KeyController>();
        }
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            StartCoroutine(Action());
        }
    }

    public void AddNewPlayer(PlayerController player){
        dicPlayers.Add(player.key.ToString(),player);
    }

    public PlayerController GetPlayer(string player){
        return dicPlayers[player];
    }

    public PlayerController Attack(PlayerController player, string target){
        PlayerController targetPlayer = GetPlayer(target);
        _inputRules.Attack(player,targetPlayer);
        return targetPlayer;
    }

    public void Block(PlayerController player){
        _inputRules.Block(player);
    }

    public IEnumerator Action(){
        if(startTimer){
            yield return new WaitForSeconds(3f);
            startTimer = false;
        }
        canPress = true;
        yield return new WaitForSeconds(rules.timeSelect);
        canPress = false;
        CheckActions();
    }

    public void CheckActions(){
        foreach(PlayerController item in dicPlayers.Values){
            if(item.GetTarget() != null){
                _inputRules.Attack(item,item.GetTarget());
            }else if(item.IsBlock()){
                _inputRules.Block(item);
            }else{
                //nothing
            }
        }
    }

    public void NewRound(){
        foreach(PlayerController item in dicPlayers.Values){
            item.Init();
        }
        StartCoroutine(Action());
    }

}
