public class BoardGenerator
{
    private Cell[,] _state;
    private int _width;
    private int _height;
    private int _minesCount;

    public BoardGenerator(Cell[,] state, int width, int height, int minesCount)
    {
        _state = state;
        _width = width;
        _height = height;
        _minesCount = minesCount;
    }

    public void GenerateCells()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Cell cell = new Cell(i, j);
                _state[i, j] = cell;
            }
        }
    }
    public void GenerateMines(int excludeX, int excludeY)
    {
        int totalTiles = _width * _height;

        if (_minesCount > totalTiles)
            _minesCount = totalTiles;

        int minesPlaced = 0;
        System.Random rand = new System.Random();

        while (minesPlaced < _minesCount)
        {
            int x = rand.Next(_width);
            int y = rand.Next(_height);

            if (x == excludeX && y == excludeY)
                continue;

            if (_state[x, y].Type != CellType.Mine)
            {
                _state[x, y].Type = CellType.Mine;
                minesPlaced++;
            }
        }
    }

    public void GenerateNumbers()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Cell cell = _state[i, j];

                if (cell.Type == CellType.Mine)
                    continue;

                cell.Number = GameplayHelper.CountAdjacentMines(_state, cell, _width, _height);

                if (cell.Number > 0)
                    cell.Type = CellType.Number;

                _state[i, j] = cell;
            }
        }
    }
}
