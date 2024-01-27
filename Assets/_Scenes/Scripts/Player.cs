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

        private List<CharacterToken> characterTokens = new List<CharacterToken>();

        public Faction getFaction()
        {
            return faction;
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
        }
    }
}