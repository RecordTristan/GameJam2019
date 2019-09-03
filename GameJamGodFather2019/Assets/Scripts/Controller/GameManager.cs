using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public bool dieTime = false;
    public bool duelActive = false;
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
        StartCoroutine(Action());
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            // StartCoroutine(Action());
        }
    }

    public void AddNewPlayer(PlayerController player){
        dicPlayers.Add(player.key.ToString(),player);
    }

    public PlayerController GetPlayer(string player){
        PlayerController Player = dicPlayers[player];
        if(Player.IsDead()){
            return null;
        }else{
            return Player;
        }
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
        StartCoroutine(HideShowGo());
        yield return new WaitForSeconds(rules.timeSelect-1);
        UIManager.instance.ExclamationShow();
        yield return new WaitForSeconds(1f);
        UIManager.instance.ExclamationHide();
        canPress = false;
        CheckActions();
        foreach(PlayerController item in dicPlayers.Values){
            item.Init();
        }
        yield return new WaitForSeconds(2f);
        NewRound();
    }

    public IEnumerator HideShowGo(){
        UIManager.instance.GoShow();
        yield return new WaitForSeconds(1f);
        UIManager.instance.GoHide();
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
        duelActive = false;
        List<PlayerController> players = dicPlayers.Select(g => g.Value).Where(g => !g.IsDead()).ToList();
        if(players.Count == 2){
            duelActive = true;
            StartCoroutine(Duel());
        }else{
            StartCoroutine(Action());
        }
    }

    public IEnumerator Duel(){
        canPress = true;
        dieTime = true;
        StartCoroutine(HideShowGo());
        yield return new WaitForSeconds(rules.timeSelect-rules.timeFinish);
        dieTime = false;
        UIManager.instance.ExclamationShow();
        yield return new WaitForSeconds(rules.timeFinish);
        canPress = false;
        CheckActions();
        foreach(PlayerController item in dicPlayers.Values){
            item.Init();
        }
        yield return new WaitForSeconds(2f);
        NewRound();
    }

}
