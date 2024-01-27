using System;
using UnityEngine;

namespace _Scenes.Scripts
{
    public class CharacterToken : MonoBehaviour, IClickable
    {
        [SerializeField] private Faction faction = Faction.Police;
        [SerializeField] private float movementSpeed = 5.0f;
        private bool isMoving = false;
        private Vector3 currentDestination = Vector3.zero;

        public void Act()
        {
            
        }

        public Faction GetFaction()
        {
            return faction;
        }

        public void moveToDestination(Vector3 destination)
        {
            currentDestination = destination;
            isMoving = true;
        }

        private void Update()
        {
            if (!isMoving)
            {
                return;
            }
            
            float step = movementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, currentDestination, step);
            if (transform.position == currentDestination)
            {
                isMoving = false;
                currentDestination = Vector3.zero;
            }
        }
    }
}