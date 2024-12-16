using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    private Vector3 entradasJogador;
    private CharacterController characterController;
    public Transform _transform;
    public float velocidadeJogador = 4f;
    public Transform myCamera;
    // private bool estaNoChao;
    // [SerializeField] private Transform veficadorChao;
    // [SerializeField] private LayerMask cenarioMask;
    // [SerializeField] private float alturaDoSalto = 1f;
    // private float gravidade = -9.81f;
    // private float velocidadeVertical;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Start(){

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    void Update()
    {
        _transform.eulerAngles = new Vector3(0, myCamera.eulerAngles.y,0);
        entradasJogador = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        entradasJogador = transform.TransformDirection(entradasJogador);
        characterController.Move(entradasJogador * Time.deltaTime * velocidadeJogador);
        // characterController.Move(entradasJogador*Time.deltaTime);
        // estaNoChao = Physics.CheckSphere(veficadorChao.position, 0.3f, cenarioMask);
        // if(Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        // {
        //     velocidadeVertical = Mathf.Sqrt(alturaDoSalto * -2f * gravidade);
        // }
        // if(estaNoChao && velocidadeVertical < 0)
        // {
        //     velocidadeVertical = -1f;
        // }
        // velocidadeVertical += gravidade * Time.deltaTime;
        // characterController.Move(new Vector3(0, velocidadeVertical, 0) * Time.deltaTime);
    }
}
