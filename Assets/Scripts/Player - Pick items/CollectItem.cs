using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    // ระยะทางที่ Raycast จะตรวจจับ
    public float raycastDistance = 5f;
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;
    //public DemoScript demoScript;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                // //bin
                if(hit.collider.CompareTag("GreenBin"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Item receivedItem = inventoryManager.ThrowItem(true);
                    inventoryManager.CheckTypes(1);
                }
                if(hit.collider.CompareTag("BlueBin"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Item receivedItem = inventoryManager.ThrowItem(true);
                    inventoryManager.CheckTypes(2);
                }
                if(hit.collider.CompareTag("YellowBin"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Item receivedItem = inventoryManager.ThrowItem(true);
                    inventoryManager.CheckTypes(3);
                }
                if(hit.collider.CompareTag("RedBin"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Item receivedItem = inventoryManager.ThrowItem(true);
                    inventoryManager.CheckTypes(4);
                }

                // //object
                if(hit.collider.CompareTag("BlueCube"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[0]);
                    inventoryManager.AddItem(itemsToPickup[0]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("RedCube"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[1]);
                    inventoryManager.AddItem(itemsToPickup[1]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("GreenCube"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[2]);
                    inventoryManager.AddItem(itemsToPickup[2]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("YellowCube"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[3]);
                    inventoryManager.AddItem(itemsToPickup[3]);
                    inventoryManager.ItemUpdate();
                }
                // if(hit.collider.CompareTag("burger"))
                // {
                //     Debug.Log("Hit " + hit.collider.name);
                //     Destroy(hit.collider.gameObject);
                //     Debug.Log(itemsToPickup[4]);
                //     inventoryManager.AddItem(itemsToPickup[4]);
                //     inventoryManager.ItemUpdate();
                // }
                // if(hit.collider.CompareTag("donut"))
                // {
                //     Debug.Log("Hit " + hit.collider.name);
                //     Destroy(hit.collider.gameObject);
                //     Debug.Log(itemsToPickup[5]);
                //     inventoryManager.AddItem(itemsToPickup[5]);
                //     inventoryManager.ItemUpdate();
                // }
                // if(hit.collider.CompareTag("fish"))
                // {
                //     Debug.Log("Hit " + hit.collider.name);
                //     Destroy(hit.collider.gameObject);
                //     Debug.Log(itemsToPickup[6]);
                //     inventoryManager.AddItem(itemsToPickup[6]);
                //     inventoryManager.ItemUpdate();
                // }
                // if(hit.collider.CompareTag("bottle"))
                // {
                //     Debug.Log("Hit " + hit.collider.name);
                //     Destroy(hit.collider.gameObject);
                //     Debug.Log(itemsToPickup[7]);
                //     inventoryManager.AddItem(itemsToPickup[7]);
                //     inventoryManager.ItemUpdate();
                // }
                // if(hit.collider.CompareTag("barrelgreen"))
                // {
                //     Debug.Log("Hit " + hit.collider.name);
                //     Destroy(hit.collider.gameObject);
                //     Debug.Log(itemsToPickup[8]);
                //     inventoryManager.AddItem(itemsToPickup[8]);
                //     inventoryManager.ItemUpdate();
                // }
                // if(hit.collider.CompareTag("barrelgrey"))
                // {
                //     Debug.Log("Hit " + hit.collider.name);
                //     Destroy(hit.collider.gameObject);
                //     Debug.Log(itemsToPickup[9]);
                //     inventoryManager.AddItem(itemsToPickup[9]);
                //     inventoryManager.ItemUpdate();
                // }
                // if(hit.collider.CompareTag("barrelorange"))
                // {
                //     Debug.Log("Hit " + hit.collider.name);
                //     Destroy(hit.collider.gameObject);
                //     Debug.Log(itemsToPickup[10]);
                //     inventoryManager.AddItem(itemsToPickup[10]);
                //     inventoryManager.ItemUpdate();
                // }
                // if(hit.collider.CompareTag("cokecan"))
                // {
                //     Debug.Log("Hit " + hit.collider.name);
                //     Destroy(hit.collider.gameObject);
                //     Debug.Log(itemsToPickup[11]);
                //     inventoryManager.AddItem(itemsToPickup[11]);
                //     inventoryManager.ItemUpdate();
                // }
                // if(hit.collider.CompareTag("paper1"))
                // {
                //     Debug.Log("Hit " + hit.collider.name);
                //     Destroy(hit.collider.gameObject);
                //     Debug.Log(itemsToPickup[12]);
                //     inventoryManager.AddItem(itemsToPickup[12]);
                //     inventoryManager.ItemUpdate();
                // }
                // if(hit.collider.CompareTag("paper2"))
                // {
                //     Debug.Log("Hit " + hit.collider.name);
                //     Destroy(hit.collider.gameObject);
                //     Debug.Log(itemsToPickup[13]);
                //     inventoryManager.AddItem(itemsToPickup[13]);
                //     inventoryManager.ItemUpdate();
                // }
                // if(hit.collider.CompareTag("paper3"))
                // {
                //     Debug.Log("Hit " + hit.collider.name);
                //     Destroy(hit.collider.gameObject);
                //     Debug.Log(itemsToPickup[14]);
                //     inventoryManager.AddItem(itemsToPickup[14]);
                //     inventoryManager.ItemUpdate();
                // }
            }
        }
    }
}
