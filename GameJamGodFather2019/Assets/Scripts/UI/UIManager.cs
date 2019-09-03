using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject exclamationGroup;
    public TextMeshProUGUI GoImage;
    
    void Awake(){
        if(GameManager.instance != null){
            if(GameManager.instance != this){
                Destroy(this.gameObject);
            }
        }else{
            instance = this;
        }

        ExclamationHide();
        GoHide();
    }

    public void ExclamationHide(){
        exclamationGroup.SetActive(false);
    }

    public void ExclamationShow(){
        exclamationGroup.SetActive(true);
    }

    public void GoHide(){
        GoImage.gameObject.SetActive(false);
    }
    public void GoShow(){
        GoImage.gameObject.SetActive(true);
    }

    public void RedGo(){
        GoImage.color = Color.red;
    }

    public void BlackGo(){
        GoImage.color = Color.black;
    }
}
