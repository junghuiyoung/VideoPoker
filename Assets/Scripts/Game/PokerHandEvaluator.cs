using System.Linq;
using System.Collections.Generic;
using VideoPoker.Models;

namespace VideoPoker.Game
{
    public enum HandRank
    {
        Nothing,
        JacksOrBetter,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }

    public class PokerHandEvaluator
    {
        public HandRank EvaluateHand(Card[] cards)
        {
            if (cards.Length != 5) return HandRank.Nothing;

            bool isFlush = IsFlush(cards);
            bool isStraight = IsStraight(cards);

            if (isFlush && isStraight)
            {
                return IsRoyalFlush(cards) ? HandRank.RoyalFlush : HandRank.StraightFlush;
            }

            if (IsFourOfAKind(cards)) return HandRank.FourOfAKind;
            if (IsFullHouse(cards)) return HandRank.FullHouse;
            if (isFlush) return HandRank.Flush;
            if (isStraight) return HandRank.Straight;
            if (IsThreeOfAKind(cards)) return HandRank.ThreeOfAKind;
            if (IsTwoPair(cards)) return HandRank.TwoPair;
            if (IsJacksOrBetter(cards)) return HandRank.JacksOrBetter;

            return HandRank.Nothing;
        }

        private bool IsFlush(Card[] cards)
        {
            return cards.All(c => c.Suit == cards[0].Suit);
        }

        private bool IsStraight(Card[] cards)
        {
            var orderedCards = cards.OrderBy(c => (int)c.Rank).ToArray();
            for (int i = 1; i < orderedCards.Length; i++)
            {
                if ((int)orderedCards[i].Rank != (int)orderedCards[i - 1].Rank + 1)
                    return false;
            }
            return true;
        }

        private bool IsRoyalFlush(Card[] cards)
        {
            return IsFlush(cards) && cards.All(c => (int)c.Rank >= 10) && IsStraight(cards);
        }

        private bool IsFourOfAKind(Card[] cards)
        {
            return cards.GroupBy(c => c.Rank).Any(g => g.Count() == 4);
        }

        private bool IsFullHouse(Card[] cards)
        {
            var groups = cards.GroupBy(c => c.Rank).ToList();
            return groups.Count == 2 && groups.Any(g => g.Count() == 3);
        }

        private bool IsThreeOfAKind(Card[] cards)
        {
            return cards.GroupBy(c => c.Rank).Any(g => g.Count() == 3);
        }

        private bool IsTwoPair(Card[] cards)
        {
            return cards.GroupBy(c => c.Rank).Count(g => g.Count() == 2) == 2;
        }

        private bool IsJacksOrBetter(Card[] cards)
        {
            return cards.GroupBy(c => c.Rank)
                       .Any(g => g.Count() == 2 && 
                                ((int)g.Key >= (int)Rank.Jack || g.Key == Rank.Ace));
        }
    }
}
