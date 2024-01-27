using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace _Scenes.Scripts
{
    public class Tile : MonoBehaviour, IClickable
    {
        private List<int> angles = new List<int>{0, 90, 180, 270};
        private Transform transform = null;

        private void Start()
        {
            transform = GetComponent<Transform>();
            int index = Random.Range(0, angles.Count - 1);

            throw new NotImplementedException();
        }

        public void Act()
        {
            
        }
    }
}