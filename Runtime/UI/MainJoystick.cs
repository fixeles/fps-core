#if JOYSTICK
using UnityEngine;

namespace Game.UI
{
    [RequireComponent(typeof(Joystick))]
    public class MainJoystick : MonoBehaviour
    {
        [SerializeField] private Joystick joystick;
        private Camera _camera;


        public Vector3 Direction => GetNormalizedWorldDirection();

        public bool IsActive()
        {
            return joystick.DeadZone * joystick.DeadZone < joystick.Direction.sqrMagnitude;
        }

        private void Awake()
        {
            _camera = Camera.main;
        }

        private Vector3 GetNormalizedWorldDirection()
        {
            float angle = _camera.transform.rotation.eulerAngles.y;
            var cos = Mathf.Cos(angle * Mathf.Deg2Rad);
            var sin = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector2 direction = joystick.Direction;
            float x = direction.x * cos - direction.y * sin;
            float z = direction.x * sin + direction.y * cos;

            return new Vector3(x, 0, z).normalized;
        }
    }
}
#endif