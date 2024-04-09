using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PadlockCodeSpriteController : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[10];
    private int[] code;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 5){
            code=GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().padlockCode1;
        }
        else if(SceneManager.GetActiveScene().buildIndex==4){
            code=GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().padlockCode2;
        }
        GameObject canvas = GetComponentInChildren<Canvas>().gameObject;
        if(code!=null && canvas!=null){
            int i =0;
            foreach(Transform child in canvas.transform){
                child.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[code[i]];
                i++;
            }
        }
    }
}
