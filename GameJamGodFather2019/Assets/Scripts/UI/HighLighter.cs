using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HighLighter : MonoBehaviour
{
    public GameObject Selecteur;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelecteurActive()
    {
        Selecteur.SetActive(true);
    }
 
    public void SelecteurDeActive()
    {
        Selecteur.SetActive(false);
    }
}
