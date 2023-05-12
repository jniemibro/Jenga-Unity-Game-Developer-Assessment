namespace JengaGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class OrbitCamera : MonoBehaviour
    {
        [SerializeField] Vector2 orbitSensitivity = Vector2.one;

        [Space()]
        [SerializeField] Transform[] potentialTargets;
        [SerializeField] Transform target;
    
        int currentTargetIndex = 0;
        float distance = 5f;

        float xRot = 0f;
        float yRot = 0f;
    
        float sensitivity = 1000f;
        Vector3 targetPosition;
        Vector3 targetLookPoint;

        public Transform Target { get { return target; } }

        void Awake()
        {
            targetPosition = transform.position;
            // update starting potential target index to match assigned target in editor
            for (int i=0; i<potentialTargets.Length; i++)
            {
                if (potentialTargets[i] == target)
                {
                    currentTargetIndex = i;
                    break;
                }
            }
            UpdateTarget(target);
        }

        public void UpdateTarget(Transform newTarget)
        {
            targetLookPoint = target.position;
            distance = Vector3.Distance(newTarget.position, transform.position);
            target = newTarget;
        }

        void Update()
        {
            //xRot += Input.GetAxis("Mouse X");
            //yRot += Input.GetAxis("Mouse Y");

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                NextTarget();
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                PreviousTarget();

            
            // interpolate camera position
            targetPosition = Vector3.Lerp(targetPosition, target.position + Quaternion.Euler(xRot, yRot, 0f) * (distance * -Vector3.back), 10 * Time.unscaledDeltaTime);
            // interpolate look target point
            targetLookPoint = Vector3.Lerp(targetLookPoint, target.position, 2 * Time.unscaledDeltaTime);
        }
    
        void LateUpdate()
        {
            if (!target)
                return;

            if (Input.GetButton("Fire1"))
            {
                xRot += Mathf.DeltaAngle(xRot, xRot + Input.GetAxis("Mouse Y") * sensitivity * Time.unscaledDeltaTime);
                yRot += Mathf.DeltaAngle(yRot, yRot + Input.GetAxis("Mouse X") * sensitivity * Time.unscaledDeltaTime);
            }
    
            if(xRot > 89f)
            {
                xRot = 89f;
            }
            else if(xRot < -89f)
            {
                xRot = -89f;
            }
    
            transform.position = targetPosition;
            transform.LookAt(targetLookPoint, Vector3.up);
        }

        /*void LateUpdate()
        {
            if (!Target)
                return;

            transform.Translate(Vector3.right * -Input.GetAxis("Mouse X") * orbitSensitivity.x, Space.Self);
            transform.Translate(Vector3.up * -Input.GetAxis("Mouse Y") * orbitSensitivity.y, Space.Self);
            transform.LookAt(target);

            Vector3 dir = target.position - transform.position;
            dir = dir.normalized * distance;
            transform.position = target.position - dir;
            //transform.LookAt(Target);

            // TODO: zooming based on obstructions?
        }*/

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