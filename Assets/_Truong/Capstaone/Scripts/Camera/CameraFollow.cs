using Cinemachine;
using Truong;
using UnityEngine;

namespace Truong
{
    public class CameraFollow : MonoBehaviour
    {
        public CinemachineVirtualCamera cam;


        private void OnEnable()
        {
            GameManager.PlayerSpawned += OnPlayerSpawned;
        }

        private void OnDisable()
        {
            GameManager.PlayerSpawned -= OnPlayerSpawned;
        }

        private void OnPlayerSpawned(Player player)
        {
            cam.Follow = player.transform.GetChild(0);
            cam.LookAt = player.transform.GetChild(0);
        }
    }

}
