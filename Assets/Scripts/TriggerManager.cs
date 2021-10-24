using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Subtegral.DialogueSystem.Runtime;
public enum Scenarios
{
    None = -1,
    WifeIntro = 0,
    BartenderIntro,
    TicketDadIntro
}


public class TriggerManager : MonoBehaviour
{
    public static TriggerManager instance = null;
    List<int> completedScenarios = new List<int>();
    public GameObject eWife, eDad, eBartender;
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
    bool IsNewScenario(Scenarios scenario)
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
        }
    }
}
