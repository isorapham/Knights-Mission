using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;
    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
        projectile.transform.localScale = transform.localScale; // copy hướng nhân vật
    }
}
