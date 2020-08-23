using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Rigidbody2D playerRb;
    public DialogueController dc;
    public InventoryController ic;
    public bool interacting = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        dc = GameObject.Find("DialogueObject").GetComponent<DialogueController>();
        ic = GameObject.Find("Inventory").GetComponent<InventoryController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Time.deltaTime * Input.GetAxis("Horizontal");
        float vert = Time.deltaTime * Input.GetAxis("Vertical");
        if (!interacting) {
            playerRb.position = playerRb.position + new Vector2(moveSpeed * horiz, moveSpeed * vert);
        }
    }

    public void dialogue(string dialogueId) {
        interacting=true;
        dc.runDialogue(dialogueId, ()=>{this.interacting=false;});
    }
}
