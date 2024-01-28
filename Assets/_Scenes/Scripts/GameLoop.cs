using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = System.Random;

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
        
        private int currentPlayerIndex = 1;

        private float tileSize;

        private Random random = new Random();

        private bool isMovementPhase = false;
        private bool isLaughPhase = false;

        private List<LaughOutcome> laughOutcomes = new List<LaughOutcome>() { LaughOutcome.Failure, LaughOutcome.None, LaughOutcome.Success };
        
        private CharacterToken currentCharacterToken = null;

        private void Start()
        {
            players = new List<Player>{ player1, player2 };
            clickDetector.OnValidClick += OnValidClick;
            hud.getSpawnButton().onClick.AddListener(handleSpawnButtonClicked);
            hud.getEndTurnButton().onClick.AddListener(handleEndTurnButton);
            hud.getHarvestButton().onClick.AddListener(handlePressResourceButton);
            
            initializeTurn();
        }

        public void OnValidClick(GameObject gameObject)
        {
            if (isLaughPhase)
            {
                CharacterToken enemyToken = gameObject.GetComponent<CharacterToken>();
                if (enemyToken != null && enemyToken.GetFaction() != getCurrentPlayer().getFaction())
                {
                    HandleLaughPhase(enemyToken);
                }

                return;
            }
            
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

        void HandleLaughPhase(CharacterToken enemyToken)
        {
            hud.setGuanoAmount(hud.getGuanoAmount() - 1);
            if (getCurrentPlayer().getExtraIngredientForLaugh() == Resource.Chem)
            {
                hud.setChemAmount(hud.getChemAmount() - 1);
            }
            else if (getCurrentPlayer().getExtraIngredientForLaugh() == Resource.Plant)
            {
                hud.setPlantAmount(hud.getPlantAmount() - 1);
            }
            
            int outcomeIndex = random.Next(laughOutcomes.Count - 1);
            LaughOutcome outcome = laughOutcomes[outcomeIndex];

            switch (outcome)
            {
                case LaughOutcome.None:
                {
                    hud.laughPanel.ShowOutcomeState(outcome);   
                    break;
                }
                case LaughOutcome.Success:
                {
                    getEnemyPlayer().killCharacter(enemyToken);
                    hud.laughPanel.ShowOutcomeState(outcome);   
                    break;
                }
                case LaughOutcome.Failure:
                {
                    HandleFailureState(enemyToken);
                    break;
                }
            }
            
            isLaughPhase = false;
            handleEndTurnButton();
        }

        void HandleFailureState(CharacterToken enemyToken)
        {
            if (enemyToken.getCurrentTile().getFactionLocation() == getCurrentPlayer().getFaction())
            {
                hud.laughPanel.ShowOutcomeState(LaughOutcome.Failure);
            }
            else
            {
                if (getCurrentPlayer().getExtraIngredientForLaugh() == Resource.Chem)
                {
                    if (getEnemyPlayer().chemAmount >= 1)
                    {
                        getEnemyPlayer().chemAmount -= 1;
                        hud.laughPanel.ShowOutcomeState(LaughOutcome.FailureBribe);
                        return;
                    }
                }
                else if (getCurrentPlayer().getExtraIngredientForLaugh() == Resource.Plant)
                {
                    if (getEnemyPlayer().plantAmount >= 1)
                    {
                        getEnemyPlayer().plantAmount -= 1;
                        hud.laughPanel.ShowOutcomeState(LaughOutcome.FailureBribe);
                        return;
                    }
                }
                
                hud.laughPanel.ShowOutcomeState(LaughOutcome.FailureNoBribe);
            }
        }

        private Player getCurrentPlayer()
        {
            return players[currentPlayerIndex];
        }

        private Player getEnemyPlayer()
        {
            return currentPlayerIndex == 0 ? players[1] : players[0];
        }
        
        private void handleSpawnButtonClicked()
        {
            getCurrentPlayer().spawnCharacterToken();
        }

        private void handleMoveToTile(Tile tile)
        {
            if (hud.getNumberOfMoves() <= 0)
            {
                return;
            }
            
            Vector3 tokenTransform = currentCharacterToken.transform.position;
            Vector3 tileToMoveTransform = tile.transform.position;

            if (Vector3.Distance(tokenTransform, tileToMoveTransform) == 1)
            {
                currentCharacterToken.getCurrentTile().setIsOccupied(false);
                currentCharacterToken.moveToDestination(tileToMoveTransform);
                hud.setNumberOfMovesText(hud.getNumberOfMoves() - 1);
                currentCharacterToken.setCurrentTile(tile);
                tile.setIsOccupied(true);
                if (tile.getResource() != Resource.None)
                {
                    hud.getHarvestButton().gameObject.SetActive(true);
                }
                else
                {
                    hud.getHarvestButton().gameObject.SetActive(false);
                }

                if (tile.getIsManufacturer() && playerHasEnoughForLaugh())
                {
                    isLaughPhase = true;
                    hud.laughPanel.ShowDefaultState();
                }

                if (tile.getIsOccupied() && playerOnSameTileAsEnemy(tile))
                {
                    if (getEnemyPlayer().guanoAmount >= 1)
                    {
                        getEnemyPlayer().guanoAmount -= 1;
                        getCurrentPlayer().guanoAmount += 1;
                    }
                }
            }
        }

        public void initializeTurn()
        {
            switchPlayers();
            initializeSanctionsMoves();
            hud.setFactionName(getCurrentPlayer().getFactionName());
            hud.playerTurnChange = true;
            loadResourcesAmount(hud.getChemAmount(), hud.getGuanoAmount(), hud.getPlantAmount());
        }

        public void storeResourcesAmount(int chemAmount, int guanoAmount, int plantAmount)
        {
            getCurrentPlayer().chemAmount = chemAmount;
            getCurrentPlayer().guanoAmount = guanoAmount;
            getCurrentPlayer().plantAmount = plantAmount;
        }

        public void loadResourcesAmount(int chemAmount, int guanoAmount, int plantAmount)
        {
            hud.setChemAmount(chemAmount);
            hud.setGuanoAmount(guanoAmount);
            hud.setPlantAmount(plantAmount);
        }

        private void switchPlayers()
        {
            currentPlayerIndex = currentPlayerIndex == 0 ? 1 : 0;
        }

        private void initializeSanctionsMoves()
        {
            int sanctionedMoves = random.Next(1, 6);
            hud.setNumberOfMovesText(sanctionedMoves);
        }

        private void handleEndTurnButton()
        {
            isMovementPhase = false;
            storeResourcesAmount(hud.getChemAmount(), hud.getGuanoAmount(), hud.getPlantAmount());
            hud.harvestButton.gameObject.SetActive(false);
            initializeTurn();
        }

        private void handlePressResourceButton()
        {
            if (currentCharacterToken == null)
            {
                Debug.Log("Something went wrong, this is not a valid state.");
                return;
            }

            switch (currentCharacterToken.getCurrentTile().getResource())
            { 
                case Resource.Chem:
                    if (hud.getChemAmount() >= 1)
                    {
                        break;
                    }
                    hud.setChemAmount(hud.getChemAmount() + 1);
                    break;
                case Resource.Guano:
                    if (hud.getGuanoAmount() >= 1)
                    {
                        break;
                    }
                    hud.setGuanoAmount(hud.getGuanoAmount() + 1);
                    break;
                case Resource.Plant:
                    if (hud.getPlantAmount() >= 1)
                    {
                        break;
                    }
                    hud.setPlantAmount(hud.getPlantAmount() + 1);
                    break;
                case Resource.None: 
                default:
                    break;
            }
        }

        private bool playerHasEnoughForLaugh()
        {
            if (hud.getGuanoAmount() <= 0)
            {
                return false;
            }

            switch (getCurrentPlayer().getExtraIngredientForLaugh())
            {
                case Resource.Chem:
                    if (hud.getChemAmount() >= 1)
                    {
                        return true;
                    }

                    break;
                case Resource.Plant:
                    if (hud.getPlantAmount() >= 1)
                    {
                        return true;
                    }

                    break;
                
                case Resource.Guano:
                case Resource.None:
                default:
                    break;
                
            }

            return false;
        }

        private bool playerOnSameTileAsEnemy(Tile tile)
        {
            foreach (CharacterToken enemyToken in getEnemyPlayer().getCharacterTokens())
            {
                if (currentCharacterToken.getCurrentTile() == enemyToken.getCurrentTile())
                {
                    return true;
                }
            }

            return false;
        }
    }
}