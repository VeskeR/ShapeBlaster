using System.Collections;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Enemies.EnemiesBehaviours
{
    public abstract class EnemyBehaviour : MonoBehaviour
    {
        protected PlayerShip _player;
        protected Rigidbody2D _rb2D;

        public Vector3 ToPlayerVector
        {
            get { return (_player.transform.position - transform.position).normalized; }
        }


        protected virtual void Awake()
        {
            _player = FindObjectOfType<PlayerShip>();
            _rb2D = GetComponent<Rigidbody2D>();
        }


        // Method that declares how enemy is moving
        public abstract IEnumerator ApplyBehaviour();
    }
}
