namespace JengaGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class OrbitCamera : MonoBehaviour
    {
        [SerializeField] Vector3 targetOffset = Vector3.up;
        [SerializeField] Vector2 orbitSensitivity = Vector2.one;

        [Space()]
        [SerializeField] JengaStack[] potentialTargets;
        [SerializeField] JengaStack target;
    
        int currentTargetIndex = 0;
        float distance = 5f;

        float xRot = 0f;
        float yRot = 0f;
    
        float sensitivity = 1000f;
        Vector3 targetPosition;
        Vector3 targetLookPoint;

        const float TRANSLATE_INTERPOLATE_SPEED = 10.0f;
        const float LOOK_INTERPOLATE_SPEED = 2.0f;

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

        public void UpdateTarget(JengaStack newTarget)
        {
            targetLookPoint = target.Position;
            distance = Vector3.Distance(newTarget.Position, transform.position);
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
            targetPosition = Vector3.Lerp(targetPosition, 
                target.transform.TransformPoint(targetOffset) + Quaternion.Euler(xRot, yRot, 0f) * (distance * -Vector3.back), 
                TRANSLATE_INTERPOLATE_SPEED * Time.unscaledDeltaTime);
            // interpolate look target point
            targetLookPoint = Vector3.Lerp(targetLookPoint, target.transform.TransformPoint(targetOffset), 
                LOOK_INTERPOLATE_SPEED * Time.unscaledDeltaTime);
        }
    
        void LateUpdate()
        {
            if (!target)
                return;

            // only use mouse axis while left-click is held
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