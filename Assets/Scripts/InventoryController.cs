using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InventoryController : MonoBehaviour
{
    [System.Serializable]
    public struct Item {
        public Sprite sprite;
        public string name;
        public bool owned;
        public bool keep;
    }

    // [SerializeField]
    public List<Item> inventory = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
    }

    public bool addItem(string itemName) {
        for (int i = 0; i < inventory.Count; i++) {
            Item item = inventory[i];
            Debug.Log(item.name);
            if (item.name == itemName && !item.owned) {
                item.owned = true;
                inventory[i] = item;
                return true;
            }
        }
        return false;
    }

    public bool hasItem(string itemName, bool use=false) {
        for (int i = 0; i < inventory.Count; i++) {
            Item item = inventory[i];
            if (item.name == itemName && item.owned) {
                if (!item.keep && use) {
                    item.owned = false;
                }
                return true;
            }
        }
        return false;
    }
}
