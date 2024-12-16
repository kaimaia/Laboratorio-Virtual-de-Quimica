using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Text itemText;
    bool invActive;
    // Start is called before the first frame update
    void Start()
    {
        itemText.text = null;
        invActive = true;
        invActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            Cursor.visible = false;
            invActive =! invActive;
            inventoryPanel.SetActive(invActive);
        }
        if(invActive){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
