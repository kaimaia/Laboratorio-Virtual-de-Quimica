using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balança : MonoBehaviour
{
    public GameObject solucaoBalanca;
    public GameObject naohBalao;
    public GameObject aguaBalao;
    public GameObject solucaoArmazenada;
    public GameObject balaoDeMistura;
    
    bool substanciaFinded = false;
    bool solutoFinded = false;
    bool solventeFinded = false;
    bool jaleco = false;
    // private string[] substancia = {"Mistura de Água e Óleo"};
    // private int j = 0;
    private InventoryPanel iController;


    // Start is called before the first frame update
    void Start()
    {
        iController = FindObjectOfType<Canvas>().GetComponent<InventoryPanel>();
    }

    // Update is called once per frame
    void Update()
    {   
        VerificarSubstancia("Jaleco", "jaleco");
        if(jaleco != true){
            return;
        }
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
        // mecanica fogão 
        if(Physics.Raycast(ray, out hit, 90f))
        {
            if(hit.collider.tag == "balanca")
            {
                VerificarSubstancia("Hidroxido de Sódio","substancia");
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if(substanciaFinded == true){
                        for (int i = 0; i < solucaoBalanca.transform.childCount; i++)
                        {
                            solucaoBalanca.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
                            solutoFinded = true;
                        }
                        solucaoBalanca.GetComponent<Outline>().enabled = true;
                        substanciaFinded = false;
                        UsarSubstancia("Hidroxido de Sódio");
                        hit.transform.GetComponent<ObjectType>().objectType.itemText = "Meça na balança 10mL de solução NaOH 0,5M (Press E)";
                        solucaoArmazenada.transform.tag = null;
                        solucaoArmazenada.GetComponent<Outline>().enabled = false;
                        solucaoArmazenada.GetComponent<MeshRenderer>().enabled = true;
                        solucaoArmazenada.GetComponent<ObjectType>().objectType.itemText = "Adicione a solução ao balão";
                        solucaoBalanca.GetComponent<Outline>().enabled = false;
                    } else {
                        hit.transform.GetComponent<ObjectType>().objectType.itemText = "Precisa-se de 10mL de NaOH 0,5M";
                    }
                }

            }

            if(hit.collider.tag == "soluto")
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if(solutoFinded == true)
                    {
                        naohBalao.GetComponent<MeshRenderer>().enabled = true;
                        for (int i = 0; i < solucaoBalanca.transform.childCount; i++)
                        {
                            solucaoBalanca.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
                            solutoFinded = false;
                        }
                        solucaoBalanca.GetComponent<Outline>().enabled = false;
                        balaoDeMistura.GetComponent<ObjectType>().objectType.itemText = "Pressione (E) para adicionar o solvente (Àgua)";
                    }else {
                        hit.transform.GetComponent<ObjectType>().objectType.itemText = "Precisa-se de 10mL de NaOH 0,5M";
                    }
                } 
            }

            if(hit.collider.tag == "balaoDeMistura")
            {   
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if(naohBalao.GetComponent<MeshRenderer>().enabled == true && aguaBalao.GetComponent<MeshRenderer>().enabled == true)
                    {
                        for (int i = 0; i < solucaoArmazenada.transform.childCount; i++)
                        {
                            solucaoArmazenada.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
                        }
                        solucaoArmazenada.transform.tag = "Object";
                        solucaoArmazenada.GetComponent<Outline>().enabled = true;
                        solucaoArmazenada.GetComponent<ObjectType>().objectType.itemText = "Pressione (E) para pegar o Hidróxido de Sódio 0,1M";
                        balaoDeMistura.GetComponent<ObjectType>().objectType.itemText = "Pressione (E) para adicionar o soluto (Hidróxido de Sódio)";
                        aguaBalao.GetComponent<MeshRenderer>().enabled = false;
                        naohBalao.GetComponent<MeshRenderer>().enabled = false;
                    } else if (naohBalao.GetComponent<MeshRenderer>().enabled == true) {
                        VerificarSubstancia("Becker com Água","solvente");
                        if(solventeFinded == true)
                        {
                            aguaBalao.GetComponent<MeshRenderer>().enabled = true;
                            balaoDeMistura.GetComponent<ObjectType>().objectType.itemText = "Pressione (E) para armazenar a mistura";
                            UsarSubstancia("Becker com Água");
                            solventeFinded = false;
                        } else {
                            balaoDeMistura.GetComponent<ObjectType>().objectType.itemText = "Precisa-se buscar Becker com Água, no experimento de Decantação";
                        }
                    } else {
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            if(solutoFinded == true)
                            {
                                naohBalao.GetComponent<MeshRenderer>().enabled = true;
                                for (int i = 0; i < solucaoBalanca.transform.childCount; i++)
                                {
                                    solucaoBalanca.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
                                    solutoFinded = false;
                                }
                                solucaoBalanca.GetComponent<Outline>().enabled = false;
                                balaoDeMistura.GetComponent<ObjectType>().objectType.itemText = "Pressione (E) para adicionar o solvente (Àgua)";
                            }else {
                                hit.transform.GetComponent<ObjectType>().objectType.itemText = "Precisa-se antes separar 10mL de NaOH 0,5M";
                            }
                        } 
                    }
                }

            }
        }
    }
    


    // Funções Auxiliares
    void VerificarSubstancia(string subs, string finded){
        for(int i = 0; i < iController.slots.Length; i++)
            {
                if(iController.slots[i] != null && iController.slots[i].name == subs)
                {
                    // fazer a mudança de cor, apagar o item do inventario e passar para a proxima 
                    if(finded == "substancia")
                    {
                        substanciaFinded = true;
                    } else if (finded == "solvente") {
                        solventeFinded = true;
                    }else if (finded == "jaleco") {
                        jaleco = true;
                    }
                }
            }
    }

    void UsarSubstancia(string subs){
        // fire.GetComponent<ObjectType>().objectType.itemText = "Pressione (E) para queimar "+ substancia[j] +" ou (Q) para mudar";
        // remover substancia
        GameObject obj = GameObject.Find(subs);
        if(subs != "Becker com Água")
        {
            obj.GetComponent<MeshRenderer>().enabled = true;
            obj.GetComponent<BoxCollider>().enabled = true;
            for (int z = 0; z < obj.transform.childCount; z++)
            {
                obj.transform.GetChild(z).GetComponent<MeshRenderer>().enabled = true;
                //Do something with child
            }
        }
        for(int i = 0; i < iController.slots.Length; i++){
            if(iController.slots[i] != null && iController.slots[i].name == subs){
                iController.slots[i] = null;
                iController.slotImage[i].sprite = null;
                iController.slotImage[i].color = new Color(0.25f,0.25f,0.25f);
            }
        }
        // itemFinded = false;
    }
}
