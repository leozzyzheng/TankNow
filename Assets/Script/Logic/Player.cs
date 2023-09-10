using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Player id, we can have multi-player played on the same machine,
    /// each player have an unique id
    /// </summary>
    private int mPlayerId;
    private PlayerInputActions mInputActions;
    private InputUser mInputUser;
    private Vector3 mSpawnPosition;

    private TankMovement mTankMovement;

    public int GetPlayerId()
    {
        return mPlayerId;
    }

    public void Init(int playerId, Vector3 spawnPosition)
    {
        mPlayerId = playerId;
        mSpawnPosition = spawnPosition;
        gameObject.transform.position = mSpawnPosition;
        mTankMovement = GetComponent<TankMovement>();
    }

    public void Respawn()
    {
        gameObject.transform.position = mSpawnPosition;
    }

    public bool IsInputDevicePaired(InputDevice inputDevice)
    {
        if (mInputUser != null)
        {
            foreach(InputDevice pairedDevice in mInputUser.pairedDevices)
            {
                if (pairedDevice == inputDevice)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void SetInputDevice(InputDevice device, bool enableRightNow)
    {
        mInputUser = InputUser.PerformPairingWithDevice(device);

        mInputActions = new PlayerInputActions();
        mInputActions.Battle.NormalFire.performed += NormalFirePerformed;
        mInputActions.Battle.SpecialFire.performed += SpecialFirePerformed;

        mInputUser.AssociateActionsWithUser(mInputActions);

        if (enableRightNow)
        {
            SetInputEnable();
        }
    }

    public void SetInputEnable()
    {
        mInputActions?.Enable();
    }

    public void SetInputDisable()
    {
        mInputActions?.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void Update()
    {
        if (mInputActions != null)
        {
            Vector2 moveVector = mInputActions.Battle.Move.ReadValue<Vector2>();
            if (Mathf.Abs(moveVector.x) > Mathf.Abs(moveVector.y))
            {
                moveVector.y = 0;
            }
            else
            {
                moveVector.x = 0;
            }

            if (moveVector.sqrMagnitude > 0.01f)
            {
                Debug.Log($"Player {mPlayerId} move {moveVector}");
                mTankMovement?.Move(new Vector3(moveVector.x, 0, moveVector.y));
            }
        }
    }

    private void NormalFirePerformed(InputAction.CallbackContext context)
    {
        Debug.Log($"Player {mPlayerId} normal fire!");
    }

    private void SpecialFirePerformed(InputAction.CallbackContext context)
    {
        Debug.Log($"Player {mPlayerId} special fire!");
    }
}
