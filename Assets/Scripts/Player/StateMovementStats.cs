using System;

[Serializable]
public struct StateMovementStats
{

    public StateMovementStats(float mov, float ma, float j)
    {
        moveSpeed = mov;
        maxSpeed = ma;
        jumpSpeed = j;
    }

    public float moveSpeed;

    public float maxSpeed;

    public float jumpSpeed;


    public override String ToString()
    {
        return String.Format("mov: {0}, max: {1}, jump: {2}", moveSpeed, maxSpeed, jumpSpeed);
    }


    public StateMovementStats validate()
    {

        return new StateMovementStats(
            Math.Max(1, moveSpeed),
            Math.Max(1, maxSpeed),
            Math.Max(1, jumpSpeed)
        );
    }


}

