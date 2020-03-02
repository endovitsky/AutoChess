using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using Views;

namespace Services
{
    public class PathFindingService
    {
        public List<SquareView> FindPath(SquareView startSquareView, SquareView targetSquareView)
        {
            var path = new List<SquareView>();

            var openSet = new List<SquareView>();
            var closedSet = new HashSet<SquareView>();
            openSet.Add(startSquareView);

            while (openSet.Count > 0)
            {
                var squareView = openSet[0];
                for (var i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < squareView.fCost || 
                        openSet[i].fCost == squareView.fCost)
                    {
                        if (openSet[i].hCost < squareView.hCost)
                            squareView = openSet[i];
                    }
                }

                openSet.Remove(squareView);
                closedSet.Add(squareView);

                if (squareView == targetSquareView)
                {
                    path = RetracePath(startSquareView, targetSquareView);

                    path.Insert(0, startSquareView); // path include start point

                    return path;
                }

                var neighbors = GetNeighbors(squareView);

                var unWalkableSquareViews = neighbors.Where(x => !x.IsWalkable).ToList();
                unWalkableSquareViews.Remove(targetSquareView); // target do not count as un walkable
                var filteredNeighbors = neighbors.Except(unWalkableSquareViews).ToList();
                filteredNeighbors = filteredNeighbors.Except(closedSet).ToList();

                foreach (var neighbor in filteredNeighbors)
                {
                    var newCostToNeighbor = squareView.gCost + GetDistance(squareView, neighbor);
                    if (newCostToNeighbor < neighbor.gCost || 
                        !openSet.Contains(neighbor))
                    {
                        neighbor.gCost = newCostToNeighbor;
                        neighbor.hCost = GetDistance(neighbor, targetSquareView);
                        neighbor.parent = squareView;

                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }

            return path;
        }

        private List<SquareView> RetracePath(SquareView startSquareView, SquareView endSquareView)
        {
            var path = new List<SquareView>();

            var currentSquareView = endSquareView;

            while (currentSquareView != startSquareView)
            {
                path.Add(currentSquareView);
                currentSquareView = currentSquareView.parent;
            }
            path.Reverse();

            return path;
        }

        private int GetDistance(SquareView squareView1, SquareView squareView2)
        {
            var distance = 0;

            var dstX = Mathf.Abs((int)squareView1.Position.x - (int)squareView2.Position.x);
            var dstY = Mathf.Abs((int)squareView1.Position.y - (int)squareView2.Position.y);

            distance = dstX > dstY ? 
                dstX : 
                dstY;

            return distance;
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

                // from coordinates to indexes
                var indexX = x - 1;
                var indexY = y - 1;

                if (indexX < 0) // too left
                {
                    continue;
                }
                if (GameManager.Instance.SquareFactory.SquareInstances.Count <= indexX) // too right
                {
                    continue;
                }
                if (GameManager.Instance.SquareFactory.SquareInstances[indexX].Count <= indexY) // too up
                {
                    continue;
                }
                if (indexY < 0) // too down
                {
                    continue;
                }

                var squareInstance = GameManager.Instance.SquareFactory.SquareInstances[indexX][indexY];
                result.Add(squareInstance);
            }

            return result;
        }
    }
}
