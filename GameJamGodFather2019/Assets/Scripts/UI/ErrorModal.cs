using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorModal : MonoBehaviour
{
    public string textController;
    public TextMeshProUGUI textDisplay;
    List<int> problem = new List<int>();

    public void ControllerProblem(int i){
        if(!problem.Contains(i+1)){
            problem.Add(i+1);
        }
        this.gameObject.SetActive(true);
        textDisplay.text = string.Format(textController,i);
        GameManager.instance.Pause();
    }

    public void ControllerNoProblem(int i){
        if(problem.Contains(i+1)){
            problem.Remove(i+1);
            this.gameObject.SetActive(false);
            GameManager.instance.Play();
        }
    }

}
