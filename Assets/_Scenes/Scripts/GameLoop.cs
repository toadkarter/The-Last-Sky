using System;
using UnityEngine;

namespace _Scenes.Scripts
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] private Player playerClass = null;
        [SerializeField] private ClickDetector clickDetector = null;
        [SerializeField] private HUD hud;
         
        private Player player1 = null;
        private Player player2 = null;

        private void Start()
        {
            clickDetector.OnValidClick += OnValidClick;
        }

        public void OnValidClick(GameObject gameObject)
        {
            Tile tile = gameObject.GetComponent<Tile>();
            if (tile != null)
            {
                tile.Act();
                hud.InfoPanel.ShowInfo(tile);
            }
        }
    }
}