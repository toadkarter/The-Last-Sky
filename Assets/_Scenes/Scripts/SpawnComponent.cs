using UnityEngine;

namespace _Scenes.Scripts
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private GameObject characterTokenClass = null;

        public CharacterToken spawnCharacterToken()
        {
            GameObject characterToken = Instantiate(characterTokenClass, transform);
            return characterToken.GetComponent<CharacterToken>();
        }
    }
}