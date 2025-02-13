using UnityEngine;
using UnityEngine.UI;
using TMPro;
using VideoPoker.Game;   // Add this line to reference GameManager

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

        [SerializeField] private GameManager gameManager;

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
            gameManager.OnDealPressed();
        }

        private void OnBetOnePressed()
        {
            gameManager.OnBetOnePressed();
        }

        private void OnBetMaxPressed()
        {
            gameManager.OnBetMaxPressed();
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
