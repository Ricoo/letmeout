using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

[System.Serializable]
[CustomEditor(typeof(Interaction))]
public class InteractionInspector : Editor
{
    public override void OnInspectorGUI() {
        Interaction script = (Interaction)target;
        script.target = (GameObject) EditorGUILayout.ObjectField("Target", script.parent, typeof(GameObject), true);

        script.t = (Interaction.Type)EditorGUILayout.EnumPopup("Interaction type", script.t);
        script.used = EditorGUILayout.Toggle("interacted (debug)", script.used);
        script.repeat = EditorGUILayout.Toggle("repeatable", script.repeat);

        GameObject player = GameObject.Find("Player");
        InventoryController ic = (InventoryController) GameObject.Find("Inventory").GetComponent(typeof(InventoryController));
        DialogueController dc = (DialogueController) GameObject.Find("DialogueObject").GetComponent(typeof(DialogueController));
        int value = 0;
        switch (script.t) {
            case Interaction.Type.Pickup:
                value = generatePopupField<InventoryController.Item, Interaction>(ic.inventory, "name", script, "item", "Item");
                break;
            case Interaction.Type.Dialogue:
                generatePopupField<DialogueController.Dialogue, Interaction>(dc.dialogues, "id", script, "dialogueId", "Dialogue ID");
                break;
            case Interaction.Type.Requires:
                generatePopupField<InventoryController.Item, Interaction>(ic.inventory, "name", script, "item", "Required item");
                script.go = (GameObject) EditorGUILayout.ObjectField("Interaction ok", script.go, typeof(GameObject), true);
                script.nok = (GameObject) EditorGUILayout.ObjectField("Interaction not ok", script.nok, typeof(GameObject), true);
                break;
            case Interaction.Type.GameObjectToggle:
                script.go = (GameObject) EditorGUILayout.ObjectField("Game Object", script.go, typeof(GameObject), true);
                break;
            case Interaction.Type.UpdateSprite:
                script.altSprite = (Sprite) EditorGUILayout.ObjectField("Updated Sprite", script.altSprite, typeof(Sprite), true);
                break;
            default:
                break;
        }
        if (GUI.changed) {
            if (script.t == Interaction.Type.Pickup) {
                if (script.target != null) {
                    script.target.GetComponent<SpriteRenderer>().sprite = ic.inventory[value].sprite;
                }
            }
            EditorUtility.SetDirty(target);
        }
    }

    // in editor, generates a <select> item to choose from the items of given {list}, which is defined by its id {field}
    int generatePopupField<T, U>(List<T> list, string field, U script, string scriptField, string prompt) {
        int retval = 0;
        string[] items = new string[list.Count];
        string curValue = (string) typeof(U).GetField(scriptField).GetValue(script);
        FieldInfo fi = typeof(T).GetField(field);
        for (int i = 0; i < list.Count; i++) {
            items[i] = (string) fi.GetValue(list[i]);
            if (items[i] == curValue) {
                retval = i;
            }
        }
        retval = EditorGUILayout.Popup(prompt, retval, items);
        typeof(U).GetField(scriptField).SetValue(script, items[retval]);
        return retval;
    }
}
