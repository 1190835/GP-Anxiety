using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRingCollision : MonoBehaviour
{

    public GameObject ring;
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        startPosition=ring.transform.position;
        startRotation=ring.transform.rotation;
    }

    void onTriggerEnter(){
        Debug.Log("teste");
    }
}
