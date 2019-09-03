using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
        
    }

    public void Quit(){
   Application.Quit();
    }

    public void Tuto(){
   SceneManager.LoadScene("");
    }

   public  void Play(){
     SceneManager.LoadScene("");
        Debug.Log("Loading!");
    }
}
