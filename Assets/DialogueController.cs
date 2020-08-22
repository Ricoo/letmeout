using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class DialogueController : MonoBehaviour
{
    [System.Serializable]
    public struct Intervention {
        public Sprite portrait;
         [TextArea(3,10)]
        public string line;
    }

    [System.Serializable]
    public class Dialogue {
        public string id;
        [SerializeField]
        public List<Intervention> interventions = new List<Intervention>();
    }

    public List<Dialogue> dialogues = new List<Dialogue>();
    // Start is called before the first frame update

    private Dialogue runningDialogue;
    void Start()
    {
        runningDialogue = null;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void runDialogue(string id) {
        foreach (Dialogue d in dialogues) {
            if (d.id == id) {
                runningDialogue = d;
                return ;
            }
        }
        throw(new KeyNotFoundException("Dialogue not found !")) ; 
    }
}
