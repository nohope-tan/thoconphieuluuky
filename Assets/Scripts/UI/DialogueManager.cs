using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game.UI
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("UI References")]
        public GameObject dialoguePanel;
        public List<GameObject> dialogueObjects;

        [Header("Settings")]
        public float typingSpeed = 0.05f;        

        private int currentIndex = 0;
        private bool isTyping = false;
        private Coroutine typingCoroutine;
        private string originalText;             

        void Start()
        {
            if (dialoguePanel != null) dialoguePanel.SetActive(false);
            foreach (GameObject obj in dialogueObjects)
            {
                if (obj != null) obj.SetActive(false);
            }
        }

        void Update()
        {
            if (dialoguePanel != null && dialoguePanel.activeSelf && Input.GetMouseButtonDown(0))
            {
                DisplayNextObject();
            }
        }

        public void StartDialogue()
        {
            if (dialogueObjects == null || dialogueObjects.Count == 0 || dialoguePanel == null) return;

            dialoguePanel.SetActive(true);
            currentIndex = 0;
            
            foreach (GameObject obj in dialogueObjects) 
            {
                if (obj != null) obj.SetActive(false);
            }

            DisplayNextObject();
        }

        public void DisplayNextObject()
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                isTyping = false;
                
                if (currentIndex > 0 && currentIndex <= dialogueObjects.Count)
                {
                    GameObject currentObj = dialogueObjects[currentIndex - 1];
                    if (currentObj != null)
                    {
                        TextMeshProUGUI textComp = currentObj.GetComponent<TextMeshProUGUI>();
                        if (textComp != null) textComp.text = originalText;
                    }
                }
                return;
            }

            if (currentIndex < dialogueObjects.Count)
            {
                if (currentIndex > 0 && dialogueObjects[currentIndex - 1] != null) 
                    dialogueObjects[currentIndex - 1].SetActive(false);

                GameObject nextObj = dialogueObjects[currentIndex];
                if (nextObj != null)
                {
                    nextObj.SetActive(true);
                    TextMeshProUGUI textComp = nextObj.GetComponent<TextMeshProUGUI>();
                    if (textComp != null)
                    {
                        originalText = textComp.text;
                        typingCoroutine = StartCoroutine(TypeSentence(textComp, originalText));
                    }
                }
                currentIndex++;
            }
            else
            {
                EndDialogue();
            }
        }

        IEnumerator TypeSentence(TextMeshProUGUI textComp, string content)
        {
            if (textComp == null) yield break;
            textComp.text = "";
            isTyping = true;
            foreach (char letter in content.ToCharArray())
            {
                textComp.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
            isTyping = false;
        }

        public void EndDialogue()
        {
            if (dialoguePanel != null) dialoguePanel.SetActive(false);
            if (currentIndex > 0 && currentIndex <= dialogueObjects.Count)
            {
                if (dialogueObjects[currentIndex - 1] != null)
                    dialogueObjects[currentIndex - 1].SetActive(false);
            }
            currentIndex = 0;
        }
    }
}
