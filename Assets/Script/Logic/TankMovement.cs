using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public int alignFrameSpeed = 3;

    private Vector3 mCurrentDirection = Vector3.zero;
    private int mAlignPositionFrameLeft = 0;
    private Vector3 mAlignPositionStep = Vector3.zero;

    public void Move(Vector3 moveDirection)
    {
        mCurrentDirection = moveDirection;
        mAlignPositionFrameLeft = 0;
    }

    public void Update()
    {
        Vector3 pos = gameObject.transform.position;

        if (mCurrentDirection != Vector3.zero)
        {
            pos += moveSpeed * Time.deltaTime * mCurrentDirection;
            gameObject.transform.position = pos;
            gameObject.transform.forward = mCurrentDirection;
            mCurrentDirection = Vector3.zero;
            return;
        }

        if (mAlignPositionFrameLeft > 0)
        {
            if (mAlignPositionFrameLeft == 1)
            {
                pos.x = Mathf.Round(pos.x);
                pos.y = Mathf.Round(pos.y);
                pos.z = Mathf.Round(pos.z);
            }
            else
            {
                // Debug.Log($"Current pos {pos}, Delta {mAlignPositionStep}");
                pos += mAlignPositionStep;
                gameObject.transform.position = pos;
            }
            
            mAlignPositionFrameLeft--;
            // Debug.Log($"Applyed pos {pos}");
            return;
        }

        Vector3 forward = gameObject.transform.forward;
        float deltaX = (forward.x > 0 ? Mathf.Ceil(pos.x) : Mathf.Floor(pos.x)) - pos.x;
        float deltaZ = (forward.z > 0 ? Mathf.Ceil(pos.z) : Mathf.Floor(pos.z)) - pos.z;

        bool alignPositionStarted = false;
        if (Mathf.Abs(deltaX) > 0.01f && Mathf.Abs(deltaX) < 0.99f)
        {
            alignPositionStarted = true;
        }
        else
        {
            deltaX = 0;
        }

        if (Mathf.Abs(deltaZ) > 0.01f && Mathf.Abs(deltaZ) < 0.99f)
        {
            alignPositionStarted = true;
        }
        else
        {
            deltaZ = 0;
        }

        if (alignPositionStarted)
        {
            mAlignPositionStep = new Vector3(deltaX / alignFrameSpeed, 0, deltaZ / alignFrameSpeed);
            mAlignPositionFrameLeft = alignFrameSpeed;
        }
    }
}
