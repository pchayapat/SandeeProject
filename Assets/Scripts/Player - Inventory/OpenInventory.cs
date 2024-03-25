using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    [Header("Inventory Screen")]
    public GameObject Inventory; // Corrected the syntax
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.E;
    [Header("Hide Crosshair")]
    public GameObject crosshair;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Corrected the method name
        {
            if(Inventory.gameObject.activeSelf){
                Inventory.SetActive(false);
                crosshair.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else{
                Inventory.SetActive(true);
                crosshair.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
