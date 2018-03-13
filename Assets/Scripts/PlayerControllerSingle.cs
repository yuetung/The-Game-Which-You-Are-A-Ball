/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerSingle : PlayerController {

    public void CmdShoot(Vector2 shootDirection)
    {
        Rigidbody2D projectile = projectileFactory.getProjectileFromType(elementType, elementLevel);
        Rigidbody2D clone;
        clone = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody2D;
        //GameObject cloneGameObject = clone.gameObject;
        //cloneGameObject.GetComponent<ProjectileController>().belongsToPlayer();
        //Vector2 velocity = shootDirection.normalized * playerShootSpeed * projectile.GetComponent<ProjectileController>().projectileSpeed;
        //float rotation = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        //cloneGameObject.GetComponent<ProjectileController>().setVelocityAndRotation(velocity, rotation);
        //// assigns a shooter to the bullet
        //cloneGameObject.GetComponent<ProjectileController>().shooter = transform.gameObject;
        depletesEnergy(elementLevel * 10);

    }

}
*/