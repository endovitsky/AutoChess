using Managers;
using UnityEngine;

namespace Views
{
    public class ChessBoardView : MonoBehaviour
    {
        private void Start()
        {
            GameManager.Instance.SquareFactory.InstantiateSquares(this.gameObject.transform);

            // set board position to center of parent container
            var xCorrection = GameManager.Instance.ChessBoardConfigurationService.Width / 2 +
                              GameManager.Instance.SquareFactory.SquareSize.x / 2;
            var yCorrection = GameManager.Instance.ChessBoardConfigurationService.Height / 2 +
                              GameManager.Instance.SquareFactory.SquareSize.y / 2;
            this.gameObject.transform.localPosition = new Vector3(
                this.gameObject.transform.localPosition.x - xCorrection,
                this.gameObject.transform.localPosition.y - yCorrection);
        }
    }
}
