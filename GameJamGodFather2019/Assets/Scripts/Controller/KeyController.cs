using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public void Attack(PlayerController Player,PlayerController Target){
        if(Player != Target.GetTarget() && !Target.IsBlock()){
            Player.Attack();
            Target.Hit();
        }else{
            Player.Attack();
        }
    }

    public void Block(PlayerController Player){
        Player.Block();
    }
}
