using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour
{
    public GameObject raycastpoint;

    void Update()
    {
        Vector3 m_Input = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"),0);
        transform.Translate(m_Input*Time.deltaTime*5,Space.World);
        rotate();
    }
    void rotate(){
        RaycastHit hit;
        Vector3 fwd = raycastpoint.transform.TransformDirection(Vector3.forward)*1000;
        Debug.DrawRay(raycastpoint.transform.position,fwd,Color.green);
        if(Physics.Raycast(raycastpoint.transform.position,fwd, out hit)){
            
            if(hit.distance>0.01){
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
                transform.rotation = rot;
                transform.Rotate(0,90,0);
            }
        }
    }
    private void OnTriggerEnter(){
        Debug.Log("ehhhhh");
    }
}
