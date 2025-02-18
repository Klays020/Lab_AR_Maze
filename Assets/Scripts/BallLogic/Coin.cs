using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            // Добавляем монету в счет
            ScoreManager.Instance.AddCoin();
            // Уничтожаем монетку после сбора
            Destroy(gameObject);
        }
    }
}
