using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : Entity
    {
        [SerializeField] Building maintarget;
        public EnemyDefinition definition;
        public elements myElement;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] int currentlife;
        public event Action OnSpawn = delegate { };
        public event Action OnDeath = delegate { };


        private void Reset() => FetchComponents();

        private void Awake() => FetchComponents();

    
        private void FetchComponents()
        {
            agent ??= GetComponent<NavMeshAgent>();

            currentlife = definition.MaxLife;
            
            
        }

        public void initialize()
        {
            FetchComponents();
            
        }

        private void OnEnable()
        {
            StartCoroutine(delay());
        }


        private IEnumerator AlertSpawn()
        {
            //Waiting one frame because event subscribers could run their onEnable after us.
            yield return null;
            OnSpawn();
        }

        private IEnumerator delay()
        {
            yield return null;

            SetTarget();
        }

        private void Update()
        {
            if (agent.hasPath
                && Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
            {
                Debug.Log($"{name}: I'll die for my people!");
                DealDamage();
                TakeDamage(10);
                if(currentlife <= 0)
                Die();
            }
        }

        private void Die()
        {
            OnDeath();
            gameObject.SetActive(false);
            pool.enqueueEnemy(this, myElement);
        }

        public override void TakeDamage(int damage)
        {
            currentlife -= damage;
        }

        public  void DealDamage()
        {
            maintarget.TakeDamage(definition.Damage);
            if (maintarget.destroyed)
                SetTarget();
        }

        public void SetTarget()
        {
            
            //Is this necessary?? We're like, searching for it from every enemy D:
            Building[] allTargets = FindObjectsOfType<Building>(false);
            Transform mytarget = null;
            float closestdistance = Mathf.Infinity;
            foreach (var target in allTargets)
            {
                float distance = Vector3.Distance(target.transform.position, agent.transform.position);
                if(distance < closestdistance)
                {
                    closestdistance = distance;
                    mytarget = target.transform;
                    maintarget = target;
                }
            }
            if (mytarget == null)
            {
                Debug.LogError($"{name}: Found no {nameof(mytarget)}!! :(");
                return;
            }

            var destination = mytarget.transform.position;
            destination.y = transform.position.y;
            agent.speed = definition.Speed;
            agent.SetDestination(destination);

            StartCoroutine(AlertSpawn());
        }


   
    }
}
