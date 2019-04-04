using UnityEngine;


public enum Direction
{
    Left, Right, Idle
}

public class PlayerMovement : MonoBehaviour
{
    private Direction direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = Direction.Idle;
    }


    // Update is called once per frame
    void Update()
    {
        InputUpdate();
    }

    private void InputUpdate()
    {
        if (Input.GetKey(CfgVariables.GetMovementLeftKey())) {
            direction = Direction.Left;
        } else if (Input.GetKey(CfgVariables.GetMovementRightKey())) {
            direction = Direction.Right;
        } else {
            direction = Direction.Idle;
        }
    }

    public Direction GetDirection()
    {
        return direction;
    }

}
