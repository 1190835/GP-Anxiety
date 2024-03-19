using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class FlashController : MonoBehaviour
{
    public Animation anim;
    private StarterAssetsInputs _input;
    public GameObject flashButton;
    void Start(){
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().hasCamera){
            flashButton.SetActive(true);
        }
        if(_input.flash){
            anim.Play();
        }
    }
}
