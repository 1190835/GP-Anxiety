using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Text = UnityEngine.UI.Text;
using Image = UnityEngine.UI.Image;
using Button = UnityEngine.UI.Button;

public class StartingTutorialController : MonoBehaviour
{
    public int  phase = 0;
    public GameObject thumbstick;
    public GameObject interactButton;
    private Text tipText;
    void Start(){
        tipText = GameObject.Find("Tip").GetComponent<Text>();
        thumbstick.GetComponent<Image>().color = Color.red;
    }
    public void nextPhase(){
        phase++;
        updateTipText();
    }
    private void updateTipText(){
        if(phase == 1){
            tipText.text="Swipe anywhere on the <color=red>right</color> side of the screen to move the camera";
            thumbstick.GetComponent<Image>().color = Color.white;
        }
        if(phase == 2){
            tipText.text="When you see the <color=red>Hand Icon</color>, press the <color=red>Interact Button</color> to interact with an object";
            interactButton.SetActive(true);
            interactButton.GetComponent<Button>().enabled = false;
            interactButton.GetComponent<Image>().color = Color.red;
        }
        if(phase ==3){
            interactButton.SetActive(false);
            interactButton.GetComponent<Button>().enabled = true;
            interactButton.GetComponent<Image>().color = Color.white;
            tipText.text="You must find the <color=green>Key</color> to the <color=green>Exit</color> door in order to escape.";
        }
        if(phase==4){
            this.gameObject.SetActive(false);
        }
    }
}
