using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashController : MonoBehaviour
{
    public Animation anim;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0)){
            anim.Play();
        }
    }
}
