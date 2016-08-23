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

    public override void OnInspectorGUI() {
        //target ist das Skript, auf das dieses hier verweist
        SlotContainerCreator slScript = (SlotContainerCreator)target;

        GUILayout.Label("Add an item:");
        // inv.setImportantVariables();                                                                                                            //space to the top gui element
        EditorGUILayout.BeginHorizontal();                                                                                  //starting horizontal GUI elements
        ItemList itemDataBase = (ItemList)Resources.Load("ItemDataBase");

        //ItemDataBaseList inventoryItemList = (ItemDatabase)Resources.Load("ItemDatabase");                            //loading the itemdatabase
        string[] items = new string[itemDataBase.Count()];


        //create a string array in length of the itemcount
        for (int i = 0; i < items.Length; i++)                                                                              //go through the item array
        {
            items[i] = itemDataBase.ItemByIndex(i).itemName;
        }


        itemIndex = EditorGUILayout.Popup("", itemIndex, items, EditorStyles.popup);                                              //create a popout with all itemnames in it and save the itemID of it
        itemValue = EditorGUILayout.IntField("", itemValue, GUILayout.Width(40));
        GUI.color = Color.yellow;                                                                                            //set the color of all following guielements to green
        if (GUILayout.Button("Add Item"))                                                                                   //creating button with name "AddItem"
        {
            ItemBase item = itemDataBase.ItemByIndex(itemIndex);

            slScript.CreateSlotContainer(item, itemValue);

        }
        EditorGUILayout.EndHorizontal();

    }

}

