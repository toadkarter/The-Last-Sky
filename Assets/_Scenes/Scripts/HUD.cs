using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scenes.Scripts
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] public InfoPanel infoPanel = null;
        [SerializeField] public Button spawnButton = null;
        [SerializeField] public TextMeshProUGUI numberOfMovesText = null;
        [SerializeField] public string numberOfMovesLabel = "Sanctioned Moves: ";
        [SerializeField] public TextMeshProUGUI factionNameText = null;
        [SerializeField] public Button endTurnButton = null;
        [SerializeField] public TextMeshProUGUI plantText = null;
        [SerializeField] public TextMeshProUGUI chemText = null;
        [SerializeField] public TextMeshProUGUI guanoText = null;
        [SerializeField] public Button harvestButton = null;
        [SerializeField] public LaughPanel laughPanel = null;
        [SerializeField] public TextMeshProUGUI playerTurnText = null;
        [SerializeField] public LaughMeter laughMeter = null;


        private List<LaughOutcome> laughOutcomes = new List<LaughOutcome>()
            { LaughOutcome.None, LaughOutcome.Failure, LaughOutcome.Success };
        private int numberOfMoves = 0;
        private int chemAmount = 0;
        private string chemTextLabel = "Clover: ";
        private int plantAmount = 0;
        private string plantTextLabel = "Grass: ";
        private int guanoAmount = 0;
        private string guanoTextLabel = "Popcorn: ";

        public bool playerTurnChange = false;
        private bool turnChangeTextIsDelaying = false;
        private bool turnChangeTextIsReversing = false;

        public LaughPanel getLaughPanel()
        {
            return laughPanel;
        }
        
        private void Start()
        {
            playerTurnText.alpha = 0f;
            infoPanel.SetVisibility(false);
            harvestButton.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (playerTurnChange)
            {
                playerTurnText.alpha += Time.deltaTime / 0.5f;
                
                if (playerTurnText.alpha >= 1.0)
                {
                    StartCoroutine(Delay());
                    playerTurnChange = false;
                    turnChangeTextIsReversing = true;
                }
            }
            else if (turnChangeTextIsReversing)
            {
                playerTurnText.alpha -= Time.deltaTime / 0.5f;

                if (playerTurnText.alpha <= 0.0)
                {
                    turnChangeTextIsReversing = false;
                }
            }
        }

        public Button getSpawnButton()
        {
            return spawnButton;
        }

        public void setNumberOfMovesText(int numberOfMoves)
        {
            this.numberOfMoves = numberOfMoves;
            numberOfMovesText.text = numberOfMovesLabel + numberOfMoves.ToString();
        }

        public int getNumberOfMoves()
        {
            return numberOfMoves;
        }

        public Button getHarvestButton()
        {
            return harvestButton;
        }

        public void setFactionName(string factionName)
        {
            factionNameText.text = factionName;
        }

        public Button getEndTurnButton()
        {
            return endTurnButton;
        }

        public void setChemAmount(int amount)
        {
            chemText.text = chemTextLabel + amount;
            chemAmount = amount;
        }

        public void setPlantAmount(int amount)
        {
            plantText.text = plantTextLabel + amount;
            plantAmount = amount;
        }

        public int getChemAmount()
        {
            return chemAmount;
        }

        public int getGuanoAmount()
        {
            return guanoAmount;
        }

        public void setGuanoAmount(int amount)
        {
            guanoText.text = guanoTextLabel + amount;
            guanoAmount = amount;
        }

        public int getPlantAmount()
        {
            return plantAmount;
        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(1f);
        }

    public InfoPanel InfoPanel => infoPanel;
    }
}