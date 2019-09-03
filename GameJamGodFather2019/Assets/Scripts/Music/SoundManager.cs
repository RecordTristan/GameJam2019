using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource music;
    private AudioSource sfx;

    public AudioClip musicPrincipal;
    [Header("SFXs")]
    public AudioClip[] insults;
    public AudioClip gresillement;
    public AudioClip swordTouch;
    public AudioClip swordClash;
    public AudioClip swordBlock;


    void Awake(){
        music = GetComponents<AudioSource>()[0];
        sfx = GetComponents<AudioSource>()[1];
    }

    #region SFXs
    public void Insult(int id){
        sfx.PlayOneShot(insults[id]);
    }
    public void Gresille(){
        sfx.PlayOneShot(gresillement);
    }
    public void SwordTouch(){
        sfx.PlayOneShot(swordTouch);
    }
    public void SwordClash(){
        sfx.PlayOneShot(swordClash);
    }
    public void SwordBlock(){
        sfx.PlayOneShot(swordBlock);
    }

    #endregion
}
