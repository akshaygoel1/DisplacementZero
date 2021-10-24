using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 5f;
    public float runSpeed = 10f;
    public GameObject player;
    float currentSpeed = 0f;
    public Animator anim;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed = runSpeed;
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed = speed;
        }

        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.position = new Vector3(player.transform.position.x + currentSpeed * Time.deltaTime, player.transform.position.y, player.transform.position.z);
            if (player.transform.localScale.x < 0)
            {
                player.transform.localScale = new Vector3(player.transform.localScale.x * -1, player.transform.localScale.y, player.transform.localScale.z);
            }
            anim.SetFloat("speed", currentSpeed);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            player.transform.position = new Vector3(player.transform.position.x - currentSpeed * Time.deltaTime, player.transform.position.y, player.transform.position.z);
            if (player.transform.localScale.x > 0)
            {
                player.transform.localScale = new Vector3(player.transform.localScale.x * -1, player.transform.localScale.y, player.transform.localScale.z);
            }
            anim.SetFloat("speed", currentSpeed);
        }
        else
        {
            anim.SetFloat("speed", 0);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Start")
        {
            GameManager.instance.StartTimer();
        }

        else if(collision.gameObject.tag == "Wife")
        {
            if (TriggerManager.instance.IsNewScenario(Scenarios.WifeIntro))
            {
                TriggerManager.instance.TriggerIntroWife();
            }
            else if (Inventory.instance.InventoryContains(Item.Drink2))
            {
                TriggerManager.instance.TriggerDrinkWife();
            }
        }
        else if (collision.gameObject.tag == "Bartender")
        {
            TriggerManager.instance.TriggerIntroBartender();
        }
        else if(collision.gameObject.tag == "Dad")
        {
            if (Inventory.instance.InventoryContains(Item.Ticket))
            {
                TriggerManager.instance.TriggerIntroDad();
            }
            else if (Inventory.instance.InventoryContains(Item.Bag))
            {
                TriggerManager.instance.TriggerBagDad();
            }
        }
        else if(collision.gameObject.tag == "Lady1")
        {
            if (TriggerManager.instance.IsNewScenario(Scenarios.SistersIntro)) {
                TriggerManager.instance.TriggerIntroSisters();
            }

            else if (Inventory.instance.InventoryContains(Item.Drink1))
            {
                TriggerManager.instance.TriggerDrinkSisters();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wife")
        {
            TriggerManager.instance.DisableE();
        }
        else if (collision.gameObject.tag == "Bartender")
        {
            TriggerManager.instance.DisableE();
        }
        else if (collision.gameObject.tag == "Dad")
        {
                TriggerManager.instance.DisableE();
        }
    }
}
