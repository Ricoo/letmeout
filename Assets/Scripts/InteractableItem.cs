using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public enum InteractionType {
        Dialogue,
        Pickup,
        Requires,
        GameObjectPopup,
        UpdateSprite
    }

    [SerializeField]
    public InteractionType interaction;
    private bool interacted = false;
    public bool interactionRepeatable = false;
    
    // case pickup
    public string itemName;

    //case dialogue
    public string dialogueId;
    public string dialogueId2;
    private GameObject player;
    private InventoryController invController;
    private SpriteRenderer sRenderer;
    private float alpha = 0f;
    private float alphaCap = .8f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        invController = GameObject.Find("Inventory").GetComponent<InventoryController>();
        sRenderer = GetComponent<SpriteRenderer>();
        Color cl = sRenderer.material.GetColor("_SolidOutline");
        cl.a = 0f;
        sRenderer.material.SetColor("_SolidOutline", cl);
    }

    // Update is called once per frame
    void Update()
    {
        Color cl = sRenderer.material.GetColor("_SolidOutline");
        // Checks if the player is in interaction range
        if (Vector3.Distance(this.transform.position, player.transform.position) < .3f && (!interacted || interactionRepeatable)) {
            if (alpha < alphaCap) {
                alpha += 2*Time.deltaTime;
                alpha = (alpha > alphaCap ? alphaCap : alpha);
            }
            // The player interacts with the item
            if (Input.GetKey("e")) {
                interacted = true;
                switch (interaction) {
                    case InteractionType.Pickup:
                        if (!invController.hasItem(itemName)) {
                            invController.addItem(itemName);
                            sRenderer.enabled = false;
                        }
                        break;
                    case InteractionType.Dialogue:
                        Debug.Log("HELLO THERE");
                        player.GetComponent<PlayerController>().dialogue(dialogueId);
                        break;
                    case InteractionType.Requires:
                        break;
                    case InteractionType.GameObjectPopup:
                        break;
                }
                if (interaction == InteractionType.Pickup) {

                } else {

                }
            }
        } else if (alpha > 0f) {
            alpha -= 2*Time.deltaTime;
            alpha = (alpha < 0f ? 0 : alpha);
        }
        cl.a = alpha;
        sRenderer.material.SetColor("_SolidOutline", cl);
    }
}