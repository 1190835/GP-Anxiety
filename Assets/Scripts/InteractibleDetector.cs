using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor;
using UnityEngine;
/*
Esta classe define o comportamento que da cast de um raio na direcao da vista do jogador
Este raio é usado para detetar se o jogador está a olhar para um objeto interagivel
*/
public class InteractibleDetector : MonoBehaviour
{
    //A main camera da cena, attached ao jogador pelo cinemachine
    private GameObject camera;
    public GameObject interactIcon;
    void Start(){
        camera = GameObject.Find("Main Camera");
    }
    // Update is called once per frame
    void Update()
    {
        if(camera!=null){
            RaycastHit hit;
            Vector3 fwd = camera.transform.TransformDirection(Vector3.forward)*100;
            //Debug.DrawRay(camera.transform.position,fwd,Color.green);
            if(Physics.Raycast(camera.transform.position,fwd,out hit, 10)){
                if(hit.collider.tag=="Interactible"){
                    interactIcon.SetActive(true);
                    if(Input.GetKey(KeyCode.Mouse0)){
                        hit.collider.GetComponent<IInteractible>().Interact();
                    }
                }
            }
            else{
                interactIcon.SetActive(false);
            }
        }
        
    }
}