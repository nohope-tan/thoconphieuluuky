using UnityEngine;
using System.Collections.Generic;

namespace Game.UI
{
    public class DialogueTrigger : MonoBehaviour
    {
        [Header("Settings")]
        public bool triggerOnce = true;    // Chỉ hiện thoại 1 lần duy nhất?
        private bool hasTriggered = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Kiểm tra nếu va chạm với Player
            if (other.CompareTag("Player"))
            {
                if (triggerOnce && hasTriggered) return;

                DialogueManager manager = FindFirstObjectByType<DialogueManager>();
                if (manager != null)
                {
                    // Kích hoạt hội thoại từ manager (Manager đã chứa sẵn list các huongdan)
                    manager.StartDialogue();
                    hasTriggered = true;
                }
            }
        }
    }
}
