using Managers;
using Models;
using Views;

namespace Extensions
{
    public static class UnitModelExtensions
    {
        public static SquareView GetSquareViewForUnitModel(this UnitModel unitModel)
        {
            var result = GameManager.Instance.SquareFactory.GetSquareViewByPosition(unitModel.Position);

            return result;
        }
    }
}
