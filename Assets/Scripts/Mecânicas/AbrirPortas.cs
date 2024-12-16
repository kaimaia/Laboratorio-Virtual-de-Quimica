using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPortas : MonoBehaviour
{
    public Transform rotacionador;
    public int direcao = 1;

    bool isOpen = false;

    void OnTriggerEnter(Collider collider){
        if(collider.tag == "Player" && !isOpen)
        {            
            // Debug.Log("aaa");
            rotacionador.eulerAngles = new Vector3(0, rotacionador.eulerAngles.y + (90*direcao),0);
            isOpen = true;
        }
    }
    void OnTriggerExit(Collider collider){
        if(isOpen){
            // Debug.Log("zzz");
            isOpen = false;
            rotacionador.eulerAngles = new Vector3(0, rotacionador.eulerAngles.y - (90*direcao),0);
            // transform -90° * direction
        }
    }

}
