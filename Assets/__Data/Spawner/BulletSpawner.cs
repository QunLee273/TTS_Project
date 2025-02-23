using UnityEngine;

public class BulletSpawner : Spawner
{
    private static BulletSpawner instance;
    public static BulletSpawner Instance => instance;

    public static string bullet1 = "Bullet_1";
    public static string bullet2 = "Bullet_2";
    public static string bullet3 = "Bullet_3";
    public static string meteoriteOne = "Meteorite_1";



    protected override void Awake()
    {
        base.Awake();
        if (BulletSpawner.instance != null) Debug.LogError("Only 1 BulletSpawner allow to exist");
        BulletSpawner.instance = this;
    }
}
