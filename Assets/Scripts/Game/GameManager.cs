using UnityEngine;
using VideoPoker.Models;
using VideoPoker.UI;
using VideoPoker.Data;

namespace VideoPoker.Game
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game Configuration")]
        [SerializeField] private PayoutConfiguration payoutConfig;
        [SerializeField] private int startingCredits = 1000;

        [Header("UI References")]
        [SerializeField] private CardsAreaManager cardsArea;
        [SerializeField] private ControlsManager controls;
        [SerializeField] private StatsDisplay stats;
        [SerializeField] private PayTableDisplay payTable;

        private Deck deck;
        private PlayerState playerState;
        private PokerHandEvaluator handEvaluator;
        private bool isFirstDeal = true;

        private void Start()
        {
            deck = new Deck();
            playerState = new PlayerState(startingCredits);
            handEvaluator = new PokerHandEvaluator();
            
            InitializeUI();
        }

        private void InitializeUI()
        {
            UpdateStatsDisplay();
            payTable.UpdatePayouts(playerState.CurrentBet);
            cardsArea.EnableHoldInteraction(false);
        }

        public void OnDealPressed()
        {
            if (isFirstDeal)
            {
                StartNewHand();
            }
            else
            {
                CompleteDraw();
            }
        }

        private void StartNewHand()
        {
            if (!playerState.CanBet(playerState.CurrentBet))
            {
                Debug.LogWarning("Not enough credits to bet!");
                return;
            }

            playerState.PlaceBet();
            deck.Shuffle();
            cardsArea.ClearHoldStates();  // Add this line
            
            Card[] newHand = new Card[5];
            for (int i = 0; i < 5; i++)
            {
                newHand[i] = deck.DrawCard();
            }

            cardsArea.DisplayCards(newHand);
            cardsArea.EnableHoldInteraction(true);
            isFirstDeal = false;
            UpdateStatsDisplay();
        }

        private void CompleteDraw()
        {
            bool[] heldCards = cardsArea.GetHeldCards();
            Card[] currentHand = cardsArea.GetCurrentHand();

            for (int i = 0; i < heldCards.Length; i++)
            {
                if (!heldCards[i])
                {
                    currentHand[i] = deck.DrawCard();
                }
            }

            cardsArea.DisplayCards(currentHand);
            cardsArea.EnableHoldInteraction(false);
            EvaluateAndPayHand(currentHand);
            isFirstDeal = true;
        }

        private void EvaluateAndPayHand(Card[] hand)
        {
            HandRank rank = handEvaluator.EvaluateHand(hand);
            int winAmount = CalculateWinAmount(rank);
            
            if (winAmount > 0)
            {
                playerState.AddWinnings(winAmount);
            }

            UpdateStatsDisplay();
        }

        private int CalculateWinAmount(HandRank rank)
        {
            return rank switch
            {
                HandRank.RoyalFlush => payoutConfig.RoyalFlushPayout * playerState.CurrentBet,
                HandRank.StraightFlush => payoutConfig.StraightFlushPayout * playerState.CurrentBet,
                HandRank.FourOfAKind => payoutConfig.FourOfAKindPayout * playerState.CurrentBet,
                HandRank.FullHouse => payoutConfig.FullHousePayout * playerState.CurrentBet,
                HandRank.Flush => payoutConfig.FlushPayout * playerState.CurrentBet,
                HandRank.Straight => payoutConfig.StraightPayout * playerState.CurrentBet,
                HandRank.ThreeOfAKind => payoutConfig.ThreeOfAKindPayout * playerState.CurrentBet,
                HandRank.TwoPair => payoutConfig.TwoPairPayout * playerState.CurrentBet,
                HandRank.JacksOrBetter => payoutConfig.JacksOrBetterPayout * playerState.CurrentBet,
                _ => 0
            };
        }

        private void UpdateStatsDisplay()
        {
            stats.UpdateCredits(playerState.Credits);
            stats.UpdateBet(playerState.CurrentBet);
            stats.UpdateLastWin(playerState.LastWin);
        }

        public void OnBetOnePressed()
        {
            if (isFirstDeal)
            {
                playerState.IncreaseBet();
                payTable.UpdatePayouts(playerState.CurrentBet);
                UpdateStatsDisplay();
            }
        }

        public void OnBetMaxPressed()
        {
            if (isFirstDeal)
            {
                playerState.SetMaxBet();
                payTable.UpdatePayouts(playerState.CurrentBet);
                UpdateStatsDisplay();
            }
        }
    }
}
