using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Tin.Scripts.Characters.Player.Utilities.Input
{
    public class PlayerArcherInputs : MonoBehaviour
    {
        public PlayerArcherInputActions ArcherInputActions { get; private set; }
        public PlayerArcherInputActions.PlayerActions PlayerArcherActions { get; private set; }
        
        private void Awake()
        {
            ArcherInputActions = new PlayerArcherInputActions();
            PlayerArcherActions = ArcherInputActions.Player;
        }

        private void OnEnable() => ArcherInputActions.Enable();
        private void OnDisable() => ArcherInputActions.Disable();

        public void DisableActionFor(InputAction action, float seconds) => 
            StartCoroutine(DisableAction(action, seconds));

        private IEnumerator DisableAction(InputAction action, float seconds)
        {
            action.Disable();
            yield return new WaitForSeconds(seconds);
            action.Enable();
        }
    }
}
