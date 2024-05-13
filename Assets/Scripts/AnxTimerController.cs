using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnxTimerController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Text>().text = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().anxTimer.ToString();
    }
}
