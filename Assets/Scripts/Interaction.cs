using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    /** Type is the type of the interaction :
        - Dialogue is a simple dialogue with one or more characters
        - Pickup adds an item to the inventory
        - Requires checks if the player has a required item, and outputs one
          of two subsequent interactions
        - GameObjectToggle toggles a GameObject to be hidden or displayed
        - UpdateSprite updates the sprite of the interactable item
    */
    public enum Type {
        Dialogue,
        Pickup,
        Requires,
        GameObjectToggle,
        UpdateSprite
    }

    [SerializeField]
    public Type t;
    public string dialogueId;
    public GameObject go;
    public GameObject nok;
    public Sprite altSprite;
    public string item;


    public bool used = false;
    public bool repeat = false;
    private GameObject player;
    public GameObject parent;
    private PlayerController pc;
    private DialogueController dc;
    private InventoryController ic;
    public GameObject target;

    void Start()
    {
        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();
        dc = GameObject.Find("DialogueObject").GetComponent<DialogueController>();
        ic = GameObject.Find("Inventory").GetComponent<InventoryController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void interact(GameObject target) {
        // Depending on the interaction type, the actions to apply are different
        if (used) {
            pc.interacting = false;
            return;
        }
        pc.interacting = true;
        if (this.target == null) {
            this.target = target;
        } else {
            target = this.target;
        }
        switch (t) {
            case Type.Dialogue:
                dc.runDialogue(dialogueId, ()=>{
                    this.markAsDone();});
                break;
            case Type.Pickup:
                ic.addItem(item);
                target.SetActive(false);
                markAsDone();
                break;
            case Type.Requires:
                if (ic.hasItem(item, true)) {
                    markAsDone(go);
                } else {
                    markAsDone(nok);
                }
                break;
            case Type.GameObjectToggle:
                go.SetActive(!go.activeSelf);
                markAsDone();
                break;
            case Type.UpdateSprite:
                target.GetComponent<SpriteRenderer>().sprite = altSprite;
                markAsDone();
                break;
        }
    }

    // marks the interaction as accomplished, except if the interaction is on repeat
    // then executes the next interaction if there is one available
    public void markAsDone(GameObject next=null) {
        used = true;
        if(next == null) {
            Interaction[] list = GetComponentsInChildren<Interaction>();
            if (list.Length > 1) {
                list[1].interact(target);
            } else {
                player.GetComponent<PlayerController>().interacting = false;
            }
        } else {
            next.GetComponent<Interaction>().interact(target);
        }
        if (repeat) {
            used = false;
        } 

    }
}