using System.Collections;
using Assets.Scripts.Management;
using UnityEngine;

namespace Assets.Scripts.Enemies.EnemiesBehaviours
{
    public class MoveRandomlyBehaviour : EnemyBehaviour
    {
        public float Speed;
        public float FramesBeforeChangingDirection;
        public float MaxAngleChange;


        public override IEnumerator ApplyBehaviour()
        {
            // Randomly choose initial direction of moving
            float moveAngle = Random.Range(0f, Mathf.PI*2);
            // Form direction vector
            Vector2 moveVector = new Vector2(Mathf.Cos(moveAngle), Mathf.Sin(moveAngle));

            while (true)
            {
                for (int i = 0; i < FramesBeforeChangingDirection; i++)
                {
                    // If object reached any border, revert the appropriate direction
                    if (transform.position.x >= GameManager.XMax || transform.position.x <= GameManager.XMin)
                    {
                        moveVector.x = -moveVector.x;
                    }
                    if (transform.position.y >= GameManager.YMax || transform.position.y <= GameManager.YMin)
                    {
                        moveVector.y = -moveVector.y;
                    }

                    // Set object's velocity
                    _rb2D.velocity = moveVector * Speed;

                    // Make sure the position is inside the borders
                    transform.position = new Vector3
                        (
                        Mathf.Clamp(transform.position.x, GameManager.XMin, GameManager.XMax),
                        Mathf.Clamp(transform.position.y, GameManager.YMin, GameManager.YMax),
                        .0f
                        );

                    yield return null;
                }

                // Get current moving direction
                moveAngle = Mathf.Atan2(moveVector.y, moveVector.x);

                // Change direction angle
                moveAngle += Random.Range(-MaxAngleChange*Mathf.Deg2Rad, MaxAngleChange*Mathf.Deg2Rad);
                // Set new direction vector
                moveVector = new Vector2(Mathf.Cos(moveAngle), Mathf.Sin(moveAngle));
            }
        }
    }
}