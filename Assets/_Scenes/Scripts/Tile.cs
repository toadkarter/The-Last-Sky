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
        [SerializeField] private string title = null;
        [SerializeField] private string description = null;
        [SerializeField] private bool hasInfo = false;
        
        private List<int> angles = new List<int>{0, 90, 180, 270};
        private Transform transform = null;
        private bool isOccupied = false;

        private void Start()
        {
            transform = GetComponent<Transform>();
            int index = Random.Range(0, angles.Count - 1);
            Debug.Log(angles[index]);
            transform.rotation.Set(transform.rotation.x, angles[index], transform.rotation.z, transform.rotation.w);
        }

        public void Act()
        {
            
        }

        public string getTitle()
        {
            return title;
        }

        public string getDescription()
        {
            return description;
        }

        public bool getHasUIInfo()
        {
            return hasInfo;
        }

        public bool getIsOccupied()
        {
            return isOccupied;
        }
    }
}