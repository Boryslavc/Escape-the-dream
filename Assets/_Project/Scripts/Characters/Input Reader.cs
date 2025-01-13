using UnityEngine;
using UnityEngine.Events;

namespace Core.Characters
{
    [CreateAssetMenu(fileName = "Input Reader", menuName = "Systems/Core/Input")]
    public class InputReader : ScriptableObject, PlayerInput.IBaseActions
    {
        public event UnityAction<Vector2> SpellCasted = delegate { };
        public event UnityAction SpellChanged = delegate { };

        private PlayerInput inputActions;

        private Vector2 inputPosition;

        private void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerInput();
                inputActions.Base.SetCallbacks(this);
            }

            inputActions.Enable();
        }


        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void OnPositionTracker(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            inputPosition = context.ReadValue<Vector2>();
        }

        public void OnSpellCast(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (context.phase == UnityEngine.InputSystem.InputActionPhase.Started)
                SpellCasted.Invoke(inputPosition);
        }

        public void OnChangeSpellType(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (context.phase == UnityEngine.InputSystem.InputActionPhase.Started)
                SpellChanged.Invoke();
        }
    }
}
