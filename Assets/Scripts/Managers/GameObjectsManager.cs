using UnityEngine;

namespace Managers
{
    public class GameObjectsManager : MonoBehaviour
    {
        public void Initialize()
        {
            GameManager.Instance.ChessBoardFactory.InstantiateChessBoard(this.gameObject.transform);
        }
    }
}
