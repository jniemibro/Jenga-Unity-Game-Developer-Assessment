namespace JengaGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class OrbitCamera : MonoBehaviour
    {
        [SerializeField] Transform[] potentialTargets;
        [SerializeField] Transform target;
    
        int currentTargetIndex = 0;
        float mouseX = 0;
        float mouseY = 0;

        public Transform Target { get { return target; } }

        void Awake()
        {
            // update starting potential target index to match assigned target in editor
            for (int i=0; i<potentialTargets.Length; i++)
            {
                if (potentialTargets[i] == target)
                {
                    currentTargetIndex = i;
                    break;
                }
            }
        }

        public void UpdateTarget(Transform newTarget)
        {
            target = newTarget;
        }

        void Update()
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY += Input.GetAxis("Mouse Y");

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                NextTarget();
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                PreviousTarget();
        }

        void LateUpdate()
        {
            if (!Target)
                return;
            transform.LookAt(Target);
        }

        void PreviousTarget()
        {
            currentTargetIndex -= 1;
            if (currentTargetIndex < 0)
                currentTargetIndex = potentialTargets.Length-1;

            target = potentialTargets[currentTargetIndex];
        }

        void NextTarget()
        {
            currentTargetIndex += 1;
            if (currentTargetIndex >= potentialTargets.Length)
                currentTargetIndex = 0;

            target = potentialTargets[currentTargetIndex];
        }
    }
}