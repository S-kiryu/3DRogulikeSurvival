using UnityEngine;
public interface IInputProvider
{
    //移動入力を取得するプロパティ
    Vector2 MoveInput { get; }
    bool IsDashing { get; }
}
