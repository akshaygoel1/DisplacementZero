using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public List<SpriteLibrary> spriteLib = new List<SpriteLibrary>();
    public SpriteRenderer slot1, slot2, slot3;
    int count =0;
    public static Inventory instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void AddItem(string itemType)
    {

        GameObject g = Instantiate(spriteLib.Find(x => x.itemName == itemType).itemPrefab, Vector3.zero, Quaternion.identity);
        if (count == 0)
        {
            g.transform.SetParent(slot1.transform);
            g.transform.position = Vector3.zero;
        }
        else if (count == 1)
        {
            g.transform.SetParent(slot2.transform);
            g.transform.position = Vector3.zero;
        }
        else if (count == 2)
        {
            g.transform.SetParent(slot3.transform);
            g.transform.position = Vector3.zero;
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
        count++;
    }


    public void AddItem(Item itemType)
    {

        GameObject g = Instantiate(spriteLib.Find(x => x.item == itemType).itemPrefab, Vector3.zero, Quaternion.identity);
        if (count == 0)
        {
            g.transform.SetParent(slot1.transform);
            g.transform.position = Vector3.zero;
        }
        else if (count == 1)
        {
            g.transform.SetParent(slot2.transform);
            g.transform.position = Vector3.zero;
        }
        else if (count == 2)
        {
            g.transform.SetParent(slot3.transform);
            g.transform.position = Vector3.zero;
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
        count++;
    }

    public void RemoveItem()
    {
        count--;
        RefreshSlots();
    }

    void RefreshSlots()
    {
        if (count != 0)
        {
            if(slot1.transform.childCount == 0)
            {
                if (slot3.transform.childCount == 1)
                {
                    if(slot2.transform.childCount == 0)
                    {
                        GameObject g = slot3.transform.GetChild(0).gameObject;
                        g.transform.SetParent(slot2.transform);
                        g.transform.position = Vector3.zero;
                    }
                    else
                    {
                        GameObject g = slot3.transform.GetChild(0).gameObject;
                        g.transform.SetParent(slot1.transform);
                        g.transform.position = Vector3.zero;
                    }
                   
                }
                if (slot2.transform.childCount == 1)
                {
                     GameObject g = slot2.transform.GetChild(0).gameObject;
                    g.transform.SetParent(slot1.transform);
                        g.transform.position = Vector3.zero;
                }
               
            }
        }
    }

}


public enum Item
{
    None,
    Screwdriver
}
[System.Serializable]
public class SpriteLibrary
{
    public Item item;
    public GameObject itemPrefab;
    public string itemName;
}