using UnityEngine;

[RequireComponent(typeof(CharacterMovementComponent))]
[RequireComponent(typeof(CharacterNavigationController))]

public class CharacterService : MonoBehaviour
{
    [HideInInspector] public CharacterMovementComponent CharacterMovementComponent;
    [HideInInspector] public CharacterNavigationController CharacterNavigationController;

    private void Awake()
    {
        CharacterMovementComponent = GetComponent<CharacterMovementComponent>();
        CharacterNavigationController = GetComponent<CharacterNavigationController>();
    }

    public void Initialize(float speed, Waypoint nextPoint, bool isWaitOnEnd)
    {
        CharacterMovementComponent.Initialize(speed);
        CharacterNavigationController.Initialize(nextPoint, isWaitOnEnd, this);
    }
}
