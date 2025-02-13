using UnityEngine;

namespace VideoPoker.Game
{
    [System.Serializable]
    public class PlayerState
    {
        public int Credits { get; private set; }
        public int CurrentBet { get; private set; }
        public int LastWin { get; private set; }

        public PlayerState(int startingCredits = 1000)
        {
            Credits = startingCredits;
            CurrentBet = 1;
            LastWin = 0;
        }

        public bool CanBet(int amount)
        {
            return Credits >= amount;
        }

        public void PlaceBet()
        {
            if (CanBet(CurrentBet))
            {
                Credits -= CurrentBet;
            }
        }

        public void IncreaseBet()
        {
            if (CurrentBet < 5)
            {
                CurrentBet++;
            }
        }

        public void SetMaxBet()
        {
            CurrentBet = 5;
        }

        public void AddWinnings(int amount)
        {
            LastWin = amount;
            Credits += amount;
        }
    }
}
