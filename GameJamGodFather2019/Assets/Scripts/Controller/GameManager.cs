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
    public bool duelPress = false;
    private KeyController _inputRules;
    private Dictionary<string,PlayerController> dicPlayers = new Dictionary<string, PlayerController>();
    private bool startTimer = true;
    private bool reload = false;
    private bool _play = true;

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
        _play = true;
        StartCoroutine(Action());
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.R) && reload){
            Reload();
        }
        if(_play){
            List<PlayerController> players = dicPlayers.Select(g => g.Value).Where(g => !g.IsDead()).ToList();
            if(players.Count == 1){
                _play = false;
                StopAllCoroutines();
                UIManager.instance.GoHide();
                UIManager.instance.ExclamationHide();

                reload = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.P)){
            if(Time.timeScale > 0){
                Pause();
            }else{
                Play();
            }
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
            for(int i = players.Count;i-->0;){
                players[i].DuelMode();
            }
            duelActive = true;
            UIManager.instance.RedGo();
            StartCoroutine(Duel());
        }else if(players.Count > 2){
            UIManager.instance.BlackGo();
            StartCoroutine(Action());
        }else{
            reload = true;
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
        if(!duelPress){
            UIManager.instance.ExclamationHide();
            CheckActions();
            foreach(PlayerController item in dicPlayers.Values){
                item.Init();
            }
            yield return new WaitForSeconds(2f);
            NewRound();
        }
    }

    public void AttackDuel(){
        CheckActions();
        foreach(PlayerController item in dicPlayers.Values){
            item.Init();
        }
    }

    public void Reload(){
        foreach(PlayerController item in dicPlayers.Values){
            item.Reload();
        }
        startTimer = true;
        duelPress = false;
        canPress = false;
        reload = false;
        duelActive = false;
        _play = true;
        NewRound();
    }

    public void Pause(){
        Time.timeScale = 0f;
    }
    public void Play(){
        Time.timeScale = 1f;
    }

}
