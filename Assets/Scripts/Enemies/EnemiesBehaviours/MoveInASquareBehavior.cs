using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemies.EnemiesBehaviours
{
    public class MoveInASquareBehavior : EnemyBehaviour
    {
        public float Speed;
        public float FramesPerSide;


        public override IEnumerator ApplyBehaviour()
        {
            // Randomly choose starting state
            // 0 for moving right, 1 for up, 2 for left, 3 for down
            int state = Random.Range(0, 4);
            // Randomly choose direction of moving
            // 0 for clockwise, 1 for counter-clockwise
            int dir = Random.Range(0, 2);

            Vector2 moveTo = Vector2.zero;

            while (true)
            {
                // According to current state choose moveTo vector
                switch (state)
                {
                    // Move right
                    case 0:
                        moveTo = Vector2.right;
                        break;
                    // Move up
                    case 1:
                        moveTo = Vector2.up;
                        break;
                    // Move left
                    case 2:
                        moveTo = -Vector2.right;
                        break;
                    // Move down
                    case 3:
                        moveTo = -Vector2.up;
                        break;
                }

                // Move towards current direction
                for (int i = 0; i < FramesPerSide; i++)
                {
                    _rb2D.velocity = moveTo * Speed;
                    yield return null;
                }

                // Change state according to direction
                state = dir == 1 ? state + 1 : state - 1;
                // Make sure state is between 0 and 3
                if (state > 3) state = 0;
                if (state < 0) state = 3;
            }
        }
    }
}