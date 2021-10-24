using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Subtegral.DialogueSystem.Runtime;
public enum Scenarios
{
    None = -1,
    WifeIntro = 0,
    BartenderIntro,
    TicketDadIntro,
    BagDad,
    SistersIntro,
    WifeDrink,
    SistersDrink,
    CeoWallet,
    ConductorKey,
    WifeBracelet,
    WifeEnd
}


public class TriggerManager : MonoBehaviour
{
    public static TriggerManager instance = null;
    List<int> completedScenarios = new List<int>();
    public GameObject eWife, eDad, eBartender, eSisters, eCeo, eConductor;
    Scenarios currentScenario = Scenarios.None;
    GameObject currentEActive;
    public void TriggerIntroWife()
    {
        if (IsNewScenario(Scenarios.WifeIntro))
        {
            eWife.SetActive(true);
            currentScenario = Scenarios.WifeIntro;
            currentEActive = eWife;
        }
    }

    public void TriggerIntroBartender()
    {
        if (IsNewScenario(Scenarios.BartenderIntro))
        {
            eBartender.SetActive(true);
            currentScenario = Scenarios.BartenderIntro;
            currentEActive = eBartender;
        }
    }


    public void TriggerIntroDad()
    {
        if (IsNewScenario(Scenarios.TicketDadIntro))
        {
            eDad.SetActive(true);
            currentScenario = Scenarios.TicketDadIntro;
            currentEActive = eDad;
        }
    }

    public void TriggerBagDad()
    {
        if (IsNewScenario(Scenarios.BagDad))
        {
            eDad.SetActive(true);
            currentScenario = Scenarios.BagDad;
            currentEActive = eDad;
        }
    }

    public void TriggerIntroSisters()
    {
        if (IsNewScenario(Scenarios.SistersIntro))
        {
            eSisters.SetActive(true);
            currentScenario = Scenarios.SistersIntro;
            currentEActive = eSisters;
        }
    }

    public void TriggerDrinkWife()
    {
        if (IsNewScenario(Scenarios.WifeDrink))
        {
            eWife.SetActive(true);
            currentScenario = Scenarios.WifeDrink;
            currentEActive = eWife;
        }
    }

    public void TriggerDrinkSisters()
    {
        if (IsNewScenario(Scenarios.SistersDrink))
        {
            eSisters.SetActive(true);
            currentScenario = Scenarios.SistersDrink;
            currentEActive = eSisters;
        }
    }

    public void TriggerCeoWallet()
    {
        if (IsNewScenario(Scenarios.CeoWallet))
        {
            eCeo.SetActive(true);
            currentScenario = Scenarios.CeoWallet;
            currentEActive = eCeo;
        }
    }
    public void TriggerConductorKey()
    {
        if (IsNewScenario(Scenarios.ConductorKey))
        {
            eConductor.SetActive(true);
            currentScenario = Scenarios.ConductorKey;
            currentEActive = eConductor;
        }
    }

    public void TriggerWifeBracelet()
    {
        if (IsNewScenario(Scenarios.WifeBracelet))
        {
            eWife.SetActive(true);
            currentScenario = Scenarios.WifeBracelet;
            currentEActive = eWife;
        }
    }


    public void TriggerWifeEnd()
    {
        if (IsNewScenario(Scenarios.WifeEnd))
        {
            eWife.SetActive(true);
            currentScenario = Scenarios.WifeEnd;
            currentEActive = eWife;
        }
    }

    public bool IsNewScenario(Scenarios scenario)
    {
        if(completedScenarios.Exists(x=>x == (int)scenario))
        {
            return false;
        }
        return true;
    }

    public void DisableE()
    {
        eWife.SetActive(false);
        eDad.SetActive(false);
        eBartender.SetActive(false);
        eSisters.SetActive(false);
        eCeo.SetActive(false);
        eConductor.SetActive(false); 
        currentScenario = Scenarios.None;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentEActive?.SetActive(false);
            if (currentScenario == Scenarios.WifeIntro)
            {
                DialogueParser.instance.TriggerConversation(0);
                completedScenarios.Add((int)Scenarios.WifeIntro);
            }
            if (currentScenario == Scenarios.BartenderIntro)
            {
                DialogueParser.instance.TriggerConversation(1);
                completedScenarios.Add((int)Scenarios.BartenderIntro);

            }
            if (currentScenario == Scenarios.TicketDadIntro)
            {
                DialogueParser.instance.TriggerConversation(2);
                completedScenarios.Add((int)Scenarios.TicketDadIntro);
                Inventory.instance.RemoveItem(Item.Ticket);

            }
            if (currentScenario == Scenarios.BagDad)
            {
                DialogueParser.instance.TriggerConversation(3);
                completedScenarios.Add((int)Scenarios.BagDad);
                Inventory.instance.RemoveItem(Item.Bag);

            }
            if (currentScenario == Scenarios.SistersIntro)
            {
                DialogueParser.instance.TriggerConversation(4);
                completedScenarios.Add((int)Scenarios.SistersIntro);

            }
            if (currentScenario == Scenarios.WifeDrink)
            {
                DialogueParser.instance.TriggerConversation(8);
                completedScenarios.Add((int)Scenarios.WifeDrink);
                Inventory.instance.RemoveItem(Item.Drink2);

            }
            if (currentScenario == Scenarios.SistersDrink)
            {
                DialogueParser.instance.TriggerConversation(5);
                completedScenarios.Add((int)Scenarios.SistersDrink);
                Inventory.instance.RemoveItem(Item.Drink1);

            }
            if (currentScenario == Scenarios.CeoWallet)
            {
                DialogueParser.instance.TriggerConversation(6);
                completedScenarios.Add((int)Scenarios.CeoWallet);
                Inventory.instance.RemoveItem(Item.Wallet);
                DialogueParser.instance.AddTriggerObtained("metceo");
                PlayerPrefs.SetInt("metceo", 1);

            }
            if (currentScenario == Scenarios.ConductorKey)
            {
                DialogueParser.instance.TriggerConversation(7);
                completedScenarios.Add((int)Scenarios.ConductorKey);

            }
            if (currentScenario == Scenarios.WifeBracelet)
            {
                DialogueParser.instance.TriggerConversation(9);
                completedScenarios.Add((int)Scenarios.WifeBracelet);
                Inventory.instance.RemoveItem(Item.Bracelet);
            }
            if (currentScenario == Scenarios.WifeEnd)
            {
                DialogueParser.instance.TriggerConversation(10);
                completedScenarios.Add((int)Scenarios.WifeEnd);
            }

        }
    }
}
