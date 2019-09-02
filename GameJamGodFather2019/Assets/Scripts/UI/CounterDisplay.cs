using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterDisplay : MonoBehaviour
{
    public GameObject[] display;

    void Update(){
        if(Input.GetKeyDown("m")){
            Use();
        }else if(Input.GetKeyDown("n")){
            Init(3);
        }
    }

    public void Init(int nbr){
        for(int i=nbr;i-->0;){
            display[i].SetActive(true);
        }
    }

    public void Use(){
        for(int i=0;i<display.Length;i++){
            if(display[i].activeSelf){
                display[i].SetActive(false);
                break;
            }
        }
    }
}
