using UnityEngine;
using System.Collections.Generic;

namespace PathFind
{
    /// <summary>
    /// The grid of nodes we use to find path
    /// </summary>
    public class Grid
    {
        public Node[,] nodes;
        int gridSizeX, gridSizeY;

        /// <summary>
        /// Create a new grid with tile prices.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="tiles_costs">2d array of floats, representing the cost of every tile.</param>
        public Grid(int width, int height, float[,] tiles_costs)
        {
            gridSizeX = width;
            gridSizeY = height;
            nodes = new Node[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    nodes[x, y] = new Node(tiles_costs[x, y], x, y);

                }
            }
        }

        /// <summary>
        /// Create a new grid of just walkable / unwalkable.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="walkable_tiles">The tilemap. true for walkable, false for blocking.</param>
        public Grid(int width, int height, bool[,] walkable_tiles)
        {
            gridSizeX = width;
            gridSizeY = height;
            nodes = new Node[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    nodes[x, y] = new Node(walkable_tiles[x, y] ? 0.0f : 1.0f, x, y);
                }
            }
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    {
                        neighbours.Add(nodes[checkX, checkY]);
                    }
                }
            }

            return neighbours;
        }
    }
}