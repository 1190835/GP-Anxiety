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
            if(Physics.Raycast(camera.transform.position,fwd,out hit, 20)){
                if(hit.collider.tag=="Interactible"){
                    //Debug.Log(hit.collider.name);
                    interactIcon.SetActive(true);
                    interactButton.SetActive(true);
                    // if(Input.GetKey(KeyCode.Mouse0)){
                    //     hit.collider.GetComponent<IInteractible>().Interact();
                    // }
                    if(_input.interact){
                        hit.collider.GetComponent<IInteractible>().Interact();
                        //failsafe. em casos onde interagir com o objeto faz com que o objeto desapareca (collectibles), o botao de interagir desaparece -> n ha button up event -> interact fica stuck em true. isto forca o interact a mudar para false
                        _input.InteractInput(false);
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
