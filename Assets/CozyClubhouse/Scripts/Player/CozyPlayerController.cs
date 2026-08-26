using UnityEngine;
using UnityEngine.AI;

namespace CozyClubhouse.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class CozyPlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3.2f;
        [SerializeField] private float rotationSpeed = 12f;
        [SerializeField] private Camera worldCamera;
        [SerializeField] private LayerMask groundMask = ~0;

        private CharacterController controller;
        private Vector3 target;
        private bool hasTarget;

        public string Activity { get; private set; } = "Available";

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            target = transform.position;
            if (!worldCamera) worldCamera = Camera.main;
        }

        private void Update()
        {
            HandleKeyboard();
            HandlePointer();
            MoveTowardTarget();
        }

        private void HandleKeyboard()
        {
            var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (input.sqrMagnitude < 0.01f) return;

            hasTarget = false;
            var direction = new Vector3(input.x, 0f, input.y).normalized;
            Move(direction);
        }

        private void HandlePointer()
        {
            if (!worldCamera) return;

            bool pressed = Input.GetMouseButtonDown(0);
            Vector2 screenPosition = Input.mousePosition;

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                pressed = true;
                screenPosition = Input.GetTouch(0).position;
            }

            if (!pressed) return;

            var ray = worldCamera.ScreenPointToRay(screenPosition);
            if (!Physics.Raycast(ray, out var hit, 100f, groundMask, QueryTriggerInteraction.Ignore)) return;

            target = hit.point;
            target.y = transform.position.y;
            hasTarget = true;
        }

        private void MoveTowardTarget()
        {
            if (!hasTarget) return;

            var delta = target - transform.position;
            delta.y = 0f;
            if (delta.sqrMagnitude < 0.04f)
            {
                hasTarget = false;
                return;
            }

            Move(delta.normalized);
        }

        private void Move(Vector3 direction)
        {
            controller.SimpleMove(direction * moveSpeed);
            if (direction.sqrMagnitude > 0.01f)
            {
                var desired = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, desired, rotationSpeed * Time.deltaTime);
            }
        }

        public void SetActivity(string activity)
        {
            Activity = activity;
            hasTarget = false;
        }

        public void SnapTo(Transform point)
        {
            if (!point) return;
            controller.enabled = false;
            transform.SetPositionAndRotation(point.position, point.rotation);
            controller.enabled = true;
            target = transform.position;
            hasTarget = false;
        }
    }
}
