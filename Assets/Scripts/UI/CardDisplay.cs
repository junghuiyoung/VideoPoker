using UnityEngine;
using UnityEngine.UI;
using VideoPoker.Models;
using TMPro;
using System.Collections.Generic;

namespace VideoPoker.UI
{
    public class CardDisplay : MonoBehaviour
    {
        [SerializeField] private Image cardBackground;
        [SerializeField] private TextMeshProUGUI suitIcon;  // Changed from Image to TextMeshProUGUI
        [SerializeField] private TextMeshProUGUI rankText;
        [SerializeField] private Button holdButton;
        [SerializeField] private GameObject holdIndicator;
        
        private Card currentCard;
        private bool isHeld;

        private readonly Dictionary<Suit, string> suitSymbols = new Dictionary<Suit, string>
        {
            { Suit.Hearts, "♥" },
            { Suit.Diamonds, "♦" },
            { Suit.Clubs, "♣" },
            { Suit.Spades, "♠" }
        };

        public bool IsHeld => isHeld;

        private void Awake()
        {
            holdButton.onClick.AddListener(ToggleHold);
            SetHoldState(false);
        }

        public void DisplayCard(Card card)
        {
            currentCard = card;
            rankText.text = GetRankDisplay(card.Rank);
            suitIcon.text = suitSymbols[card.Suit];         // Directly set text
            suitIcon.color = card.Suit == Suit.Hearts || card.Suit == Suit.Diamonds 
                ? Color.red : Color.black;
            gameObject.SetActive(true);
        }

        private string GetRankDisplay(Rank rank)
        {
            return rank switch
            {
                Rank.Jack => "J",
                Rank.Queen => "Q",
                Rank.King => "K",
                Rank.Ace => "A",
                _ => ((int)rank).ToString()
            };
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
