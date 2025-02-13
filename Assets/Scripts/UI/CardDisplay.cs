using UnityEngine;
using UnityEngine.UI;
using VideoPoker.Models;
using TMPro;

namespace VideoPoker.UI
{
    public class CardDisplay : MonoBehaviour
    {
        [SerializeField] private Image cardBackground;
        [SerializeField] private Image suitIcon;
        [SerializeField] private TextMeshProUGUI rankText;
        [SerializeField] private Button holdButton;
        [SerializeField] private GameObject holdIndicator;
        
        private Card currentCard;
        private bool isHeld;

        public bool IsHeld => isHeld;

        private void Awake()
        {
            holdButton.onClick.AddListener(ToggleHold);
            SetHoldState(false);
        }

        public void DisplayCard(Card card)
        {
            currentCard = card;
            rankText.text = card.Rank.ToString();
            // Note: You'll need to set up proper suit icons/sprites
            suitIcon.color = card.Suit == Suit.Hearts || card.Suit == Suit.Diamonds 
                ? Color.red : Color.black;
            gameObject.SetActive(true);
        }

        public void HideCard()
        {
            currentCard = null;
            gameObject.SetActive(false);
            SetHoldState(false);
        }

        public void SetHoldState(bool held)
        {
            isHeld = held;
            holdIndicator.SetActive(held);
        }

        private void ToggleHold()
        {
            if (currentCard != null)
            {
                SetHoldState(!isHeld);
            }
        }

        public void EnableInteraction(bool enable)
        {
            holdButton.interactable = enable;
        }
    }
}
