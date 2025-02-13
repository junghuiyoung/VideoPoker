using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VideoPoker.UI
{
    public class StatsDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI creditsText;
        [SerializeField] private TextMeshProUGUI currentBetText;
        [SerializeField] private TextMeshProUGUI lastWinText;

        public void UpdateCredits(int credits)
        {
            creditsText.text = $"Credits: {credits}";
        }

        public void UpdateBet(int bet)
        {
            currentBetText.text = $"Bet: {bet}";
        }

        public void UpdateLastWin(int amount)
        {
            lastWinText.text = $"Last Win: {amount}";
        }
    }
}
