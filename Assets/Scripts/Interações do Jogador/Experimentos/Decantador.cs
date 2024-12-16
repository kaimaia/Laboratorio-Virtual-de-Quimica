using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decantador : MonoBehaviour
{
    public GameObject agua;
    public GameObject oleo;
    public GameObject aguadecantada;
    // bool inActive = false;
    bool itemFinded = false;
    private string[] substancia = {"Mistura de Água e Óleo"};
    private int j = 0;
    private InventoryPanel iController;
    bool jaleco = false;

    // Start is called before the first frame update
    void Start()
    {
        iController = FindObjectOfType<Canvas>().GetComponent<InventoryPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        VerificarJaleco();
        if(jaleco != true){
            return;
        }
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
        // mecanica fogão 
        if(Physics.Raycast(ray, out hit, 90f)){
            if(hit.collider.tag == "decantador")
            {
                // verificar se existe substancia no inventário, fazer a mudança de cor, e passar para a próxima substância
                if(itemFinded == true){
                    hit.transform.GetComponent<ObjectType>().objectType.itemText = "Pressione (E) para decantar a "+ substancia[j];
                } else {
                    hit.transform.GetComponent<ObjectType>().objectType.itemText = "Precisa-se da Mistura de Água e Óleo";
                }
                VerificarSubstancia();
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if(itemFinded == false){
                        hit.transform.GetComponent<ObjectType>().objectType.itemText = "Você ainda não pegou a substância!";
                        itemFinded = false;
                    } else {
                        UsarSubstancia();
                        agua.GetComponent<MeshRenderer>().enabled = true;
                        oleo.GetComponent<MeshRenderer>().enabled = true;
                        aguadecantada.GetComponent<MeshRenderer>().enabled = false;
                        hit.collider.tag = "decantadorcommistura";
                        // inActive =! inActive;
                        // aguadecantada.SetActive(!inActive);
                    }
                }
            } else if(hit.collider.tag == "decantadorcommistura"){

                hit.transform.GetComponent<ObjectType>().objectType.itemText = "Pressione (E) novamente para decantar a "+ substancia[j];
                if(Input.GetKeyDown(KeyCode.E)){
                    Color oleoColor = new Color(0.87f, 0.9f, 0f, 0.24f);
                    agua.GetComponent<Renderer>().material.SetColor("_Color", oleoColor);
                    oleo.GetComponent<MeshRenderer>().enabled = false;
                    aguadecantada.GetComponent<MeshRenderer>().enabled = true;
                    aguadecantada.GetComponent<BoxCollider>().enabled = true;
                    for (int z = 0; z < aguadecantada.transform.childCount; z++)
                    {
                        aguadecantada.transform.GetChild(z).GetComponent<MeshRenderer>().enabled = true;
                    }
                    hit.collider.tag = "decantadorcomoleo";
                    hit.transform.GetComponent<ObjectType>().objectType.itemText = "Pressione (E) para limpar o decantador";
                }
            } else if(hit.collider.tag == "decantadorcomoleo")
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if(aguadecantada.GetComponent<MeshRenderer>().enabled == false)
                    {
                        Color aguaColor = new Color(0.05f, 0.12f, 0.76f, 0.334f);
                        agua.GetComponent<Renderer>().material.SetColor("_Color", aguaColor);
                        agua.GetComponent<MeshRenderer>().enabled = false;
                        hit.collider.tag = "decantador";
                    } else {
                    hit.transform.GetComponent<ObjectType>().objectType.itemText = "Retire a água do Becker antes";
                    }
                }
            }
        }

    }


    
    // Funções auxiliares 

    void VerificarSubstancia(){
        
        for(int i = 0; i < iController.slots.Length; i++)
            {
                if(iController.slots[i] != null && iController.slots[i].name == substancia[j])
                {
                    // fazer a mudança de cor, apagar o item do inventario e passar para a proxima 
                    itemFinded = true;
                }
            }
    }
    void VerificarJaleco(){
        
        for(int i = 0; i < iController.slots.Length; i++)
            {
                if(iController.slots[i] != null && iController.slots[i].name == "Jaleco")
                {
                    // fazer a mudança de cor, apagar o item do inventario e passar para a proxima 
                    jaleco = true;
                }
            }
    }
    void UsarSubstancia(){
        // fire.GetComponent<ObjectType>().objectType.itemText = "Pressione (E) para queimar "+ substancia[j] +" ou (Q) para mudar";
        // remover substancia
        GameObject obj = GameObject.Find(substancia[j]);
        obj.GetComponent<MeshRenderer>().enabled = true;
        obj.GetComponent<BoxCollider>().enabled = true;
        for (int z = 0; z < obj.transform.childCount; z++)
        {
            obj.transform.GetChild(z).GetComponent<MeshRenderer>().enabled = true;
            //Do something with child
        }
        for(int i = 0; i < iController.slots.Length; i++){
            if(iController.slots[i] != null && iController.slots[i].name == substancia[j]){
                iController.slots[i] = null;
                iController.slotImage[i].sprite = null;
                iController.slotImage[i].color = new Color(0.25f,0.25f,0.25f);
            }
        }
        itemFinded = false;
    }
}
