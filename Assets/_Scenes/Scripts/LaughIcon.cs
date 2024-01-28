using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scenes.Scripts
{
    public class LaughIcon : MonoBehaviour
    {
        [SerializeField] private Faction faction = Faction.Police;
        [SerializeField] private Image currentImage = null;
        [SerializeField] private Color policeColor;
        [SerializeField] private Color terroristColor;

        public void SetOnState()
        {
            if (faction == Faction.Police)
            {
                currentImage.color = policeColor;
            }
            else if (faction == Faction.Terrorist)
            {
                currentImage.color = terroristColor;
            }
            else if (faction == Faction.Unaligned)
            {
                currentImage.color = Color.yellow;
            }
        }
    }
}