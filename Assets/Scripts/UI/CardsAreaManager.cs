using UnityEngine;
using VideoPoker.Models;
using System.Collections.Generic;
using System.Linq;

namespace VideoPoker.UI
{
    public class CardsAreaManager : MonoBehaviour
    {
        [SerializeField] private CardDisplay[] cardDisplays;
        private Card[] currentHand;

        private void Awake()
        {
            currentHand = new Card[5];
        }

        public void DisplayCards(Card[] cards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                currentHand[i] = cards[i];
                cardDisplays[i].DisplayCard(cards[i]);
            }
        }

        public void ClearCards()
        {
            foreach (var display in cardDisplays)
            {
                display.HideCard();
            }
        }

        public void EnableHoldInteraction(bool enable)
        {
            foreach (var display in cardDisplays)
            {
                display.EnableInteraction(enable);
            }
        }

        public bool[] GetHeldCards()
        {
            return cardDisplays.Select(d => d.IsHeld).ToArray();
        }

        public Card[] GetCurrentHand()
        {
            return currentHand.ToArray();
        }
    }
}
