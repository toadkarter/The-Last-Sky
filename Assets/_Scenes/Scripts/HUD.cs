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

        private int numberOfMoves = 0;
        
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

        public InfoPanel InfoPanel => infoPanel;
    }
}