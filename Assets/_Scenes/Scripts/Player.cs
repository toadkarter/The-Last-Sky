using System.Collections.Generic;
using UnityEngine;

namespace _Scenes.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Faction faction;
        [SerializeField] private Tile spawnerTile;
        [SerializeField] private GameObject characterTokenClass;
        [SerializeField] private int maxCharactersAtPlay = 5;
        [SerializeField] private string factionName;
        [SerializeField] private Resource extraIngredientForLaugh = Resource.None;

        public int chemAmount = 0;
        public int plantAmount = 0;
        public int guanoAmount = 0;

        private List<CharacterToken> characterTokens = new List<CharacterToken>();

        public Resource getExtraIngredientForLaugh()
        {
            return extraIngredientForLaugh;
        }
        
        public List<CharacterToken> getCharacterTokens()
        {
            return characterTokens;
        }

        public void killCharacter(CharacterToken characterToken)
        {
            characterTokens.Remove(characterToken);
            Destroy(characterToken.gameObject);
            maxCharactersAtPlay--;
        }
        
        public Faction getFaction()
        {
            return faction;
        }

        public string getFactionName()
        {
            return factionName;
        }

        public void spawnCharacterToken()
        {
            if (characterTokens.Count + 1 > maxCharactersAtPlay || spawnerTile.getIsOccupied())
            {
                // TODO: Add some sort of error notification saying that you are trying to spawn too many players.
                Debug.Log("Illegal Action");
                return;
            }
            GameObject gameObject = Instantiate(characterTokenClass, spawnerTile.transform);
            CharacterToken characterToken = gameObject.GetComponent<CharacterToken>();
            spawnerTile.setIsOccupied(true);
            characterTokens.Add(characterToken);
            characterToken.setCurrentTile(spawnerTile);
        }
    }
}