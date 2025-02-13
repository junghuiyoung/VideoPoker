using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace VideoPoker.Models
{
    public class Deck
    {
        private List<Card> cards;
        private System.Random rng;

        public Deck()
        {
            rng = new System.Random();
            InitializeDeck();
        }

        private void InitializeDeck()
        {
            cards = new List<Card>();
            foreach (Suit suit in System.Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in System.Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(suit, rank));
                }
            }
        }

        public void Shuffle()
        {
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card temp = cards[k];
                cards[k] = cards[n];
                cards[n] = temp;
            }
        }

        public Card DrawCard()
        {
            if (cards.Count == 0)
            {
                Debug.LogWarning("Attempting to draw from an empty deck. Reinitializing...");
                InitializeDeck();
                Shuffle();
            }

            Card card = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
            return card;
        }

        public int RemainingCards => cards.Count;
    }
}
