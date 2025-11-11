using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAsteroid
{
    void Initialize(AsteroidData data);
}

[CreateAssetMenu(fileName = "AsteroidData", menuName = "Enemy/Asteroid Data")]
public class AsteroidData : ScriptableObject
{
    public int Size;
    public float Speed;
}