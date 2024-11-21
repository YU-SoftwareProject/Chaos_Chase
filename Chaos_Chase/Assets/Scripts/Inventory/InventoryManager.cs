using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List <Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    private void Awake() 
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        Items.Add(item);
        ListItems();
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }
    
    public void ListItems()
    {
        for (int i = 0; i < ItemContent.childCount; i++)
        {
            Transform slot = ItemContent.GetChild(i);
        
            // 아이템이 slot에 존재하는 경우 아이템 이미지 보이게 설정
            if (i < Items.Count)
            {
                var itemIcon = slot.Find("ItemImage").GetComponent<UnityEngine.UI.Image>();
                itemIcon.sprite = Items[i].icon;
                itemIcon.enabled = true;
            }
            // 아이템이 slot에 없는 나머지 빈 슬롯 유지
            else
            {
                var itemIcon = slot.Find("ItemImage").GetComponent<UnityEngine.UI.Image>();
                itemIcon.enabled = true;
            }
        }
    }
}
