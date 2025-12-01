using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public int Count = 5;
    public GameObject Prefab;

    public void Start()
    {
        for (int i = 0; i < Count; i++)
        {
            GameObject ch = Instantiate(Prefab);
            Waypoint point = transform.GetChild(Random.Range(0, transform.childCount)).GetComponent<Waypoint>();
            ch.transform.position = point.transform.position
                + new Vector3(Random.Range(-point.Radius, point.Radius), 0.11f, Random.Range(-point.Radius, point.Radius)) * 10;

            ch.GetComponent<CharacterNavigationController>().Initialize(Random.Range(2f, 6f), point, Mathf.RoundToInt(Random.Range(0f, 1f)) == 0);
        }
    }
}
