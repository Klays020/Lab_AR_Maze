using System;
using System.Linq;
using UnityEngine;

public enum Difficulty
{
    Easy = 0,
    Medium = 1,
    Hard = 2
}

public class SeedSelector : MonoBehaviour
{
    [Header("Настройки")]
    [Tooltip("Имя файла в папке Resources (без расширения .txt). Например, SeedData")]
    public string seedFileName = "SeedData";

    public int GetRandomSeedForDifficulty(Difficulty difficulty)
    {
        TextAsset seedDataAsset = Resources.Load<TextAsset>(seedFileName);

        if (seedDataAsset == null)
        {
            Debug.LogError($"Файл '{seedFileName}.txt' не найден в папке Resources!");
            return 0;
        }

        string[] lines = seedDataAsset.text.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

        int index = (int)difficulty;

        if (index >= lines.Length)
        {
            Debug.LogWarning($"В файле всего {lines.Length} строк, а запрошен индекс {index}. Будет использована последняя строка");
            index = lines.Length - 1;
        }

        string selectedLine = lines[index];

        // Находим последний двоеточие (':'), чтобы извлечь часть со списком сидов
        int colonIndex = selectedLine.LastIndexOf(':');
        if (colonIndex == -1 || colonIndex >= selectedLine.Length - 1)
        {
            Debug.LogError($"Неверный формат строки (нет двоеточия или отсутствуют сиды): {selectedLine}");
            return 0;
        }

        // Извлекаем подстроку после двоеточия
        string seedsPart = selectedLine.Substring(colonIndex + 1);

        int[] seeds = seedsPart.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => {
                int value;
                return int.TryParse(s.Trim(), out value) ? value : -1;
            })
            .Where(val => val != -1)
            .ToArray();

        if (seeds.Length == 0)
        {
            Debug.LogError($"Не удалось извлечь сиды из строки: {selectedLine}");
            return 0;
        }

        int randomIndex = UnityEngine.Random.Range(0, seeds.Length);
        int chosenSeed = seeds[randomIndex];

        Debug.Log($"Выбран сид: {chosenSeed} для сложности {difficulty}");
        return chosenSeed;
    }
}
