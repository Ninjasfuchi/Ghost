using UnityEngine;

[CreateAssetMenu(menuName = "Create SausagerStats", fileName = "SausagerStats", order = 0)]
public class SausagerStats : ScriptableObject
{
    [Header("Movement")]
    public float movementSpeed;
    public float rotationSpeed;
}