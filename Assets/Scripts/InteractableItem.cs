using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public GameObject io;
    private Interaction interaction;
    
    private GameObject player;
    private PlayerController pc;
    private InventoryController ic;
    private SpriteRenderer sRenderer;
    private float alpha = 0f;
    private float alphaCap = .8f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();
        ic = GameObject.Find("Inventory").GetComponent<InventoryController>();
        sRenderer = GetComponent<SpriteRenderer>();
        Color cl = sRenderer.material.GetColor("_SolidOutline");
        cl.a = 0f;
        sRenderer.material.SetColor("_SolidOutline", cl);
        interaction=io.GetComponent<Interaction>();
    }

    // Update is called once per frame
    void Update()
    {
        Color cl = sRenderer.material.GetColor("_SolidOutline");
        // Checks if the player is in interaction range
        if (Vector3.Distance(this.transform.position, player.transform.position) < .3f && (!interaction.used || interaction.repeat)) {
            if (alpha < alphaCap) {
                alpha += 2*Time.deltaTime;
                alpha = (alpha > alphaCap ? alphaCap : alpha);
            }
            // The player interacts with the item
            if (Input.GetKey("e") && !pc.interacting) {
                interaction.interact(this.gameObject);
            }
        } else if (alpha > 0f) {
            alpha -= 2*Time.deltaTime;
            alpha = (alpha < 0f ? 0 : alpha);
        }
        cl.a = alpha;
        sRenderer.material.SetColor("_SolidOutline", cl);
    }
}