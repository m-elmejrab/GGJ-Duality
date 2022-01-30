using UnityEngine;
public interface IGetPushedByAir
{
    public void StartApplyingVentForce(Vector2 ventForce);

    public void StopApplyingVentForce();
}