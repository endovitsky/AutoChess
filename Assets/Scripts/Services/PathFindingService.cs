using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Models;
using UnityEngine;
using Views;

namespace Services
{
    public class PathFindingService
    {
        private List<SquareView> _passedSquareViews = new List<SquareView>();

        public UnitModel GetClosestUnitOfTeam(UnitModel thisUnitModel, string teamName)
        {
            UnitModel result = null;

            var unitModels = GameManager.Instance.UnitsStateMonitoringService.GetAliveUnitModelsForTeam(teamName);
            var unitModelsPathLengths = new Dictionary<UnitModel, int>();
            foreach (var unitModel in unitModels)
            {
                var path = GetPath(thisUnitModel.SquareView, unitModel.SquareView);
                var pathLength = GetPathLength(path);
                unitModelsPathLengths.Add(unitModel, pathLength);
            }

            result = unitModelsPathLengths.OrderBy(x=>x.Value).First().Key;

            return result;
        }

        public List<SquareView> GetPath(SquareView startSquareView, SquareView targetSquareView)
        {
            var path = new List<SquareView>();

            _passedSquareViews = new List<SquareView>();

            // mark start square as visited
            _passedSquareViews.Add(startSquareView);

            // add start square to path
            path.Add(startSquareView);

            var additionalPath = GetPathRecursively(startSquareView, targetSquareView);
            path.AddRange(additionalPath);

            _passedSquareViews.Clear();

            return path;
        }

        private List<SquareView> GetPathRecursively(SquareView startSquareView, SquareView targetSquareView)
        {
            var path = new List<SquareView>();

            var neighbors = GetNeighbors(startSquareView);
            neighbors = neighbors.Except(_passedSquareViews).ToList();
            foreach (var neighbor in neighbors)
            {
                // already visited this square - nothing to do here
                if (_passedSquareViews.Contains(neighbor))
                {
                    continue;
                }

                // mark square as visited
                _passedSquareViews.Add(neighbor);

                // add square to path
                path.Add(neighbor);

                // this is target square - path is found - return it
                if (neighbor == targetSquareView)
                {
                    return path;
                }

                // this is not a target square - go forth
                var additionalPath = GetPath(neighbor, targetSquareView);
                path.AddRange(additionalPath);
            }

            return path;
        }

        private List<SquareView> GetNeighbors(SquareView squareView)
        {
            var result = new List<SquareView>();

            var coordinates = new List<Vector2>();
            var position = squareView.gameObject.transform.localPosition;
            coordinates.Add(new Vector2(position.x - 1, position.y)); // left
            coordinates.Add(new Vector2(position.x + 1, position.y)); // right
            coordinates.Add(new Vector2(position.x, position.y - 1)); // bottom
            coordinates.Add(new Vector2(position.x, position.y + 1)); // top
            coordinates.Add(new Vector2(position.x - 1, position.y - 1)); // left bottom
            coordinates.Add(new Vector2(position.x - 1, position.y + 1)); // left top
            coordinates.Add(new Vector2(position.x + 1, position.y + 1)); // right top
            coordinates.Add(new Vector2(position.x + 1, position.y - 1)); // right bottom

            foreach (var vector2 in coordinates)
            {
                var x = System.Math.Abs((int)vector2.x);
                var y = System.Math.Abs((int)vector2.y);

                if (GameManager.Instance.SquareFactory.SquareInstances.Count > x &&
                    GameManager.Instance.SquareFactory.SquareInstances[x].Count > y)
                {
                    var squareInstance = GameManager.Instance.SquareFactory.SquareInstances[x][y];
                    result.Add(squareInstance);
                }
            }

            return result;
        }

        private int GetPathLength(List<SquareView> path)
        {
            var result = 0;

            foreach (var squareView in path)
            {
                result++;
            }

            return result;
        }
    }
}
