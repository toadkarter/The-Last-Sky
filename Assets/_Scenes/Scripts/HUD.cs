using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scenes.Scripts
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] public InfoPanel infoPanel = null;
        [SerializeField] public Button spawnButton = null;

        private void Start()
        {
            infoPanel.SetVisibility(false);
            
        }

        public Button getSpawnButton()
        {
            return spawnButton;
        }

        public InfoPanel InfoPanel => infoPanel;
    }
}