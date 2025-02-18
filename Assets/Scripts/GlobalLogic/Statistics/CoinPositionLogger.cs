using System.Collections.Generic;
using UnityEngine;

public class CoinPositionLogger : MonoBehaviour
{
    // Если хотите видеть список позиций в инспекторе в режиме выполнения, 
    // объявляем его как [SerializeField]
    [SerializeField] private List<Vector3> coinPositions = new List<Vector3>();

    /// <summary>
    /// Метод собирает все объекты с тегом "Coin" и выводит их позиции.
    /// </summary>
    public void LogCoinPositions()
    {
        // Очищаем список перед заполнением
        coinPositions.Clear();

        // Находим все объекты с тегом "Coin"
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        Debug.Log("Найдено монет: " + coins.Length);

        // Проходим по всем монеткам
        foreach (GameObject coin in coins)
        {
            Vector3 worldPos = coin.transform.position;
            Vector3 localPos = coin.transform.localPosition;
            coinPositions.Add(worldPos);

            Debug.Log($"Монетка {coin.name}: мировая позиция = {worldPos}, локальная позиция = {localPos}");
        }
    }
}
