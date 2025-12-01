using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private int _count = 5;
    [SerializeField] private GameObject _prefab;

    public void Start()
    {
        for (int i = 0; i < _count; i++)
        {
            GameObject ch = Instantiate(_prefab);
            Waypoint point = transform.GetChild(Random.Range(0, transform.childCount)).GetComponent<Waypoint>();
            ch.transform.position = point.transform.position
                + new Vector3(Random.Range(-point.Radius, point.Radius), 0.11f, Random.Range(-point.Radius, point.Radius)) * 10;

            ch.GetComponent<CharacterNavigationController>().Initialize(Random.Range(2f, 6f), point, Mathf.RoundToInt(Random.Range(0f, 1f)) == 0);
        }
    }
}
