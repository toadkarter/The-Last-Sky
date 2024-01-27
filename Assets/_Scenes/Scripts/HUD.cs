using System;
using UnityEngine;

namespace _Scenes.Scripts
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] public InfoPanel infoPanel = null;

        private void Start()
        {
            infoPanel.SetVisibility(false);
        }

        public InfoPanel InfoPanel => infoPanel;
    }
}