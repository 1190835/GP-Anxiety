using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RingController : MonoBehaviour
{
    public GameObject raycastpoint;
    private Vector3 startPosition;
    private Quaternion startRotation;

    //Numero de vezes que o jogador cometeu um erro
    public int failCounter;
    //Tempo minimo entre erros (para eliminar erros repetidos)
    private float failCooldown = 1.5f;
    //Contador do tempo entre erros
    public float failTimeout;
    //Tempo decorrido
    public float timer;
    public AudioClip doorOpenSFX;
    public AudioSource audioSource;

    public Vector2 moveDirection;
    void Start()
    {
        startPosition=transform.position;
        startRotation=transform.rotation;
        Cursor.lockState=CursorLockMode.Locked;
        timer = 0f;
        failTimeout=0f;
        failCounter=0;
    }
    void Update()
    {
        failTimeout-=Time.deltaTime;
        timer+=Time.deltaTime;

        //Bloquear a rotacao do anel no eixo X
        transform.rotation=Quaternion.Euler(transform.rotation.eulerAngles.x, 90, 0);

        //Movimento do anel em world coordinates, nao em relacao a posicao/orientacao do anel
        if(moveDirection!=Vector2.zero){
            //Vector3 m_Input = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"),0);
            Vector3 m_Input = new Vector3(moveDirection.x, moveDirection.y,0);
            transform.Translate(m_Input*Time.deltaTime*10,Space.World);
            //Debug.Log(m_Input.ToString());
        }

        //Bloquear movimento para o limite esquerdo do puzzle
        if(transform.position.x<startPosition.x){
            transform.position=startPosition;
        }
        rotate();
    }

    //Da cast de um raio ao longo do buraco do anel, ao colidir com a mesh do tubo, usa a normal da colisao para rodar o anel para ser sempre
    //perpendicular ao tubo
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

    //Chamado quando o anel colide com um collider. Neste caso pode ser o tubo ou a "finish area"
    //No caso do tubo, a posicao do anel e reset para o inicio e o erro e contabilizado
    //No caso da finish area, o puzzle acaba
    private void OnTriggerEnter(Collider other){
        if(other.tag=="Finish"){
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().saveRingMetrics(failCounter,timer);
            this.enabled=false;
            audioSource.PlayOneShot(doorOpenSFX);
            //Mudar de cena quando o SFX tiver acabado de tocar
            Invoke("ChangeScene",doorOpenSFX.length);
            return;
        }
        transform.position=startPosition;
        transform.rotation=startRotation;
        //Erro cooldown check
        if(failTimeout<=0){
            //Incrementar erros e iniciar cooldown
            failCounter++;
            failTimeout=failCooldown;
        }
    }

    private void ChangeScene(){
        Debug.Log("changing scenes");
        if(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().hasCamera){
            SceneManager.LoadScene("MainHallwayOtherworld");
        }
        else{
            SceneManager.LoadScene("SideHallway");
        }
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().showUI();
    }

    public void VirtualMoveInput(Vector2 virtualMoveDirection){
        moveDirection=virtualMoveDirection;
    }
}
