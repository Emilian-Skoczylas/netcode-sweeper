
using UnityEngine;

public static class GameplayHelper
{
    public static readonly (int x, int y)[] NeighborOffsets = {
    (-1,-1), (-1,0), (-1,1),
    (0,-1),         (0,1),
    (1,-1),  (1,0),  (1,1)
    };

    public static int CountAdjacentMines(Cell[,] state, Cell cell, int boardWidth, int boardHeight)
    {
        int count = 0;

        for (int adjacentX = -1; adjacentX <= 1; adjacentX++)
        {
            for (int adjacentY = -1; adjacentY <= 1; adjacentY++)
            {
                if (adjacentX == 0 && adjacentY == 0)
                    continue;

                int x = cell.Position.x + adjacentX;
                int y = cell.Position.y + adjacentY;

                if (x < 0 || x >= boardWidth || y < 0 || y >= boardHeight)
                    continue;

                if (state[x, y].Type == CellType.Mine)
                    count++;
            }
        }
        if (count > 1)
            Debug.Log("");

        return count;
    }

    public static bool IsCoordinateValid(int width, int height, int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }
}
