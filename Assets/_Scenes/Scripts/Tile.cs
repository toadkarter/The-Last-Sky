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
        [SerializeField] private Material hoverMaterial = null;
        [SerializeField] private Material clickMaterial = null;
        [SerializeField] private MeshRenderer meshRenderer = null;
        [SerializeField] private SpawnComponent spawnComponent = null;
        [SerializeField] private Resource resource = Resource.None;
        
        private List<Material> currentMaterials = new List<Material>();
        private List<Material> hoverMaterials = new List<Material>();
        private List<Material> clickMaterials = new List<Material>();
        private List<int> angles = new List<int>{0, 90, 180, 270};
        private Transform transform = null;
        private bool isOccupied = false;

        public Resource getResource()
        {
            return resource;
        }
        

        private void Start()
        {
            spawnComponent = GetComponent<SpawnComponent>();
            meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.GetMaterials(currentMaterials);

            hoverMaterials.Add(hoverMaterial);
            clickMaterials.Add(clickMaterial);
            
            foreach (Material currentMaterial in currentMaterials)
            {
                hoverMaterials.Add(currentMaterial);
                clickMaterials.Add(currentMaterial);
            }

            transform = GetComponent<Transform>();
            int index = Random.Range(0, angles.Count - 1);
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

        public void setIsOccupied(bool inIsOccupied)
        {
            isOccupied = inIsOccupied;
        }
    }
}