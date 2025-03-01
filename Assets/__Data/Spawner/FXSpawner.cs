using UnityEngine;

public class FXSpawner : Spawner
{
    private static FXSpawner instance;
    public static FXSpawner Instance => instance;

    public static string blood1 = "Blood_1";
    public static string impact1 = "Impact_1";
    public static string meteorFire_1 = "MeteorFire_1";
    public static string hit_1 = "Hit_1";

    protected override void Awake()
    {
        base.Awake();
        if (FXSpawner.instance != null) Debug.LogError("Only 1 FXSpawner allow to exist");
        FXSpawner.instance = this;
    }
}
