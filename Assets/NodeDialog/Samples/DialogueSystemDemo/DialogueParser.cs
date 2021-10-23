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
        [SerializeField] private List<DialogueContainer> dialogues = new List<DialogueContainer>();

        [SerializeField] private Button choicePrefab;

        List<string> triggersObtained = new List<string>();

        public void TriggerConversation(DialogueContainer dialogToTrigger)
        {
            var narrativeData = dialogToTrigger.NodeLinks.First(); //Entrypoint node
            ProceedToNarrative(dialogToTrigger,narrativeData.TargetNodeGUID);
        }

        private void ProceedToNarrative(DialogueContainer dialogue, string narrativeDataGUID)
        {
            var text = dialogue.DialogueNodeData.Find(x => x.NodeGUID == narrativeDataGUID).DialogueText;

            string[] dialogWords = text.Split(' ');

            if (dialogWords[dialogWords.Length - 1][0] == '_')
            {
                //Give player the item corresponding to dialogWords[dialogWords.Length -1]

                string s = "";

                for(int i=0; i < dialogWords.Length - 1; i++)
                {
                    s += dialogWords[i] + " ";
                }
                dialogue.dialogueText.text = ProcessProperties(dialogue, s);
            }
            else
            {
                dialogue.dialogueText.text = ProcessProperties(dialogue, text);

            }
            var choices = dialogue.NodeLinks.Where(x => x.BaseNodeGUID == narrativeDataGUID);
           
            var buttons = dialogue.buttonContainer.GetComponentsInChildren<Button>();
            for (int i = 0; i < buttons.Length; i++)
            {
                Destroy(buttons[i].gameObject);
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
                string[] words = choice.PortName.Split(' ');
                if (words[0][0] == '@')
                {
                    words[0]= words[0].Remove(0,1);
                    if (triggersObtained.Exists(x => x == words[0]))
                    {
                        var button = Instantiate(choicePrefab, dialogue.buttonContainer);

                        string s = "";
                        for(int i= 1; i < words.Length; i++)
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
                    button.onClick.AddListener(() => ProceedToNarrative(dialogue,choice.TargetNodeGUID));
                }
            }
        }

        IEnumerator DelayedConversation(DialogueContainer dialog, NodeLinkData node)
        {
            yield return new WaitForSeconds(2);
            ProceedToNarrative(dialog, node.TargetNodeGUID);
        }

        private string ProcessProperties(DialogueContainer dialogue,  string text)
        {
           
            foreach (var exposedProperty in dialogue.ExposedProperties)
            {
                text = text.Replace($"[{exposedProperty.PropertyName}]", exposedProperty.PropertyValue);
            }
            return text;
        }
    }
}