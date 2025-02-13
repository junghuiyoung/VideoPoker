using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VideoPoker.UI
{
    public class ControlsManager : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button dealButton;
        [SerializeField] private Button betOneButton;
        [SerializeField] private Button betMaxButton;
        
        [Header("Labels")]
        [SerializeField] private TextMeshProUGUI dealButtonText;

        private bool isFirstDeal = true;

        private void Awake()
        {
            dealButton.onClick.AddListener(OnDealPressed);
            betOneButton.onClick.AddListener(OnBetOnePressed);
            betMaxButton.onClick.AddListener(OnBetMaxPressed);
            
            UpdateDealButtonText(true);
        }

        private void OnDealPressed()
        {
            isFirstDeal = !isFirstDeal;
            UpdateDealButtonText(isFirstDeal);
            SetBettingEnabled(isFirstDeal);
            // We'll hook up the actual dealing logic later
        }

        private void OnBetOnePressed()
        {
            // We'll implement betting logic later
        }

        private void OnBetMaxPressed()
        {
            // We'll implement max betting logic later
        }

        private void UpdateDealButtonText(bool isFirstDeal)
        {
            dealButtonText.text = isFirstDeal ? "DEAL" : "DRAW";
        }

        private void SetBettingEnabled(bool enabled)
        {
            betOneButton.interactable = enabled;
            betMaxButton.interactable = enabled;
        }
    }
}
