using UnityEngine;

namespace CozyClubhouse.CameraSystem
{
    public class DioramaCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(8f, 9f, -8f);
        [SerializeField] private float followSpeed = 4f;

        public void SetTarget(Transform newTarget) => target = newTarget;

        private void LateUpdate()
        {
            if (!target) return;
            var desired = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desired, 1f - Mathf.Exp(-followSpeed * Time.deltaTime));
            transform.LookAt(target.position + Vector3.up * 0.8f);
        }
    }
}
