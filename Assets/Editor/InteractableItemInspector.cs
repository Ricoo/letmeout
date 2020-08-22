using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

[System.Serializable]
[CustomEditor(typeof(InteractableItem))]
public class InteractableItemInspector : Editor
{
    public override void OnInspectorGUI() {
        InteractableItem script = (InteractableItem)target;

        // script.interactions = (List<Interaction>)EditorGUILayout.PropertyField(script.interactions);
        script.interaction = (InteractableItem.InteractionType)EditorGUILayout.EnumPopup("Interaction type", script.interaction);


        switch (script.interaction) {
            case InteractableItem.InteractionType.Pickup:
                GameObject player = GameObject.Find("Player");
                InventoryController ic = (InventoryController) GameObject.Find("Inventory").GetComponent(typeof(InventoryController));
                int value = generatePopupField<InventoryController.Item, InteractableItem>(ic.inventory, "name", script, "itemName", "Item Name");
                script.gameObject.GetComponent<SpriteRenderer>().sprite = ic.inventory[value].sprite;
                break;
            case InteractableItem.InteractionType.Dialogue:
                DialogueController dc = (DialogueController) GameObject.Find("DialogueObject").GetComponent(typeof(DialogueController));
                generatePopupField<DialogueController.Dialogue, InteractableItem>(dc.dialogues, "id", script, "dialogueId", "Dialogue ID");
                break;
            default:
                break;
        }
        if (GUI.changed) {
            EditorUtility.SetDirty(target);
        }
    }

    int generatePopupField<T, U>(List<T> list, string field, U script, string scriptField, string prompt) {
        int retval = 0;
        string[] items = new string[list.Count];
        // Type itemType = typeof(T);
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
