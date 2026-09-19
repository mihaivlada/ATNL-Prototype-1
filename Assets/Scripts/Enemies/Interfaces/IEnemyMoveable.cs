using UnityEngine;

public interface IMoveable 
{
      Rigidbody RB { get; set; }

        bool isFacingRight { get; set; }

    void MoveEnemy(Vector2 velocity);
    void CheckForLeftOrRightFacing(Vector2 velocity);

}
