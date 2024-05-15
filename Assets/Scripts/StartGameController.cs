using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameController : MonoBehaviour
{
    public GameObject startUI;
    public GameObject usernameUI;
    public GameObject inputField;
    public void InputName(){
        startUI.SetActive(false);
        usernameUI.SetActive(true);
    }
    public void StartGame(){
        if(inputField.GetComponent<InputField>().text!=""){
            Debug.Log(inputField.GetComponent<InputField>().text);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().saveUsername(inputField.GetComponent<InputField>().text);
            SceneManager.LoadScene("MainHallway");
        }
    }

    public void startTutorial(){
        SceneManager.LoadScene("RingTutorial");
    }
}
