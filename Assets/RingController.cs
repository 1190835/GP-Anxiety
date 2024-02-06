using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour
{
    public GameObject raycastpoint;

    void Update()
    {
        if(Input.GetAxis("Mouse X")<0){
            transform.Translate(-Time.deltaTime,0,0,Space.World);
        }
        if(Input.GetAxis("Mouse X")>0){
            transform.Translate(Time.deltaTime,0,0,Space.World);
        }
        if(Input.GetAxis("Mouse Y")<0){
            transform.Translate(0,-Time.deltaTime,0,Space.World);
        }
        if(Input.GetAxis("Mouse Y")>0){
            transform.Translate(0,Time.deltaTime,0,Space.World);
        }
        rotate();
    }
    void rotate(){
        RaycastHit hit;
        Vector3 fwd = raycastpoint.transform.TransformDirection(Vector3.forward)*1000;
        Debug.DrawRay(raycastpoint.transform.position,fwd,Color.green);
        if(Physics.Raycast(raycastpoint.transform.position,fwd, out hit)){
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
            transform.rotation = rot;
            transform.Rotate(0,90,0);
        }
    }
    private void OnTriggerEnter(){
        Debug.Log("ehhhhh");
    }
}
