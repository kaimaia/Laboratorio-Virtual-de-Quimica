using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentoDoFogo : MonoBehaviour
{
    public GameObject fogo;
    bool inActive = false;
    bool itemFinded = false;
    private string[] substancia = {"Cloreto de Sódio","Cloreto de Cobre","Magnésio","Cloreto de Estrôncio"};
    private int j = 0;
    private InventoryPanel iController;
    bool jaleco = false;

    // Start is called before the first frame update
    void Start()
    {
        fogo.SetActive(inActive);
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
            if(hit.collider.tag == "fogo")
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    fogo.SetActive(!inActive);
                    inActive =! inActive;
                }
            } else if(hit.collider.tag == "queimar")
            {
                // verificar se existe substancia no inventário, fazer a mudança de cor, e passar para a próxima substância
                if(itemFinded == true){
                    fogo.GetComponent<ObjectType>().objectType.itemText = "Pressione (E) para queimar "+ substancia[j] +" ou (Q) para mudar";
                }
                if(Input.GetKeyDown(KeyCode.Q)){
                    itemFinded = false;
                    if(j<3){
                        j++;
                    } else {
                        j = 0;
                    }
                    hit.transform.GetComponent<ObjectType>().objectType.itemText = "Pressione (E) para queimar "+ substancia[j] +" ou (Q) para mudar";
                    VerificarSubstancia(substancia[j]);
                }
                VerificarSubstancia(substancia[j]);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if(itemFinded == false){
                        hit.transform.GetComponent<ObjectType>().objectType.itemText = "Você ainda não pegou a substância!";
                        itemFinded = false;
                    } else {
                        if(substancia[j] == "Cloreto de Sódio"){
                            QueimarSubstancia(Color.black,Color.yellow,fogo);
                        } else if(substancia[j] == "Cloreto de Cobre"){
                            QueimarSubstancia(Color.green, Color.black, fogo);
                        } else if(substancia[j] == "Magnésio"){
                            QueimarSubstancia(Color.white, Color.black, fogo);
                        } else if(substancia[j] == "Cloreto de Estrôncio"){
                            QueimarSubstancia(Color.black, Color.red, fogo);
                        }
                    }
                }
            }
        }






    }

    // Funções auxiliares 

    void VerificarSubstancia(string subs){
        for(int i = 0; i < iController.slots.Length; i++)
            {
                if(iController.slots[i] != null && iController.slots[i].name == subs)
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
    void QueimarSubstancia(Color color, Color color2, GameObject fire){
        fire.GetComponent<ObjectType>().objectType.itemText = "Pressione (E) para queimar "+ substancia[j] +" ou (Q) para mudar";
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
        StopAllCoroutines();
        StartCoroutine(StartFire(fogo, color, color2));
        // obj, emissão, cor
        itemFinded = false;
    }


    // Courrotinas
    IEnumerator StartFire(GameObject fire, Color color, Color color2){
        Color baseColor = new Color(0.4f, 0.35f, 0.03f, 1f);
        Color baseEmission = new Color(0.05f, 0.1f, 0.64f);
        fire.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        fire.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        fire.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
        fire.GetComponent<Renderer>().material.SetColor("_Color", color2);
        yield return new WaitForSeconds(2);
        fire.GetComponent<Renderer>().material.SetColor("_Color", baseColor);
        fire.GetComponent<Renderer>().material.SetColor("_EmissionColor", baseEmission);
    }
}
