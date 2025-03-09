using UnityEngine;

public class Coin : MonoBehaviour
{
    [Tooltip("���������, ������� ��������������� ��� ����� �������")]
    [SerializeField] private AudioClip coinCollectClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            ScoreManager.Instance.AddCoin();

            if (AudioManager.Instance != null && coinCollectClip != null)
            {
                AudioManager.Instance.PlayEffect(coinCollectClip);
            }

            Destroy(gameObject);
        }
    }
}
