using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Rigidbody2D playerRb;
    public DialogueController dc;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        dc = GameObject.Find("DialogueObject").GetComponent<DialogueController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Time.deltaTime * Input.GetAxis("Horizontal");
        float vert = Time.deltaTime * Input.GetAxis("Vertical");
     
        playerRb.position = playerRb.position + new Vector2(moveSpeed * horiz, moveSpeed * vert);
    }

    public void dialogue(string dialogueId) {
        dc.runDialogue(dialogueId);
    }
}
