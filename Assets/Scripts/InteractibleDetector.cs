using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEditor;
using UnityEngine;
/*
Esta classe define o comportamento que da cast de um raio na direcao da vista do jogador
Este raio é usado para detetar se o jogador está a olhar para um objeto interagivel
Quando o raio colide com um objeto interagivel, o jogador pode ativar o comportamento associado a esse objeto
*/
public class InteractibleDetector : MonoBehaviour
{
    //A main camera da cena, attached ao jogador pelo cinemachine
    private GameObject camera;
    public GameObject interactIcon;
    public GameObject interactButton;
    private StarterAssetsInputs _input;
    void Start(){
        camera = GameObject.Find("Main Camera");
        _input = GetComponent<StarterAssetsInputs>();
    }
    // Update is called once per frame
    void Update()
    {
        if(camera!=null){
            RaycastHit hit;
            Vector3 fwd = camera.transform.TransformDirection(Vector3.forward)*20;
            Debug.DrawRay(camera.transform.position,fwd,Color.green);
            if(Physics.Raycast(camera.transform.position,fwd,out hit, 100)){
                if(hit.collider.tag=="Interactible"){
                    Debug.Log(hit.collider.name);
                    interactIcon.SetActive(true);
                    interactButton.SetActive(true);
                    // if(Input.GetKey(KeyCode.Mouse0)){
                    //     hit.collider.GetComponent<IInteractible>().Interact();
                    // }
                    if(_input.interact){
                        hit.collider.GetComponent<IInteractible>().Interact();
                    }
                }
                else{
                    interactIcon.SetActive(false);
                    interactButton.SetActive(false);
                }
            }
        }
        
    }
}
