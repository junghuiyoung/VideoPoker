using UnityEngine;
using UnityEngine.UI;
using TMPro;
using VideoPoker.Data;

namespace VideoPoker.UI
{
    public class PayTableDisplay : MonoBehaviour
    {
        [SerializeField] private PayoutConfiguration payoutConfig;
        
        [Header("Hand Labels")]
        [SerializeField] private TextMeshProUGUI royalFlushText;
        [SerializeField] private TextMeshProUGUI straightFlushText;
        [SerializeField] private TextMeshProUGUI fourOfAKindText;
        [SerializeField] private TextMeshProUGUI fullHouseText;
        [SerializeField] private TextMeshProUGUI flushText;
        [SerializeField] private TextMeshProUGUI straightText;
        [SerializeField] private TextMeshProUGUI threeOfAKindText;
        [SerializeField] private TextMeshProUGUI twoPairText;
        [SerializeField] private TextMeshProUGUI jacksOrBetterText;

        private void Start()
        {
            UpdatePayouts(1); // Initialize with 1 credit bet
        }

        public void UpdatePayouts(int betMultiplier)
        {
            royalFlushText.text = $"Royal Flush\n{payoutConfig.RoyalFlushPayout * betMultiplier}";
            straightFlushText.text = $"Straight Flush\n{payoutConfig.StraightFlushPayout * betMultiplier}";
            fourOfAKindText.text = $"Four of a Kind\n{payoutConfig.FourOfAKindPayout * betMultiplier}";
            fullHouseText.text = $"Full House\n{payoutConfig.FullHousePayout * betMultiplier}";
            flushText.text = $"Flush\n{payoutConfig.FlushPayout * betMultiplier}";
            straightText.text = $"Straight\n{payoutConfig.StraightPayout * betMultiplier}";
            threeOfAKindText.text = $"Three of a Kind\n{payoutConfig.ThreeOfAKindPayout * betMultiplier}";
            twoPairText.text = $"Two Pair\n{payoutConfig.TwoPairPayout * betMultiplier}";
            jacksOrBetterText.text = $"Jacks or Better\n{payoutConfig.JacksOrBetterPayout * betMultiplier}";
        }
    }
}
