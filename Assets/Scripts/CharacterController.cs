using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Subtegral.DialogueSystem.Runtime;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public float speed = 5f;
    public float runSpeed = 10f;
    public GameObject player;
    float currentSpeed = 0f;
    public Animator anim;
    public GameObject lockGO, coverGO, bombE;
    public Sprite wireCut;
    public SpriteRenderer bomb;
    public GameObject textInput;
    public InputField input;
    int bombStep = 0;
    public GameObject playerDialogCanvas;
    public Text dialogText;
    public static CharacterController instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerPrefs.DeleteAll();
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (bombE.activeInHierarchy)
            {
                DisarmBomb();
            }
        }
    }


    public void TriggerPlayerDialog(string s)
    {
        playerDialogCanvas.SetActive(true);
        dialogText.text = s;
        StartCoroutine(DisableDialogBox());
    }


    IEnumerator DisableDialogBox()
    {
        yield return new WaitForSeconds(3);
        playerDialogCanvas.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Start")
        {
            GameManager.instance.StartTimer();
        }

        else if (collision.gameObject.tag == "Wife")
        {
            if (PlayerPrefs.GetInt("diffuse", 0) == 1)
            {
                TriggerManager.instance.TriggerWifeEnd();
            }
            else if (TriggerManager.instance.IsNewScenario(Scenarios.WifeIntro))
            {
                TriggerManager.instance.TriggerIntroWife();
            }
            else if (Inventory.instance.InventoryContains(Item.Drink2))
            {
                TriggerManager.instance.TriggerDrinkWife();
            }
            else if (Inventory.instance.InventoryContains(Item.Bracelet))
            {
                TriggerManager.instance.TriggerWifeBracelet();
            }
        }
        else if (collision.gameObject.tag == "Bartender")
        {
            TriggerManager.instance.TriggerIntroBartender();
        }
        else if (collision.gameObject.tag == "Dad")
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
        else if (collision.gameObject.tag == "Lady1")
        {
            if (TriggerManager.instance.IsNewScenario(Scenarios.SistersIntro))
            {
                TriggerManager.instance.TriggerIntroSisters();
            }

            else if (Inventory.instance.InventoryContains(Item.Drink1))
            {
                TriggerManager.instance.TriggerDrinkSisters();
            }
        }
        else if (collision.gameObject.tag == "Ceo")
        {
            if (Inventory.instance.InventoryContains(Item.Wallet))
            {
                TriggerManager.instance.TriggerCeoWallet();
            }
        }
        else if (collision.gameObject.tag == "Conductor")
        {
            Debug.Log("Collided with conductor");
            if (DialogueParser.instance.TriggerExists("metceo"))
            {
                TriggerManager.instance.TriggerConductorKey();
            }
        }
        else if (collision.gameObject.tag == "Lock")
        {
            if (Inventory.instance.InventoryContains(Item.Key))
            {
                Inventory.instance.RemoveItem(Item.Key);
                lockGO.SetActive(false);
            }
        }
        else if (collision.gameObject.tag == "Bomb")
        {

            bombE.SetActive(true);

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
        else if (collision.gameObject.tag == "Lady1")
        {
            TriggerManager.instance.DisableE();
        }
        else if (collision.gameObject.tag == "Ceo")
        {
            TriggerManager.instance.DisableE();
        }
        else if (collision.gameObject.tag == "Bomb")
        {
            bombE.SetActive(false);
        }
        //else if (collision.gameObject.tag == "Conductor")
        //{
        //    TriggerManager.instance.DisableE();
        //}
    }

    void DisarmBomb()
    {
        if (bombStep == 0)
        {
            if (Inventory.instance.InventoryContains(Item.Screwdriver))
            {
                coverGO.SetActive(false);
                bombStep++;
                Inventory.instance.RemoveItem(Item.Screwdriver);
            }
        }
        if (bombStep == 1)
        {
            if (Inventory.instance.InventoryContains(Item.WireCutters))
            {
                bomb.sprite = wireCut;
                bombStep++;
                Inventory.instance.RemoveItem(Item.WireCutters);

            }
        }
        if (bombStep == 2)
        {
            textInput.SetActive(true);
            bombStep++;
            bombE.SetActive(false);
        }
    }

    public void CheckCode()
    {
        if (input.text.Equals("0710"))
        {
            textInput.SetActive(false);
            bomb.gameObject.SetActive(false);
            TriggerPlayerDialog("Yay! I diffused the bomb!");
            PlayerPrefs.SetInt("diffuse", 1);
        }
        else
        {
            input.text = "";
        }
    }
}
