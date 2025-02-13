using UnityEngine;

namespace VideoPoker.Data
{
    [CreateAssetMenu(fileName = "PayoutConfiguration", menuName = "Video Poker/Payout Configuration")]
    public class PayoutConfiguration : ScriptableObject
    {
        public int RoyalFlushPayout = 250;     // Usually 800 for max bet (5 coins)
        public int StraightFlushPayout = 50;
        public int FourOfAKindPayout = 25;
        public int FullHousePayout = 9;
        public int FlushPayout = 6;
        public int StraightPayout = 4;
        public int ThreeOfAKindPayout = 3;
        public int TwoPairPayout = 2;
        public int JacksOrBetterPayout = 1;    // Pair of Jacks or better
    }
}
