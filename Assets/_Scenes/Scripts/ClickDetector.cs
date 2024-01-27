using System;
using UnityEngine;

namespace _Scenes.Scripts
{
    public class ClickDetector : MonoBehaviour
    {
        private Camera camera;

        public delegate void ValidClickCallback(GameObject gameObject);

        public event ValidClickCallback OnValidClick;

        private void Start()
        {
            camera = GetComponent<Camera>();
            if (camera == null)
            {
                Debug.Log("Could not find camera");
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit raycastHit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out raycastHit, 100f))
                {
                    if (raycastHit.transform == null)
                    {
                        return;
                    }
                    
                    IClickable clickableObject = raycastHit.transform.gameObject.GetComponent<IClickable>();

                    if (clickableObject != null)
                    {
                        OnValidClick(raycastHit.transform.gameObject);
                    }
                }
            }
        }
    }
}