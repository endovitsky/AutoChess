using Managers;
using UnityEngine;

namespace Views
{
    public class ChessBoardView : MonoBehaviour
    {
        public void Initialize()
        {
            GameManager.Instance.SquareFactory.InstantiateSquares(this.gameObject.transform);
        }
    }
}
