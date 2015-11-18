using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemies.EnemiesBehaviours
{
    public class FollowPlayerBehavior : EnemyBehaviour
    {
        public float Speed;
        public float Acceleration;


        public override IEnumerator ApplyBehaviour()
        {
            while (true)
            {
                // Smoothly increase velocity to it's maximum towards player position
                _rb2D.velocity = Vector2.Lerp(_rb2D.velocity, ToPlayerVector*Speed, Acceleration);

                yield return null;
            }
        }
    }
}