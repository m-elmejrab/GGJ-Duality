using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyAttackArea : MonoBehaviour
{
    public float damageRatio = 0.2f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayerMovement pMovement = collision.gameObject.GetComponent<PlayerMovement>();
            pMovement.TakeDamage(damageRatio);
        }
    }
}
