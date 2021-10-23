using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Subtegral.DialogueSystem.Runtime;
public enum Scenarios
{
    WifeIntro = 0,
    BartenderIntro,
    TicketDadIntro
}

public class TriggerManager : MonoBehaviour
{
    public static TriggerManager instance = null;
    List<int> completedScenarios = new List<int>();

    public void TriggerIntroWife()
    {
        if (IsNewScenario(Scenarios.WifeIntro))
        {
            DialogueParser.instance.TriggerConversation(0);
            completedScenarios.Add((int)Scenarios.WifeIntro);
        }
    }

    public void TriggerIntroBartender()
    {
        if (IsNewScenario(Scenarios.BartenderIntro))
        {
            DialogueParser.instance.TriggerConversation(1);
            completedScenarios.Add((int)Scenarios.BartenderIntro);

        }
    }


    public void TriggerIntroDad()
    {
        if (IsNewScenario(Scenarios.TicketDadIntro))
        {
            DialogueParser.instance.TriggerConversation(2);
            completedScenarios.Add((int)Scenarios.TicketDadIntro);
            Inventory.instance.RemoveItem(Item.Ticket);

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

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
}
