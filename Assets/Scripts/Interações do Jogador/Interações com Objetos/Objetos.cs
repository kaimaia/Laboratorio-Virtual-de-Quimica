using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "New Object", menuName = "Inventory Objects/Create New")]

public class Objetos : ScriptableObject
{
    public string itemName;
    public string itemText;
    public Sprite itemSprite;
}
