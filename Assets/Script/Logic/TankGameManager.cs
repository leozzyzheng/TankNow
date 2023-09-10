using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankGameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerManager mPlayerManager = new PlayerManager();

    [SerializeField]
    private MapManager mMapManager = new MapManager();

    private GlobalInputActions mGlobalInputActions;

    // Start is called before the first frame update
    private void Start()
    {
        mPlayerManager.StartGame();

        mGlobalInputActions = new GlobalInputActions();
        mGlobalInputActions.StartNewPlayer.Start.performed += StartNewPlayer;
        mGlobalInputActions.Enable();
    }

    private void OnDestroy()
    {
        mPlayerManager.EndGame();
        mGlobalInputActions?.Disable();
    }

    private void StartNewPlayer(InputAction.CallbackContext context)
    {
        Vector3 position = mMapManager.GetSpawnPoint(mPlayerManager.GetCurrentPlayerCount());
        if (position == Vector3.zero)
        {
            return;
        }

        InputDevice device = context.control.device;
        mPlayerManager.RequestNewPlayer(device, position);
    }
}
