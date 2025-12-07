using UnityEngine;
using System.Collections.Generic;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private Transform _waypointRoot;
    [SerializeField] private int _count = 5;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private List<Material> _materials = new List<Material>();

    public void Start()
    {
        SpawnCharacters();
    }

    private void SpawnCharacters()
    {
        for (int i = 0; i < _count; i++)
        {
            GameObject ch = Instantiate(_prefab);
            Waypoint point = _waypointRoot.GetChild(Random.Range(0, _waypointRoot.childCount)).GetComponent<Waypoint>();
            ch.transform.position = point.transform.position
                + new Vector3(Random.Range(-point.Radius, point.Radius), 0.11f, Random.Range(-point.Radius, point.Radius)) * 10;

            ch.GetComponent<CharacterNavigationController>().Initialize(Random.Range(2f, 6f), point, Mathf.RoundToInt(Random.Range(0, 5)) != 0);

            if(_materials != null && _materials.Count > 0)
            {
                ch.GetComponent<MeshRenderer>().material = _materials[Random.Range(0, _materials.Count)];
            }
        }
    }
}
