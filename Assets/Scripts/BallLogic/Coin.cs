using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            // ��������� ������ � ����
            ScoreManager.Instance.AddCoin();
            // ���������� ������� ����� �����
            Destroy(gameObject);
        }
    }
}
