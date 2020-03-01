using Managers;
using UnityEngine;

namespace GameObjects
{
    public class ChessBoard : MonoBehaviour
    {
        public void Initialize()
        {
            GameManager.Instance.SquareFactory.InstantiateSquares(this.gameObject.transform);
        }
    }
}
