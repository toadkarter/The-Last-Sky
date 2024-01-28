using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scenes.Scripts
{
    public class LaughPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI killPanelTitleText = null;
        [SerializeField] private TextMeshProUGUI killPanelDescriptionText = null;
        [SerializeField] private Button continueButton;

        [SerializeField] private string title;
        [SerializeField] private string description;
        
        [SerializeField] private string successTitle;
        [SerializeField] private string successDescriptionPolice;
        [SerializeField] private string successDescriptionTerrorist;
        [SerializeField] private string failureBribeTitle;
        [SerializeField] private string failureBribeDescriptionPolice;
        [SerializeField] private string failureBribeDescriptionTerrorist;
        [SerializeField] private string failureNoBribeTitle;
        [SerializeField] private string failureNoBribeDescriptionPolice;
        [SerializeField] private string failureNoBribeDescriptionTerrorist;
        [SerializeField] private string failureTitle;
        [SerializeField] private string failureDescriptionPolice;
        [SerializeField] private string failureDescriptionTerrorist;
        [SerializeField] private string noneTitle;
        [SerializeField] private string noneDescriptionPolice;
        [SerializeField] private string noneDescriptionTerrorist;
        [SerializeField] private string winnerTitle;
        [SerializeField] private string winnerDescriptionPolice;
        [SerializeField] private string winnerDescriptionTerrorist;

        private bool isQuitting = false;
        
        Dictionary<LaughOutcome, List<string>> outcomeDescription = new Dictionary<LaughOutcome, List<string>>();

        private void Start()
        {
            continueButton.onClick.AddListener(handleContinueButtonPressed);
            gameObject.SetActive(false);
            outcomeDescription = new Dictionary<LaughOutcome, List<string>>()
            {
                { LaughOutcome.None, new List<string>{noneTitle, noneDescriptionPolice, noneDescriptionTerrorist} },
                { LaughOutcome.FailureBribe, new List<string>{failureBribeTitle, failureBribeDescriptionPolice, failureBribeDescriptionTerrorist} },
                { LaughOutcome.FailureNoBribe, new List<string>{failureNoBribeTitle, failureNoBribeDescriptionPolice, failureNoBribeDescriptionTerrorist} },
                { LaughOutcome.Failure, new List<string>{failureTitle, failureDescriptionPolice, failureDescriptionTerrorist} },
                { LaughOutcome.Success, new List<string>{successTitle, winnerDescriptionPolice, winnerDescriptionTerrorist} },
            };
        }

        private void handleContinueButtonPressed()
        {
            if (isQuitting)
            {
                Application.Quit();
                return;
            }
            gameObject.SetActive(false);
        }

        public void ShowDefaultState()
        {
            gameObject.SetActive(true);
            killPanelTitleText.text = title;
            killPanelDescriptionText.text = description;
        }

        public void ShowOutcomeState(LaughOutcome outcome, Faction faction)
        {
            gameObject.SetActive(true);
            killPanelTitleText.text = outcomeDescription[outcome][0];

            string descriptionToSet = outcomeDescription[outcome][1];
            if (faction == Faction.Terrorist)
            {
                descriptionToSet = outcomeDescription[outcome][2];
            }

            killPanelDescriptionText.text = descriptionToSet;
        }

        public void ShowWinnerState(Faction faction)
        {
            isQuitting = true;
            gameObject.SetActive(true);
            killPanelTitleText.text = winnerTitle;

            string descriptionToSet = successDescriptionPolice;
            if (faction == Faction.Terrorist)
            {
                descriptionToSet = successDescriptionTerrorist;
            }
            
            killPanelDescriptionText.text = descriptionToSet;
        }
    }
}