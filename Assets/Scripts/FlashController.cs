using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class FlashController : MonoBehaviour
{
    public Animation anim;
    public StarterAssetsInputs _input;
    public GameObject flashButton;
    public float flashTimeout;
    private float flashCooldown = 2f;
    void Start(){
        //_input = GetComponent<StarterAssetsInputs>();
        flashTimeout=0f;
    }

    // Update is called once per frame
    void Update()
    {
        flashTimeout-=Time.deltaTime;
        if(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().hasCamera){
            flashButton.SetActive(true);
        }
        if(_input.flash && flashTimeout<=0){
            anim.Play();
            flashTimeout=flashCooldown;
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().incrementFlashCount();
        }
    }
}
