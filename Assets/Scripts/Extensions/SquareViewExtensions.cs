using Managers;
using Models;
using Views;

namespace Extensions
{
    public static class SquareViewExtensions
    {
        public static UnitModel GetUnitModelForSquareView(this SquareView squareView)
        {
            var result = GameManager.Instance.UnitFactory.GetUnitViewByPosition(squareView.Position);

            if (result == null)
            {
                return null;
            }

            return result.UnitModel;
        }
    }
}
