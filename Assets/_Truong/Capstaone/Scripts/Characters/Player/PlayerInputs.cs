using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Truong
{
    public class PlayerInputs : MonoBehaviour
    {
        public PlayerInput inputActions { get; private set; }
        public PlayerInput.PlayerActions playerActions { get; private set; }

        private void Awake()
        {
            inputActions = new PlayerInput();
            playerActions = inputActions.Player;
        }

        private void OnEnable()
        {
            inputActions.Enable();
        }
        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void DisableActionFor(InputAction action, float second)
        {
            StartCoroutine(DisableAction(action, second));
        }

        private IEnumerator DisableAction(InputAction action, float second)
        {
            action.Disable();

            yield return new WaitForSeconds(second);

            action.Enable();
        }
    }
}

