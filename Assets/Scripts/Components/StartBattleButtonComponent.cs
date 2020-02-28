using Managers;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    [RequireComponent(typeof(Button))]
    public class StartBattleButtonComponent : MonoBehaviour
    {
        private void Start()
        {
            this.gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
        }
        private void OnDestroy()
        {
            this.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        }

        private void OnClick()
        {
            GameManager.Instance.GameStateService.SelectedGameState = GameStateService.GameState.Started;
        }
    }
}
