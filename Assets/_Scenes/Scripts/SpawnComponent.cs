using UnityEngine;

namespace _Scenes.Scripts
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private GameObject characterTokenClass = null;

        public void spawnCharacterToken()
        {
            Instantiate(characterTokenClass, transform);
        }
    }
}