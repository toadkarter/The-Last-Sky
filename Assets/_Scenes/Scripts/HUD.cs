using System;
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


        private int numberOfMoves = 0;
        private int chemAmount = 0;
        private string chemTextLabel = "Chem: ";
        private int plantAmount = 0;
        private string plantTextLabel = "Plant: ";
        private int guanoAmount = 0;
        private string guanoTextLabel = "Guano: ";

        private void Start()
        {
            infoPanel.SetVisibility(false);
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

    public InfoPanel InfoPanel => infoPanel;
    }
}