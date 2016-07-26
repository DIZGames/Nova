using UnityEngine;
using System.Collections;
using UnityEditor;
using Assets.Script;

[CustomEditor(typeof(SlotList))]
public class SlotListEditor : Editor {


    private int itemIndex;
    private int itemValue = 1;

    //public Transform gObject;

    public override void OnInspectorGUI() {
        //target ist das Skript, auf das dieses hier verweist
        SlotList slScript = (SlotList)target;


        //gObject = (Transform)EditorGUILayout.ObjectField(gObject, typeof(Transform), true);



        GUILayout.Label("Add an item:");
        // inv.setImportantVariables();                                                                                                            //space to the top gui element
        EditorGUILayout.BeginHorizontal();                                                                                  //starting horizontal GUI elements
        ItemList itemDataBase = (ItemList)Resources.Load("ItemDataBase");

        //ItemDataBaseList inventoryItemList = (ItemDatabase)Resources.Load("ItemDatabase");                            //loading the itemdatabase
        string[] items = new string[itemDataBase.getCount()];

        //create a string array in length of the itemcount
        for (int i = 0; i < items.Length; i++)                                                                              //go through the item array
        {
            items[i] = itemDataBase.getItemByIndex(i).Name;                                                            //and paste all names into the array
        }

        itemIndex = EditorGUILayout.Popup("", itemIndex, items, EditorStyles.popup);                                              //create a popout with all itemnames in it and save the itemID of it
        itemValue = EditorGUILayout.IntField("", itemValue, GUILayout.Width(40));
        GUI.color = Color.yellow;                                                                                            //set the color of all following guielements to green
        if (GUILayout.Button("Add Item"))                                                                                   //creating button with name "AddItem"
        {
            ItemBase item = itemDataBase.getItemByIndex(itemIndex);

            slScript.addItemToNextFreeSlot(item, itemValue);

        }
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Delete All Items From List"))                                                                                   //creating button with name "AddItem"
        {
            slScript.DeleteAllItems();

        }
    }
}

