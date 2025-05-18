using System.Collections.Generic;
using UnityEngine;

public class Bullet_Pool : MonoBehaviour
{
    public GameObject bulletPrefab;
    [SerializeField] int poolSize = 20;

    private List<GameObject> bullets;

    void Start()
    {
        bullets = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bullets.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }

        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.SetActive(false);
        bullets.Add(newBullet);
        return newBullet;
    }
}
