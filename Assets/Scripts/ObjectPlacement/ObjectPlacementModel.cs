using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPlacementModel : MonoBehaviour
{
    [SerializeField]
    private Transform[] _prefabs;
    
    private readonly List<Transform> _spawnedTransforms = new List<Transform>();
    
    public Transform currentTransform {get; private set;}
    public GameObject currentGObj => currentTransform.gameObject;

    private void Start()
    {
        // �������� �� ���� ��������, ������ �� ���������� � ��������� ��
        foreach (Transform prefab in _prefabs)
        {
            Transform instantiated = Instantiate(prefab);
            instantiated.gameObject.SetActive(false);
            _spawnedTransforms.Add(instantiated);
        }

        // �������� ������ ������ �� ���������
        currentTransform = _spawnedTransforms.First();
        _prefabs = null;
    }

    public void SetCurrentTransform(int index)
    {
        if (index >= 0 && index < _spawnedTransforms.Count)
            currentTransform = _spawnedTransforms[index];
    }
}
