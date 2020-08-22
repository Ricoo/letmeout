using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaController : MonoBehaviour
{
    public GameObject player;
    private float alpha = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null) {
            player = GameObject.Find("/Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < player.transform.position.y) {
            Color tmp = GetComponent<SpriteRenderer>().color;
            alpha -= 1f*Time.deltaTime;
            alpha = alpha < .2f ? .2f : alpha;
            tmp.a = alpha;
            GetComponent<SpriteRenderer>().color = tmp;
        }
        else if (alpha < 1f ){
            Color tmp = GetComponent<SpriteRenderer>().color;
            alpha += 1f*Time.deltaTime;
            alpha = alpha > 1f ? 1f : alpha;
            tmp.a = alpha;
            GetComponent<SpriteRenderer>().color = tmp;    
        }
    }
}
