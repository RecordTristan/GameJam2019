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
    private KeyController _inputRules;
    private Dictionary<string,PlayerController> dicPlayers = new Dictionary<string, PlayerController>();

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
    }

    public void AddNewPlayer(PlayerController player){
        dicPlayers.Add(player.key.ToString(),player);
    }

}
