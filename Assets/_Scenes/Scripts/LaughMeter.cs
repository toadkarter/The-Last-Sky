using System.Collections.Generic;
using UnityEngine;

namespace _Scenes.Scripts
{
    public class LaughMeter : MonoBehaviour
    {
        [SerializeField] private List<LaughIcon> policeLaughIcons;
        [SerializeField] private List<LaughIcon> terroristLaughIcons;
        [SerializeField] private LaughIcon winnerLaughIcon;

        private int currentPoliceLaughIndex = 0;
        private int currentTerroristLaughIndex = 0;

        public bool isWinner = false;

        public void increasePoliceLaughIndex()
        {
            policeLaughIcons[currentPoliceLaughIndex].SetOnState();
            if (currentPoliceLaughIndex + 1 < policeLaughIcons.Count)
            {
                currentPoliceLaughIndex += 1;
            }
            else
            {
                winnerLaughIcon.SetOnState();
                isWinner = true;
            }
        }

        public void increaseTerroristLaughIndex()
        {
            terroristLaughIcons[currentTerroristLaughIndex].SetOnState();
            if (currentTerroristLaughIndex + 1 < terroristLaughIcons.Count)
            {
                currentTerroristLaughIndex += 1;
            }
            else
            {
                winnerLaughIcon.SetOnState();
                isWinner = true;
            }
        }
    }
}