using UnityEngine;
public interface IMovementProvider
{
    float CurrentSpeed { get; }
    bool IsMoving { get; }
}