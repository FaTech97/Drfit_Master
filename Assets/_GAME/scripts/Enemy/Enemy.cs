using System;
using UnityEngine;
using UnityEngine.AI;

namespace _GAME.scripts.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyConfig _config;
        [SerializeField] private Transform _player;
        
        private NavMeshAgent _navMashAgent;
        private SphereCollider _collider;
        private IWeapon _weapon;

        private void OnValidate()
        {
            _navMashAgent = GetComponent<NavMeshAgent>();
            _navMashAgent.stoppingDistance = _config.attackRadius;
            _navMashAgent.speed = _config.speed;
            _weapon = GetComponentInChildren<IWeapon>();
        }

        private void Update()
        {
            MoveToPlayerOrWait();
            TryAttack();
        }

        private void MoveToPlayerOrWait()
        {
            if (Vector3.Distance(_player.position, transform.position) > _config.aggreRadius)
                return;
            
            _navMashAgent.SetDestination(_player.position);
        }

        private void TryAttack()
        {
            if (Vector3.Distance(_player.position, transform.position) > _config.attackRadius)
                return;
            
            _weapon.Attack(_player);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _config.aggreRadius);
        }
    }
}
