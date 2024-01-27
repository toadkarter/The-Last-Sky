using System;
using UnityEngine;

namespace _Scenes.Scripts
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] private Player playerClass = null;
        [SerializeField] private ClickDetector clickDetector = null;
         
        private Player player1 = null;
        private Player player2 = null;

        private void Start()
        {
            clickDetector.OnValidClick += OnValidClick;
        }

        public void OnValidClick(GameObject gameObject)
        {
            Debug.Log(gameObject);
        }
    }
}