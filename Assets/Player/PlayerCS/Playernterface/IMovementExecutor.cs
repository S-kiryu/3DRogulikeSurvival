using UnityEngine;

public interface IMovementExecutor
{
    void Execute(Vector2 direction, float speed);
}
