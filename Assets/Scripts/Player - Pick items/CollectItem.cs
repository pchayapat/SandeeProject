using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    // ระยะทางที่ Raycast จะตรวจจับ
    public float raycastDistance = 5f;
    public InventoryManager inventoryManager;
    public MinigameCore minigameCore;
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
                //Mission Trigger
                if(hit.collider.CompareTag("Mission1"))
                {
                    minigameCore.Mission1Start();
                }
                if(hit.collider.CompareTag("Mission2"))
                {
                    minigameCore.Mission2Start();
                }
                if(hit.collider.CompareTag("Mission3"))
                {
                    minigameCore.Mission3Start();
                }
                //bin
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

                //object
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
                if(hit.collider.CompareTag("Spray3"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[4]);
                    inventoryManager.AddItem(itemsToPickup[4]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Spray2"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[5]);
                    inventoryManager.AddItem(itemsToPickup[5]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Spray"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[6]);
                    inventoryManager.AddItem(itemsToPickup[6]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Batt"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[7]);
                    inventoryManager.AddItem(itemsToPickup[7]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Clean"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[8]);
                    inventoryManager.AddItem(itemsToPickup[8]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Clean2"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[9]);
                    inventoryManager.AddItem(itemsToPickup[9]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Banana"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[10]);
                    inventoryManager.AddItem(itemsToPickup[10]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Apple"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[11]);
                    inventoryManager.AddItem(itemsToPickup[11]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Hamburger"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[12]);
                    inventoryManager.AddItem(itemsToPickup[12]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Sandwich"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[13]);
                    inventoryManager.AddItem(itemsToPickup[13]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Fish"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[14]);
                    inventoryManager.AddItem(itemsToPickup[14]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Pizza"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[15]);
                    inventoryManager.AddItem(itemsToPickup[15]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Snack"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[16]);
                    inventoryManager.AddItem(itemsToPickup[16]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Cup"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[17]);
                    inventoryManager.AddItem(itemsToPickup[17]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Juice"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[18]);
                    inventoryManager.AddItem(itemsToPickup[18]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Milk"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[19]);
                    inventoryManager.AddItem(itemsToPickup[19]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Cereal"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[20]);
                    inventoryManager.AddItem(itemsToPickup[20]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Newspaper"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[21]);
                    inventoryManager.AddItem(itemsToPickup[21]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Bottle"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[22]);
                    inventoryManager.AddItem(itemsToPickup[22]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Can"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[23]);
                    inventoryManager.AddItem(itemsToPickup[23]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Can2"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[24]);
                    inventoryManager.AddItem(itemsToPickup[24]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Can3"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[25]);
                    inventoryManager.AddItem(itemsToPickup[25]);
                    inventoryManager.ItemUpdate();
                }
                if(hit.collider.CompareTag("Egg"))
                {
                    Debug.Log("Hit " + hit.collider.name);
                    Destroy(hit.collider.gameObject);
                    Debug.Log(itemsToPickup[26]);
                    inventoryManager.AddItem(itemsToPickup[26]);
                    inventoryManager.ItemUpdate();
                }

            }
        }
    }
}
