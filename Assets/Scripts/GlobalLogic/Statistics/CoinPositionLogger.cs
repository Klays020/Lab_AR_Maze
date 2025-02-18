using System.Collections.Generic;
using UnityEngine;

public class CoinPositionLogger : MonoBehaviour
{
    // ���� ������ ������ ������ ������� � ���������� � ������ ����������, 
    // ��������� ��� ��� [SerializeField]
    [SerializeField] private List<Vector3> coinPositions = new List<Vector3>();

    /// <summary>
    /// ����� �������� ��� ������� � ����� "Coin" � ������� �� �������.
    /// </summary>
    public void LogCoinPositions()
    {
        // ������� ������ ����� �����������
        coinPositions.Clear();

        // ������� ��� ������� � ����� "Coin"
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        Debug.Log("������� �����: " + coins.Length);

        // �������� �� ���� ��������
        foreach (GameObject coin in coins)
        {
            Vector3 worldPos = coin.transform.position;
            Vector3 localPos = coin.transform.localPosition;
            coinPositions.Add(worldPos);

            Debug.Log($"������� {coin.name}: ������� ������� = {worldPos}, ��������� ������� = {localPos}");
        }
    }
}
