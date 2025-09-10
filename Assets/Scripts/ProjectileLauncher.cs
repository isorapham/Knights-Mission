using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;
    public void FireProjectile()
    {
         Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);
    }
}
