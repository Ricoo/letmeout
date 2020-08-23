using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject textBox;
    public GameObject portrait;
    public GameObject frame;
    private Dialogue runningDialogue;
    private int currentLine;
    private float elapsedTime;
    private Action callback = null;
    void Start()
    {
        runningDialogue = null;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (Input.GetKey("space") && runningDialogue != null) {
            nextLine();
        }
        
    }

    public void runDialogue(string id, Action cb) {
        foreach (Dialogue d in dialogues) {
            if (d.id == id) {
                callback = cb;
                runningDialogue = d;
                currentLine = 0;
                frame.SetActive(true);
                printLine();
                return ;
            }
        }
        throw(new KeyNotFoundException("Dialogue not found !")) ; 
    }

    public void nextLine() {
        if (elapsedTime < 1) {
            return;
        }
        currentLine += 1;

        if (currentLine == runningDialogue.interventions.Count) {
            runningDialogue = null;
            callback();
            frame.SetActive(false);
            return;
        }
        printLine();
    }

    public void printLine() {
        textBox.GetComponent<Text>().text = runningDialogue.interventions[currentLine].line;
        portrait.GetComponent<SpriteRenderer>().sprite = runningDialogue.interventions[currentLine].portrait;
        portrait.transform.GetComponent<SpriteRenderer>().material.color = setAlpha(portrait.transform.GetComponent<SpriteRenderer>().material.color, 0);
        StartCoroutine(FadeTo(portrait, 1.0f, 1.0f));
        elapsedTime = 0;
    }

    public Dialogue getRunningDialogue() {
        return this.runningDialogue;
    }

    public Color setAlpha(Color c, float alpha) {
        return new Color(c.r, c.g, c.b, c.a);
    }
    IEnumerator FadeTo(GameObject go, float aValue, float aTime) {
      float alpha = go.transform.GetComponent<SpriteRenderer>().material.color.a;
      for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
      {
          Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
          go.transform.GetComponent<SpriteRenderer>().material.color = newColor;
          yield return null;
      }
    }
}
