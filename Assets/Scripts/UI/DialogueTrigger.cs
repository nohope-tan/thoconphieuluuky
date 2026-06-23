using UnityEngine;
using System.Collections.Generic;

namespace Game.UI
{
    public class DialogueTrigger : MonoBehaviour
    {
        [Header("Settings")]
        public bool triggerOnce = true;    
        private bool hasTriggered = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            if (other.CompareTag("Player"))
            {
                if (triggerOnce && hasTriggered) return;

                DialogueManager manager = FindFirstObjectByType<DialogueManager>();
                if (manager != null)
                {
                    
                    manager.StartDialogue();
                    hasTriggered = true;
                }
            }
        }
    }
}

