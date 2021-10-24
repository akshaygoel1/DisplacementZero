using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject e;
    public Item item;

    bool isInTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isInTrigger = true;
            e.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isInTrigger = false;
            e.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInTrigger)
            {
                Inventory.instance.AddItem(item);
                            Destroy(this.gameObject);
            }
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            Inventory.instance.AddItem(item);
    //            Destroy(this.gameObject);
    //        }
    //    }
    //}


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        e.SetActive(true);
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        e.SetActive(false);
    //    }
    //}

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            Inventory.instance.AddItem(item);
    //            Destroy(this.gameObject);
    //        }
    //    }
    //}
}
