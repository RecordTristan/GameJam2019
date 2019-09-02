using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public void Attack(PlayerController Player,PlayerController Target){
        if(Player.GetTarget() != Target.GetTarget() && !Target.IsBlock()){
            Target.Hit();
        }
    }

    public void Block(PlayerController Player){

    }
}
