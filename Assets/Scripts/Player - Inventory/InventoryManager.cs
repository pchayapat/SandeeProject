using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    int selectedSlot = -1;
    int numSlot = 0;
    public GameObject[] itemsPrefabs;
    public Transform handPosition;

    private void Start(){
        ChangeSelectedSlot(0);
    }

    private void Update(){
        if(Input.inputString != null){
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if(isNumber && number > 0 && number < 9){
                numSlot = number - 1;
                ChangeSelectedSlot(numSlot);
            }
        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0f ){
            if(numSlot > 0){
                numSlot -= 1;
                ChangeSelectedSlot(numSlot);
            }
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0f ){
            if(numSlot < 7){
                numSlot += 1;
                ChangeSelectedSlot(numSlot);
            }
        }
    }
    void ChangeSelectedSlot(int newValue){
        if(selectedSlot >=0){
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        //P
        Transform targetTransform = inventorySlots[newValue].transform;
        Debug.Log(targetTransform.childCount);
        if (targetTransform.childCount > 0)
        {
            foreach (Transform child in targetTransform)
            {   
                Image imageComponent = child.GetComponent<Image>();
                
                if (imageComponent != null)
                {
                    string spriteName = imageComponent.sprite.name.ToLower();

                    switch (spriteName)
                    {
                        case "bluecube":
                            InstantiateAndDestroy(itemsPrefabs[0], handPosition);
                            break;      
                        case "redcube":
                            InstantiateAndDestroy(itemsPrefabs[1], handPosition);
                            break;
                        case "greencube":
                            InstantiateAndDestroy(itemsPrefabs[2], handPosition);
                            break;
                        case "yellowcube":
                            InstantiateAndDestroy(itemsPrefabs[3], handPosition);
                            break;
                    }
                }
            }
        }
        else if(targetTransform.childCount == 0)
        {
            foreach (Transform child in handPosition)
            {
                Destroy(child.gameObject);
            }
        }

        void InstantiateAndDestroy(GameObject prefab, Transform parent)
        {
            GameObject newItem = Instantiate(prefab, parent);
            Rigidbody newItemRigidbody = newItem.GetComponent<Rigidbody>();
            if (newItemRigidbody != null)
            {
                Destroy(newItemRigidbody);
            }
            if (parent.childCount > 1)
            {
                for (int i = 0; i < parent.childCount - 1; i++)
                {
                    Destroy(parent.GetChild(i).gameObject);
                }
            }
        }
        //P
        selectedSlot = newValue;
    }

    public bool AddItem(Item item) {
        for(int i =0; i < inventorySlots.Length; i++){
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null){
                SpawnNewItem(item,slot);
                return true;
            }
        }
        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot){
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem InventoryItem = newItemGo.GetComponent<InventoryItem>();
        InventoryItem.InitialiseItem(item);
    }

    public Item ThrowItem(bool throwItem){
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if(itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if(throwItem == true)
            {
                
                Destroy(itemInSlot.gameObject);

                foreach (Transform child in handPosition)
                {
                    Destroy(child.gameObject);
                }
            }
            return item;
        }
        return null;
    }
    public void CheckTypes(int BinType){
        foreach (Transform child in handPosition)
        {
            if(child.gameObject.CompareTag("BlueCube") && BinType == 2){
                Debug.Log("True");
                ScoreManager.scoreCount += 1;
            }
            else if(child.gameObject.CompareTag("RedCube") && BinType == 4){
                Debug.Log("True");
                ScoreManager.scoreCount += 1;
            }
            else if(child.gameObject.CompareTag("GreenCube") && BinType == 1){
                Debug.Log("True");
                ScoreManager.scoreCount += 1;
            }
            else if(child.gameObject.CompareTag("YellowCube") && BinType == 3){
                Debug.Log("True");
                ScoreManager.scoreCount += 1;
            }
            else
            {
                Debug.Log("False");
                Missing.MissingCount -= 1;
                //ScoreManager.scoreCount -= 1;
            }
        }
    }
    public void ItemUpdate()
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        int newValue = numSlot;
        if(itemInSlot != null)
        {
            Transform targetTransform = inventorySlots[newValue].transform;
            Debug.Log(targetTransform.childCount);
            if (targetTransform.childCount > 0)
            {
                foreach (Transform child in targetTransform)
                {   
                    Image imageComponent = child.GetComponent<Image>();
                    
                    if (imageComponent != null)
                    {
                        string spriteName = imageComponent.sprite.name.ToLower();

                        switch (spriteName)
                        {
                            case "bluecube":
                                InstantiateAndDestroy(itemsPrefabs[0], handPosition);
                                break;
                            case "redcube":
                                InstantiateAndDestroy(itemsPrefabs[1], handPosition);
                                break;
                            case "greencube":
                                InstantiateAndDestroy(itemsPrefabs[2], handPosition);
                                break;
                            case "yellowcube":
                                InstantiateAndDestroy(itemsPrefabs[3], handPosition);
                                break;
                            case "burger":
                                InstantiateAndDestroy(itemsPrefabs[4], handPosition);
                                break;
                            case "donut":
                                InstantiateAndDestroy(itemsPrefabs[5], handPosition);
                                break;
                            case "fish":
                                InstantiateAndDestroy(itemsPrefabs[6], handPosition);
                                break;
                            case "bottle":
                                InstantiateAndDestroy(itemsPrefabs[7], handPosition);
                                break;
                            case "barrelgreen":
                                InstantiateAndDestroy(itemsPrefabs[8], handPosition);
                                break;
                            case "barrelgrey":
                                InstantiateAndDestroy(itemsPrefabs[9], handPosition);
                                break;
                            case "barrelorange":
                                InstantiateAndDestroy(itemsPrefabs[10], handPosition);
                                break;
                            case "cokecan":
                                InstantiateAndDestroy(itemsPrefabs[11], handPosition);
                                break;
                            case "paper1":
                                InstantiateAndDestroy(itemsPrefabs[12], handPosition);
                                break;
                            case "paper2":
                                InstantiateAndDestroy(itemsPrefabs[13], handPosition);
                                break;
                            case "paper3":
                                InstantiateAndDestroy(itemsPrefabs[14], handPosition);
                                break;
                        }
                    }
                }
            }
            else if(targetTransform.childCount == 0)
            {
                foreach (Transform child in handPosition)
                {
                    Destroy(child.gameObject);
                }
            }

            void InstantiateAndDestroy(GameObject prefab, Transform parent)
            {
                GameObject newItem = Instantiate(prefab, parent);
                Rigidbody newItemRigidbody = newItem.GetComponent<Rigidbody>();
                if (newItemRigidbody != null)
                {
                    Destroy(newItemRigidbody);
                }
                if (parent.childCount > 1)
                {
                    for (int i = 0; i < parent.childCount - 1; i++)
                    {
                        Destroy(parent.GetChild(i).gameObject);
                    }
                }
            }
        }
    }
}