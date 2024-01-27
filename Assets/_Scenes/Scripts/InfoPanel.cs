using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scenes.Scripts
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button spawnButton;

        private Tile currentTile = null;

        private void Start()
        {
            spawnButton.gameObject.SetActive(false);
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        }

        public void SetVisibility(bool on)
        {
            gameObject.SetActive(on);
        }

        public void ShowInfo(Tile tile)
        {
            if (tile == null)
            {
                return;
            }

            if (!tile.getHasUIInfo())
            {
                return;
            }
            
            SetVisibility(true);
            title.SetText(tile.getTitle());
            description.SetText(tile.getDescription());

            if (tile.GetComponent<SpawnComponent>() != null)
            {
                spawnButton.gameObject.SetActive(true);
                spawnButton.onClick.AddListener(OnSpawnButtonClicked);
            }

            currentTile = tile;
        }

        public void OnQuitButtonClicked()
        {
            SetVisibility(false);
            currentTile = null;
        }

        public void OnSpawnButtonClicked()
        {
            spawnButton.onClick.RemoveAllListeners();
            spawnButton.gameObject.SetActive(false);
            
            SpawnComponent spawnComponent = currentTile.GetComponent<SpawnComponent>();
            spawnComponent.spawnCharacterToken();
            
            OnQuitButtonClicked();
        }
    }
}