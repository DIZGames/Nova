using Assets.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SlotContainerCreator))]
public class SlotContainerCreatorEditor : Editor {

    private int itemIndex;
    private int itemValue = 1;

    private SlotContainerCreator slScript;

    void OnEnable() {
        slScript = (SlotContainerCreator)target;
    }

    public override void OnInspectorGUI() {

        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();

        slScript.slotList = (Transform)EditorGUILayout.ObjectField("SlotList", slScript.slotList, typeof(Transform), true);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Add an item:");
        ItemList itemDataBase = (ItemList)Resources.Load("ItemDataBase");

        string[] items = new string[itemDataBase.Count()];

        for (int i = 0; i < items.Length; i++) {
            items[i] = itemDataBase.ItemByIndex(i).itemName;
        }

        itemIndex = EditorGUILayout.Popup("", itemIndex, items, EditorStyles.popup);
        itemValue = EditorGUILayout.IntField("", itemValue, GUILayout.Width(40));
        GUI.color = Color.yellow;
        if (GUILayout.Button("Add Item")) {
            ItemBase item = itemDataBase.ItemByIndex(itemIndex);

            slScript.CreateSlotContainer(item, itemValue);

        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Alles löschen")) {

            slScript.DeleteAll();

        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        

    }

}

