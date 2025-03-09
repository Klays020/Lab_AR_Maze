using System.Collections.Generic;
using UnityEngine;

public class CoinPositionLogger : MonoBehaviour
{
    [SerializeField] private List<Vector3> coinPositions = new List<Vector3>();

    public void LogCoinPositions()
    {
        coinPositions.Clear();

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
