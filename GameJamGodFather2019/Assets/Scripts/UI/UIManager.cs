using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject exclamationGroup;
    
    void Awake(){
        if(GameManager.instance != null){
            if(GameManager.instance != this){
                Destroy(this.gameObject);
            }
        }else{
            instance = this;
        }
        
        ExclamationHide();
    }

    public void ExclamationHide(){
        exclamationGroup.SetActive(false);
    }

    public void ExclamationShow(){
        exclamationGroup.SetActive(true);
    }
}
