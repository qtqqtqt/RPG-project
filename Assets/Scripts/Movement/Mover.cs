using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6f;

        NavMeshAgent navMeshAgent;
        Health health;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction )
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        [System.Serializable]
        struct MoverData
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }

        public object CaptureState()
        {
            /*
            Dictionary<string, object> data = new();
            data["position"] = new SerializableVector3(transform.position)
            data["rotation"] = new SerializableVector3(transform.eulerAngles);
            */
            MoverData data = new MoverData();
            data.position = new SerializableVector3(transform.position);
            data.rotation = new SerializableVector3(transform.eulerAngles);

            return data;
        }

        public void RestoreState(object state)
        {
            /*
            Dictionary<string, object> data = (Dictionary<string, object>)state;

            SerializableVector3 restoredPosition = (SerializableVector3)data["position"];
            SerializableVector3 restoredRotation = (SerializableVector3)data["rotation"];
            */

            MoverData data = (MoverData)state;

            Vector3 restoredPosition = data.position.ToVector();
            Vector3 restoredRotation = data.rotation.ToVector();

            navMeshAgent.Warp(restoredPosition);
            transform.eulerAngles = restoredRotation;

            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}