using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RingTutorialController : MonoBehaviour
{
    public GameObject raycastpoint;
    private Vector3 startPosition;
    private Quaternion startRotation;

    //Tempo minimo entre erros (para eliminar erros repetidos)
    private float failCooldown = 1f;
    //Contador do tempo entre erros
    public float failTimeout;
    public AudioClip doorOpenSFX;
    public AudioClip failSFX;
    public AudioSource audioSource;
    private RingMouse ringMouse;

    public Vector2 moveDirection;

    void Awake(){
        GameObject obj = GameObject.FindGameObjectWithTag("GameController");
        if(obj!=null){
            Destroy(obj);
        }
        obj = GameObject.FindGameObjectWithTag("Music");
        if(obj!=null){
            Destroy(obj);
        }
    }
    void Start()
    {
        ringMouse=GetComponent<RingMouse>();
        startPosition=transform.position;
        startRotation=transform.rotation;
        Cursor.lockState=CursorLockMode.Locked;
        failTimeout=0f;
    }
    void Update()
    {
        failTimeout-=Time.deltaTime;

        //Bloquear a rotacao do anel no eixo X
        transform.rotation=Quaternion.Euler(transform.rotation.eulerAngles.x, 90, 0);

        moveDirection=ringMouse.moveDir;
        //Movimento do anel em world coordinates, nao em relacao a posicao/orientacao do anel
        if(moveDirection!=Vector2.zero && failTimeout<0){
            //Vector3 m_Input = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"),0);
            Vector3 m_Input = new Vector3(moveDirection.x, moveDirection.y,0);
            transform.Translate(m_Input*Time.deltaTime,Space.World);
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
            audioSource.PlayOneShot(doorOpenSFX);
        }
        else{
            audioSource.PlayOneShot(failSFX);
            failTimeout=failCooldown;
        }
        transform.position=startPosition;
        transform.rotation=startRotation;
        
    }

    public void moveToPadlockTutorial(){
        SceneManager.LoadScene("PadlockTutorial");
    }

    // public void VirtualMoveInput(Vector2 virtualMoveDirection){
    //     moveDirection=virtualMoveDirection;
    // }
}
