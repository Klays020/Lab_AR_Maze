using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

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

    [Header("Основные настройки генерации")]
    public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
    public bool FullRandom = false;
    public int RandomSeed;
    public GameObject Floor = null;
    public GameObject Wall = null;
    public GameObject Pillar = null;

    public int Rows = 5;
    public int Columns = 5;

    public float CellWidth = 5;
    public float CellHeight = 5;
    public bool AddGaps = true;
    public GameObject GoalPrefab = null;

    [Header("Difficulty Settings")]
    [Tooltip("Если true, берём сид из SeedSelector (по выбранной сложности в GameSettings)")]
    public bool useSeedFromFile = true;
    private SeedSelector seedSelector;

    [Header("Scaling")]
    [Tooltip("Базовый коэффициент масштаба лабиринта при спавне (например, 0.15)")]
    public float overallScale = 0.15f;

    [Header("Seed Test Settings")]
    public bool runSeedTest = false;
    public int testSeedStart = 1;
    public int testSeedEnd = 10;

    public GameObject firstTile;
    public GameObject finishTile;

    private int finishRow = -1, finishColumn = -1;
    private float maxGoalDist = -1f;

    private BasicMazeGenerator mMazeGenerator = null;
    private List<int> validSeeds = new List<int>();

    void Start()
    {
        if (runSeedTest)
        {
            StartCoroutine(TestSeedRange());
        }
        else
        {
            Rows = GameSettings.MazeRows;
            Columns = GameSettings.MazeColumns;

            if (useSeedFromFile)
            {
                seedSelector = FindObjectOfType<SeedSelector>();

                if (seedSelector != null)
                {
                    RandomSeed = seedSelector.GetRandomSeedForDifficulty(GameSettings.selectedDifficulty);
                }
                else
                {
                    Debug.Log("SeedSelector не найден в сцене. Будет использован RandomSeed из инспектора");
                }
            }

            GenerateAndSpawnMaze();
        }
    }

    void GenerateAndSpawnMaze()
    {
        if (!FullRandom)
        {
            Random.seed = RandomSeed;
            Debug.Log("Выбран сид: " + RandomSeed);
        }

        ChooseAlgorithm();
        mMazeGenerator.GenerateMaze();
        SpawnMaze();

        GlobalContainer.Instance.transform.position = transform.position;
        GlobalContainer.Instance.transform.localScale = Vector3.one * overallScale;

        Debug.Log("Лабиринт сгенерирован, вызываем событие OnObjectPlaced");
        ObjectPlacement.RaiseOnObjectPlaced(transform);
    }

    void SpawnMaze()
    {
        maxGoalDist = -1f;
        finishTile = null;
        finishRow = -1;
        finishColumn = -1;

        int coinCount = 0;

        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                float x = column * (CellWidth + (AddGaps ? 0.2f : 0));
                float z = row * (CellHeight + (AddGaps ? 0.2f : 0));

                MazeCell cell = mMazeGenerator.GetMazeCell(row, column);

                GameObject tmp = Instantiate(Floor, new Vector3(x, 0, z), Quaternion.identity) as GameObject;
                tmp.transform.parent = GlobalContainer.Instance.transform;

                if (row == 0 && column == 0)
                {
                    firstTile = tmp;
                    Debug.Log("Первая плитка установлена, позиция: " + firstTile.transform.position);
                }

                if (cell.IsGoal && !(row == 0 && column == 0))
                {
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

                if (cell.IsGoal && GoalPrefab != null)
                {
                    if (!(row == finishRow && column == finishColumn))
                    {
                        tmp = Instantiate(GoalPrefab, new Vector3(x, 1, z), Quaternion.identity) as GameObject;
                        tmp.transform.parent = GlobalContainer.Instance.transform;
                        coinCount++;
                    }
                }
            }
        }

        if (finishTile != null)
        {
            Debug.Log("Финишная плитка выбрана, позиция: " + finishTile.transform.position);
            BoxCollider bc = finishTile.GetComponent<BoxCollider>();
            if (bc == null)
            {
                bc = finishTile.AddComponent<BoxCollider>();
            }
            bc.isTrigger = true;
            if (finishTile.GetComponent<FinishTileController>() == null)
            {
                finishTile.AddComponent<FinishTileController>();
            }
        }
        else
        {
            Debug.Log("Финишная плитка не найдена!");
        }

        Debug.Log("Общее количество монет: " + coinCount);
    }

    void ChooseAlgorithm()
    {
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
    }

    // Короутина для тестирования сидов
    IEnumerator TestSeedRange()
    {
        validSeeds.Clear();

        for (int seed = testSeedStart; seed <= testSeedEnd; seed++)
        {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in GlobalContainer.Instance.transform)
            {
                children.Add(child.gameObject);
            }
            foreach (GameObject child in children)
            {
                Destroy(child);
            }
            yield return null;

            Random.seed = seed;
            ChooseAlgorithm();
            mMazeGenerator.GenerateMaze();
            SpawnMaze();
            GlobalContainer.Instance.transform.position = transform.position;
            GlobalContainer.Instance.transform.localScale = Vector3.one * overallScale;

            yield return new WaitForSeconds(0.1f);

            int coinCount = 0;
            foreach (Transform child in GlobalContainer.Instance.transform)
            {
                if (child.CompareTag("Coin"))
                    coinCount++;
            }

            if (coinCount == 3)
            {
                Debug.Log("Seed " + seed + " генерирует 3 монет");
                validSeeds.Add(seed);
            }

            yield return new WaitForSeconds(0.1f);
        }

        string filePath = Application.persistentDataPath + "/validSeeds.txt";
        string text = $"Algorithm PureRecursive, Seeds with 3 coins with rows: {Rows} column: {Columns}: "
                      + string.Join(", ", validSeeds.ToArray()) + "\n";
        File.AppendAllText(filePath, text);
        Debug.Log("Подходящие сиды сохранены в файл: " + filePath);
    }
}
