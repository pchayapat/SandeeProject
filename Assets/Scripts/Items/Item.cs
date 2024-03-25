using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject {
    [Header("Only gameplay")]
    public ItemType type;
    [Header("Only UI")]
    public bool stackable = true;
    [Header("Both")]
    public Sprite image;
}

public enum ItemType {
    Plastic,
    Electronic,
    Food
}
