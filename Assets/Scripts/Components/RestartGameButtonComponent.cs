using Managers;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Components
{
    [RequireComponent(typeof(Button))]
    public class RestartGameButtonComponent : MonoBehaviour
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
            GameManager.Instance.GameStateService.SelectedGameState = GameStateService.GameState.NotStarted;

            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
