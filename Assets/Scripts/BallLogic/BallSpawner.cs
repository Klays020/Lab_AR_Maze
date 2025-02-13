using System;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [Header("��������� ����")]
    [SerializeField] private GameObject ballPrefab; // ������ ���� � Rigidbody � SphereCollider
    [SerializeField] private float ballRadius = 0.52f;

    [SerializeField] private float verticalSpawnOffset = 0.52f;

    private bool ballSpawned = false;
    private Vector3 ballBaseScale;

    //private GameObject spawnedBall;
    private void OnEnable()
    {
        Debug.Log("��������");
        ObjectPlacement.OnObjectPlaced += HandleObjectPlaced;
    }

    private void OnDisable()
    {
        ObjectPlacement.OnObjectPlaced -= HandleObjectPlaced;
    }

    private void HandleObjectPlaced(Transform spawnedObjectTransform)
    {
        if (ballSpawned)
            return;

        MazeSpawner mazeSpawner = spawnedObjectTransform.GetComponent<MazeSpawner>();
        Vector3 spawnPos;

        if (mazeSpawner == null)
        {
            Debug.Log("mazeSpawner == null");
        }

        if (mazeSpawner.firstTile == null)
        {
            Debug.Log("mazeSpawner.firstTile == null");
        }

        if (mazeSpawner != null && mazeSpawner.firstTile != null)
        {
            Debug.Log("����� �������");

            spawnPos = mazeSpawner.firstTile.transform.position + Vector3.up * verticalSpawnOffset;
        }
        else
        {
            Debug.Log("����� ���������");
            spawnPos = spawnedObjectTransform.position + Vector3.up * verticalSpawnOffset;
        }

        Instantiate(ballPrefab, spawnPos, Quaternion.identity, GlobalContainer.Instance.transform);
        ballSpawned = true;
    }
}
