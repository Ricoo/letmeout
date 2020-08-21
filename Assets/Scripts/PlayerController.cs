using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Time.deltaTime * Input.GetAxis("Horizontal");
        float vert = Time.deltaTime * Input.GetAxis("Vertical");
        transform.position = transform.position + new Vector3(moveSpeed * horiz, moveSpeed * vert, 0);
    }
}
