using UnityEngine;

public class CheckChild: MonoBehaviour
{
    public GameObject[] gameObjectsToCheck;
    public bool fullSlot;

    private void Update()
    {
        bool allSlotsFull = true;

        foreach (GameObject go in gameObjectsToCheck)
        {
            if (go.transform.childCount == 0)
            {
                allSlotsFull = false;
                break;
            }
        }

        if (allSlotsFull)
        {
            Debug.Log("Full slot");
            fullSlot = true;
        }
        else
        {
            fullSlot = false;
        }
    }

    public void DestroyChildrenInSlots()
    {
        foreach (GameObject go in gameObjectsToCheck)
        {
            foreach (Transform child in go.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
