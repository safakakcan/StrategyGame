namespace PathFind
{
    /// <summary>
    /// A node in the grid map
    /// </summary>
    public class Node
    {
        // node starting params
        public bool walkable;
        public int gridX;
        public int gridY;
        public float penalty;

        // calculated values while finding path
        public int gCost;
        public int hCost;
        public Node parent;

        public Node(float _price, int _gridX, int _gridY)
        {
            walkable = _price != 0.0f;
            penalty = _price;
            gridX = _gridX;
            gridY = _gridY;
        }

        public int fCost
        {
            get
            {
                return gCost + hCost;
            }
        }
    }
}