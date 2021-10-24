using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Subtegral.DialogueSystem.DataContainers;

namespace Subtegral.DialogueSystem.Runtime
{
    public class DialogueParser : MonoBehaviour
    {
        public static DialogueParser instance = null;
        [SerializeField] private List<DialogHolder> dialogues = new List<DialogHolder>();

        [SerializeField] private Button choicePrefab;

        List<string> triggersObtained = new List<string>();
        int secondsPassed = 0;


        public void AddTriggerObtained(string s)
        {
            triggersObtained.Add(s);
        }
        public bool TriggerExists(string s)
        {
            return triggersObtained.Exists(x => string.Equals( x, s));
        }
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this);
        }

        private void Start()
        {
            if(PlayerPrefs.GetInt("metceo",0) == 1)
            {
                AddTriggerObtained("metceo");
            }
            StartCoroutine(AutomatedDialog());
        }

        IEnumerator AutomatedDialog()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                secondsPassed++;
                if(dialogues.Exists(x=> x.dialogContainer.isAutomated && secondsPassed >x.dialogContainer.timeToTrigger && secondsPassed - 1.6f < x.dialogContainer.timeToTrigger))
                {
                    TriggerConversation(dialogues.Find(x => x.dialogContainer.isAutomated && secondsPassed > x.dialogContainer.timeToTrigger && secondsPassed -1f <= x.dialogContainer.timeToTrigger).dialogContainer);
                }
            }
        }


        public void TriggerConversation(int index)
        {
            Debug.Log("Triggered Convo");
            var narrativeData = dialogues[index].dialogContainer.NodeLinks.First(); //Entrypoint node



            ProceedToNarrative(dialogues.Find(x => x.dialogContainer == dialogues[index].dialogContainer), narrativeData.TargetNodeGUID);
        }

        public void TriggerConversation(DialogueContainer dialogToTrigger)
        {
            Debug.Log("Triggered Convo");
            var narrativeData = dialogToTrigger.NodeLinks.First(); //Entrypoint node



            ProceedToNarrative(dialogues.Find(x=>x.dialogContainer == dialogToTrigger),narrativeData.TargetNodeGUID);
        }

        private void ProceedToNarrative(DialogHolder dialogue, string narrativeDataGUID)
        {
            dialogue.canvas.SetActive(true);
            var text = dialogue.dialogContainer.DialogueNodeData.Find(x => x.NodeGUID == narrativeDataGUID).DialogueText;
            char[] chars = text.ToCharArray();

            if (chars.Contains(' '))
            {

                string[] dialogWords = text.Split(' ');

                if (dialogWords[dialogWords.Length - 1][0] == '_')
                {
                    //Give player the item corresponding to dialogWords[dialogWords.Length -1]
                    Inventory.instance.AddItem(dialogWords[dialogWords.Length - 1].Remove(0, 1));
                    string s = "";

                    for (int i = 0; i < dialogWords.Length - 1; i++)
                    {
                        s += dialogWords[i] + " ";
                    }
                    dialogue.dialogueText.text = ProcessProperties(dialogue, s);
                }
                else
                {
                    dialogue.dialogueText.text = ProcessProperties(dialogue, text);
                
                }
            }
            else
            {
                dialogue.dialogueText.text = ProcessProperties(dialogue, text);
                if (string.Equals(dialogue.dialogueText.text, "Stay."))
                {
                    GameManager.instance.End();
                }
            }
            var choices = dialogue.dialogContainer.NodeLinks.Where(x => x.BaseNodeGUID == narrativeDataGUID);
           
            var buttons = dialogue.buttonContainer.GetComponentsInChildren<Button>();
            for (int i = 0; i < buttons.Length; i++)
            {
                Destroy(buttons[i].gameObject);
            }

            if(choices.Count() == 0)
            {
                StartCoroutine(HideConvo(dialogue));
            }

            if(choices.Count() == 1)
            {
                IEnumerator enumerator = choices.GetEnumerator();
                enumerator.MoveNext();
                object first = enumerator.Current;

                NodeLinkData firstNode = (NodeLinkData)first;
                if(firstNode.PortName == "-")
                {
                    StartCoroutine(DelayedConversation(dialogue, firstNode));
                    return;
                }
            }
            foreach (var choice in choices)
            {
                char[] charsChoice = choice.PortName.ToCharArray();

                if (charsChoice.Contains(' '))
                {
                    string[] words = choice.PortName.Split(' ');
                    if (words.Length > 0 && words[0][0] == '@')
                    {
                        words[0] = words[0].Remove(0, 1);
                        if (triggersObtained.Exists(x => x == words[0]))
                        {
                            var button = Instantiate(choicePrefab, dialogue.buttonContainer);

                            string s = "";
                            for (int i = 1; i < words.Length; i++)
                            {
                                s += words[i] + " ";
                            }

                            button.GetComponentInChildren<Text>().text = ProcessProperties(dialogue, s);
                            button.onClick.AddListener(() => ProceedToNarrative(dialogue, choice.TargetNodeGUID));
                        }
                    }
                    else
                    {
                        var button = Instantiate(choicePrefab, dialogue.buttonContainer);
                        button.GetComponentInChildren<Text>().text = ProcessProperties(dialogue, choice.PortName);
                        button.onClick.AddListener(() => ProceedToNarrative(dialogue, choice.TargetNodeGUID));
                    }
                }
                else
                {
                    var button = Instantiate(choicePrefab, dialogue.buttonContainer);
                    button.GetComponentInChildren<Text>().text = ProcessProperties(dialogue, choice.PortName);
                    button.onClick.AddListener(() => ProceedToNarrative(dialogue, choice.TargetNodeGUID));
                }
            }
        }

        IEnumerator HideConvo(DialogHolder dialog)
        {
            yield return new WaitForSeconds(2);
            dialog.canvas.SetActive(false);
        }
        IEnumerator DelayedConversation(DialogHolder dialog, NodeLinkData node)
        {
            yield return new WaitForSeconds(2);
            ProceedToNarrative(dialog, node.TargetNodeGUID);
        }

        private string ProcessProperties(DialogHolder dialogue,  string text)
        {
           
            foreach (var exposedProperty in dialogue.dialogContainer.ExposedProperties)
            {
                text = text.Replace($"[{exposedProperty.PropertyName}]", exposedProperty.PropertyValue);
            }
            return text;
        }
    }

    [System.Serializable]
    public class DialogHolder
    {
        public DialogueContainer dialogContainer;
        public GameObject canvas;
        [SerializeField] public Text dialogueText;
        [SerializeField] public Transform buttonContainer;
    }
}