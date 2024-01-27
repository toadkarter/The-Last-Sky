using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Scenes.Scripts
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private Button quitButton;
        
        public void SetVisibility(bool on)
        {
            gameObject.SetActive(on);
        }

        public void ShowInfo(Tile tile)
        {
            if (tile == null)
            {
                return;
            }

            if (!tile.getHasUIInfo())
            {
                return;
            }
            
            SetVisibility(true);
            title.SetText(tile.getTitle());
            description.SetText(tile.getDescription());
        }
    }
}