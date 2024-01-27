using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scenes.Scripts
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] private Player player1 = null;
        [SerializeField] private Player player2 = null;
        [SerializeField] private ClickDetector clickDetector = null;
        [SerializeField] private HUD hud;
        [SerializeField] private Tile tile;

        private List<Player> players;
        
        private int currentPlayerIndex = 0;

        private float tileSize;

        private bool isMovementPhase = false;
        private CharacterToken currentCharacterToken = null;

        private void Start()
        {
            players = new List<Player>{ player1, player2 };
            clickDetector.OnValidClick += OnValidClick;
            hud.getSpawnButton().onClick.AddListener(handleSpawnButtonClicked);
        }

        public void OnValidClick(GameObject gameObject)
        {
            Tile tile = gameObject.GetComponent<Tile>();
            if (tile != null)
            {
                if (isMovementPhase)
                {
                    handleMoveToTile(tile);
                    return;
                }
                tile.Act();
                hud.InfoPanel.ShowInfo(tile);
            }

            CharacterToken characterToken = gameObject.GetComponent<CharacterToken>();
            if (characterToken != null && characterToken.GetFaction() == getCurrentPlayer().getFaction())
            {
                isMovementPhase = true;
                currentCharacterToken = characterToken;
            }
        }

        private Player getCurrentPlayer()
        {
            return players[currentPlayerIndex];
        }
        
        private void handleSpawnButtonClicked()
        {
            getCurrentPlayer().spawnCharacterToken();
        }

        private void handleMoveToTile(Tile tile)
        {
            Vector3 tokenTransform = currentCharacterToken.transform.position;
            Vector3 tileToMoveTransform = tile.transform.position;

            if (Vector3.Distance(tokenTransform, tileToMoveTransform) == 1)
            {
                currentCharacterToken.moveToDestination(tileToMoveTransform);
                return;
            }
        }
    }
}