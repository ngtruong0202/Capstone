using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Tin.Scripts.Characters.Player.Utilities.Input
{
    public class PlayerArcherInputs : MonoBehaviour
    {
        public PlayerArcherInputActions ArcherInputActionses { get; private set; }
        public PlayerArcherInputActions.PlayerActions PlayerArcherActions { get; private set; }
        
        private void Awake()
        {
            ArcherInputActionses = new PlayerArcherInputActions();
            PlayerArcherActions = ArcherInputActionses.Player;
        }

        private void OnEnable() => ArcherInputActionses.Enable();
        private void OnDisable() => ArcherInputActionses.Disable();

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
