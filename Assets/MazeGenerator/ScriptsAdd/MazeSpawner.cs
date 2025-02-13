using UnityEngine;
using System.Collections;

public class MazeSpawner : MonoBehaviour
{
    public enum MazeGenerationAlgorithm
    {
        PureRecursive,
        RecursiveTree,
        RandomTree,
        OldestTree,
        RecursiveDivision,
    }

    public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
    public bool FullRandom = false;
    public int RandomSeed = 12345;
    public GameObject Floor = null;
    public GameObject Wall = null;
    public GameObject Pillar = null;
    public int Rows = 5;
    public int Columns = 5;
    public float CellWidth = 5;
    public float CellHeight = 5;
    public bool AddGaps = true;
    public GameObject GoalPrefab = null;

    [Header("Scaling")]
    [Tooltip("Базовый коэффициент масштаба лабиринта при спавне (например, 0.15)")]
    public float overallScale = 0.15f;

    public GameObject firstTile;
    public GameObject finishTile;

    // Для хранения координат финишной клетки (по индексам)
    private int finishRow = -1, finishColumn = -1;
    private float maxGoalDist = -1f;

    private BasicMazeGenerator mMazeGenerator = null;

    void Start()
    {
        if (!FullRandom)
        {
            Random.seed = RandomSeed;
        }

        switch (Algorithm)
        {
            case MazeGenerationAlgorithm.PureRecursive:
                mMazeGenerator = new RecursiveMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RecursiveTree:
                mMazeGenerator = new RecursiveTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RandomTree:
                mMazeGenerator = new RandomTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.OldestTree:
                mMazeGenerator = new OldestTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RecursiveDivision:
                mMazeGenerator = new DivisionMazeGenerator(Rows, Columns);
                break;
        }

        mMazeGenerator.GenerateMaze();

        // Проходим по всем клеткам лабиринта
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                float x = column * (CellWidth + (AddGaps ? 0.2f : 0));
                float z = row * (CellHeight + (AddGaps ? 0.2f : 0));

                MazeCell cell = mMazeGenerator.GetMazeCell(row, column);
                GameObject tmp = Instantiate(Floor, new Vector3(x, 0, z), Quaternion.identity) as GameObject;
                tmp.transform.parent = GlobalContainer.Instance.transform;

                // Первая плитка (ячейка [0,0])
                if (row == 0 && column == 0)
                {
                    firstTile = tmp;
                    Debug.Log("Первая плитка установлена, позиция: " + firstTile.transform.position);
                }

                // Если клетка имеет IsGoal (тупиковая клетка) и не является стартовой,
                // вычисляем расстояние (по индексам) и выбираем ту, у которой оно максимальное.
                if (cell.IsGoal && !(row == 0 && column == 0))
                {
                    // Используем индексы клеток для расстояния от начала (0,0)
                    float d = Vector2.Distance(new Vector2(column, row), Vector2.zero);
                    if (d > maxGoalDist)
                    {
                        maxGoalDist = d;
                        finishTile = tmp;
                        finishRow = row;
                        finishColumn = column;
                    }
                }

                if (cell.WallRight)
                {
                    tmp = Instantiate(Wall, new Vector3(x + CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;
                    tmp.transform.parent = GlobalContainer.Instance.transform;
                }
                if (cell.WallFront)
                {
                    tmp = Instantiate(Wall, new Vector3(x, 0, z + CellHeight / 2) + Wall.transform.position, Quaternion.identity) as GameObject;
                    tmp.transform.parent = GlobalContainer.Instance.transform;
                }
                if (cell.WallLeft)
                {
                    tmp = Instantiate(Wall, new Vector3(x - CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 270, 0)) as GameObject;
                    tmp.transform.parent = GlobalContainer.Instance.transform;
                }
                if (cell.WallBack)
                {
                    tmp = Instantiate(Wall, new Vector3(x, 0, z - CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;
                    tmp.transform.parent = GlobalContainer.Instance.transform;
                }
                // Создаем GoalPrefab (например, монетку) только если клетка IsGoal,
                // но если эта клетка является финишной, GoalPrefab не создается.
                if (cell.IsGoal && GoalPrefab != null)
                {
                    // Если текущие индексы совпадают с финишными, пропускаем создание GoalPrefab
                    if (!(row == finishRow && column == finishColumn))
                    {
                        tmp = Instantiate(GoalPrefab, new Vector3(x, 1, z), Quaternion.identity) as GameObject;
                        tmp.transform.parent = GlobalContainer.Instance.transform;
                    }
                }
            }
        }

        if (Pillar != null)
        {
            for (int row = 0; row < Rows + 1; row++)
            {
                for (int column = 0; column < Columns + 1; column++)
                {
                    float x = column * (CellWidth + (AddGaps ? 0.2f : 0));
                    float z = row * (CellHeight + (AddGaps ? 0.2f : 0));
                    GameObject tmp = Instantiate(Pillar, new Vector3(x - CellWidth / 2, 0, z - CellHeight / 2), Quaternion.identity) as GameObject;
                    tmp.transform.parent = GlobalContainer.Instance.transform;
                }
            }
        }

        // Настраиваем finishTile, если она была выбрана
        if (finishTile != null)
        {
            Debug.Log("Финишная плитка выбрана, позиция: " + finishTile.transform.position);
            // Настраиваем Collider как триггер
            BoxCollider bc = finishTile.GetComponent<BoxCollider>();
            if (bc == null)
            {
                bc = finishTile.AddComponent<BoxCollider>();
            }
            bc.isTrigger = true;
            // Добавляем контроллер финиша
            if (finishTile.GetComponent<FinishTileController>() == null)
            {
                finishTile.AddComponent<FinishTileController>();
            }
        }
        else
        {
            Debug.Log("Финишная плитка не найдена!");
        }

        // Перемещаем GlobalContainer в позицию MazeSpawner и устанавливаем его масштаб
        GlobalContainer.Instance.transform.position = transform.position;
        GlobalContainer.Instance.transform.localScale = Vector3.one * overallScale;

        Debug.Log("Лабиринт сгенерирован, вызываем событие OnObjectPlaced");
        ObjectPlacement.RaiseOnObjectPlaced(transform);
    }
}

