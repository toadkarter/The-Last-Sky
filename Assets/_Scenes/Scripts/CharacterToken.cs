using UnityEngine;

namespace _Scenes.Scripts
{
    public class CharacterToken : MonoBehaviour, IClickable
    {
        [SerializeField] private Faction faction = Faction.Police;
        private Tile currentTile = null;
        
        public void Act()
        {
            
        }

        public void SetCurrentTile(Tile inTile)
        {
            currentTile = inTile;
        }
    }
}