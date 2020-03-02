using System.Collections.Generic;
using Managers;
using Views;

namespace Services
{
    public class UnitsMovingService
    {
        public void Initialize()
        {
            GameManager.Instance.UnitsPathProvidingService.PathsChanged += OnPathsChanged;
        }

        private void OnPathsChanged(List<List<SquareView>> paths)
        {
            foreach (var path in paths)
            {
                var unitView = path[0].UnitView;
                var squareView = path[1];
                unitView.UnitModel.Move(squareView);
            }
        }
    }
}
