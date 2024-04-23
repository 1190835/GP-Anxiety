using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PadlockCodeSpriteController : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[10];
    private int[] code;
    // Este gameobject deve ter 5 child objects. 5 sprites. A imagem de cada child e atribuida
    // de acordo com a combinacao do padlock que foi gerada no inicio do jogo
    // Estas imagens sao guardadas aqui em sprites[] por ordem de 0->9
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 6){
            code=GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().padlockCode1;
        }
        else if(SceneManager.GetActiveScene().buildIndex==5){
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
