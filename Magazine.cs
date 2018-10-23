using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine {

    private List<Projectile> projectiles = new List<Projectile>();
    public Projectile projectile;
    public int maxBullets;

    public Magazine(int size, Projectile projectile)
    {
        maxBullets = size;
        this.projectile = projectile;
        reload(size);
    }

    public Projectile removeBullet()
    {
        if (projectiles.Count == 0)
            return null;
        
        Projectile proj = projectiles[projectiles.Count-1];
        projectiles.Remove(proj);
        return proj;
    }

    public int amount()
    {
        return projectiles.Count;
    }

    public int reload(int amountLeft) //returns cost
    {
        int asumedcost = (maxBullets) - projectiles.Count;

        int cost = 0;
        for (int i = 0; i <= asumedcost; i++)
        {
            if (i < amountLeft)
            {
                cost++;
                projectiles.Add(projectile);
            }
            else break;
        }

        return cost;
    }
}
