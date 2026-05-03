using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game.UI
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("UI References")]
        public GameObject dialoguePanel;         // Kéo Object 'Texttho dai ca' vào đây
        public List<GameObject> dialogueObjects; // Kéo huongdan1, huongdan2, huongdan3... vào đây theo thứ tự

        [Header("Settings")]
        public float typingSpeed = 0.05f;        // Tốc độ hiện chữ

        private int currentIndex = 0;
        private bool isTyping = false;
        private Coroutine typingCoroutine;
        private string originalText;             // Lưu lại nội dung gốc của text

        void Start()
        {
            // Ẩn tất cả các text và panel lúc đầu
            if (dialoguePanel != null) dialoguePanel.SetActive(false);
            foreach (GameObject obj in dialogueObjects)
            {
                if (obj != null) obj.SetActive(false);
            }
        }

        void Update()
        {
            // Nhấn Space hoặc Chuột trái để tiếp tục
            if (dialoguePanel.activeSelf && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
            {
                DisplayNextObject();
            }
        }

        public void StartDialogue()
        {
            if (dialogueObjects.Count == 0) return;

            dialoguePanel.SetActive(true);
            currentIndex = 0;
            
            // Ẩn tất cả trước khi bắt đầu
            foreach (GameObject obj in dialogueObjects) obj.SetActive(false);

            DisplayNextObject();
        }

        public void DisplayNextObject()
        {
            // Nếu đang gõ mà bấm tiếp, hiện hết chữ luôn
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                isTyping = false;
                
                GameObject currentObj = dialogueObjects[currentIndex - 1];
                TextMeshProUGUI textComp = currentObj.GetComponent<TextMeshProUGUI>();
                if (textComp != null) textComp.text = originalText;
                return;
            }

            // Nếu còn Object tiếp theo
            if (currentIndex < dialogueObjects.Count)
            {
                // Ẩn Object cũ (nếu có)
                if (currentIndex > 0) dialogueObjects[currentIndex - 1].SetActive(false);

                // Hiện Object mới
                GameObject nextObj = dialogueObjects[currentIndex];
                nextObj.SetActive(true);

                // Lấy component Text để chạy hiệu ứng
                TextMeshProUGUI textComp = nextObj.GetComponent<TextMeshProUGUI>();
                if (textComp != null)
                {
                    originalText = textComp.text;
                    typingCoroutine = StartCoroutine(TypeSentence(textComp, originalText));
                }

                currentIndex++;
            }
            else
            {
                // Hết danh sách thì đóng lại
                EndDialogue();
            }
        }

        IEnumerator TypeSentence(TextMeshProUGUI textComp, string content)
        {
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
            dialoguePanel.SetActive(false);
            if (currentIndex > 0 && currentIndex <= dialogueObjects.Count)
                dialogueObjects[currentIndex - 1].SetActive(false);
            currentIndex = 0;
        }
    }
}
