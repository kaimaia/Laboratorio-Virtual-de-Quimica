using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    public Objetos[] slots;
    public Image[] slotImage;
    // public int[] slotAmount;
    OutlineObject outlineObject;
    private Inventario iController;
    bool jaleco = false;   


    // Start is called before the first frame update


    void Start()
    {
        iController = FindObjectOfType<Inventario>();
        iController.itemText.text = "Pegue o jaleco para fazer experimentos";
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
        if(Physics.Raycast(ray, out hit, 90f))
        {   
            if(outlineObject){
                if(outlineObject.transform != hit.transform && jaleco == false){
                    iController.itemText.text = "Pegue o jaleco para fazer experimentos";
                    outlineObject.Deselect();
                } else if(outlineObject.transform != hit.transform){
                    iController.itemText.text = null;
                    outlineObject.Deselect();
                }
            }
            outlineObject = hit.transform.GetComponent<OutlineObject>();
            if(jaleco == true || jaleco == false && hit.collider.tag == "jaleco")
            {
                    if(outlineObject)
                {
                    outlineObject.Select();
                    if(hit.transform.GetComponent<ObjectType>()){
                        iController.itemText.text = hit.transform.GetComponent<ObjectType>().objectType.itemText;  
                    }
                }
                if(hit.collider.tag == "Object" ||hit.collider.tag == "jaleco")
                {
                    if(Input.GetKeyDown(KeyCode.E))
                    {   
                        for(int i = 0; i < slots.Length; i++)
                        {
                            if(slots[i] == null || slots[i].name == hit.transform.GetComponent<ObjectType>().objectType.name)
                            {
                                slots[i] = hit.transform.GetComponent<ObjectType>().objectType;
                                // slotAmount[i]++;
                                slotImage[i].sprite = hit.transform.GetComponent<ObjectType>().objectType.itemSprite;
                                slotImage[i].color = Color.white;
                                StopCoroutine("Deselect");
                                hit.transform.GetComponent<BoxCollider>().enabled = false;
                                if(hit.transform.GetComponent<MeshRenderer>()){
                                    hit.transform.GetComponent<MeshRenderer>().enabled = false;
                                }
                                for (int z = 0; z < hit.transform.childCount; z++)
                                {
                                    hit.transform.GetChild(z).GetComponent<MeshRenderer>().enabled = false;
                                    //Do something with child
                                }
                                iController.itemText.text = null;
                                jaleco = true;
                                break;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if(outlineObject && jaleco == true){
                iController.itemText.text = null;
                outlineObject.Deselect();
            }
            outlineObject = null;
        }
    }
    // IEnumerator StartFire2(GameObject fire, Color color){
    //     fire.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
    //     fire.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    //     yield return new WaitForSeconds(2);
    // }
}
