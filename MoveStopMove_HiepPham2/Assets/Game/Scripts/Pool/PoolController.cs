using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class PoolController : MonoBehaviour
{
    [Header("---- POOL CONTROLER TO INIT POOL ----")]
    [Header("Put object pool to list Pool or Resources/Pool")]

    [Space]
    [Header("Pool")]
    public List<PoolAmount> Pool;

    [Header("Particle")]
    public ParticleAmount[] Particle;


    public void Awake()
    {
        for (int i = 0; i < Pool.Count; i++)
        {
            SimplePool.Preload(Pool[i].prefab, Pool[i].amount, Pool[i].root, Pool[i].collect);
        }

        for (int i = 0; i < Particle.Length; i++)
        {
            ParticlePool.Preload(Particle[i].prefab, Particle[i].amount, Particle[i].root);
            ParticlePool.Shortcut(Particle[i].particleType, Particle[i].prefab);
        }
    }
}

[System.Serializable]
public class PoolAmount
{
    public Transform root;
    public GameUnit prefab;
    public int amount;
    public bool collect;

    public PoolAmount (Transform root, GameUnit prefab, int amount, bool collect)
    {
        this.root = root;
        this.prefab = prefab;
        this.amount = amount;
        this.collect = collect;
    }
}


[System.Serializable]
public class ParticleAmount
{
    public Transform root;
    public ParticleType particleType;
    public ParticleSystem prefab;
    public int amount;
}


public enum WeaponType
{
    W_Hammer_1 = PoolType.W_Hammer_1,
    W_Hammer_2 = PoolType.W_Hammer_2,
    W_Hammer_3 = PoolType.W_Hammer_3,
    W_Candy_1 = PoolType.W_Candy_1,
    W_Candy_2 = PoolType.W_Candy_2,
    W_Candy_3 = PoolType.W_Candy_3,
    W_Boomerang_1 = PoolType.W_Boomerang_1,
    W_Boomerang_2 = PoolType.W_Boomerang_2,
    W_Boomerang_3 = PoolType.W_Boomerang_3,
}

public enum BulletType
{
    B_Hammer_1 = PoolType.B_Hammer_1,
    B_Hammer_2 = PoolType.B_Hammer_2,
    B_Hammer_3 = PoolType.B_Hammer_3,
    B_Candy_1 = PoolType.B_Candy_1,
    B_Candy_2 = PoolType.B_Candy_2,
    B_Candy_3 = PoolType.B_Candy_3,
    B_Boomerang_1 = PoolType.B_Boomerang_1,
    B_Boomerang_2 = PoolType.B_Boomerang_2,
    B_Boomerang_3 = PoolType.B_Boomerang_3,
}

public enum HatType 
{
    HAT_None = 0,
    HAT_Arrow = PoolType.HAT_Arrow,
    HAT_Cap = PoolType.HAT_Cap,
    HAT_Cowboy = PoolType.HAT_Cowboy,
    HAT_Crown = PoolType.HAT_Crown,
    HAT_Ear = PoolType.HAT_Ear,
    HAT_StrawHat = PoolType.HAT_StrawHat,
    HAT_Headphone = PoolType.HAT_Headphone,
    HAT_Horn = PoolType.HAT_Horn,
    HAT_Police = PoolType.HAT_Police,
}

public enum SkinType
{
    SKIN_Normal = PoolType.SKIN_Normal,
    SKIN_Devil = PoolType.SKIN_Devil,
    SKIN_Angle = PoolType.SKIN_Angle,
    SKIN_Witch = PoolType.SKIN_Witch,
    SKIN_Deadpool = PoolType.SKIN_Deadpool,
    SKIN_Thor = PoolType.SKIN_Thor,
}

public enum AccessoryType
{
    ACC_None = 0,
    ACC_Book = PoolType.ACC_Book,
    ACC_CaptainShield = PoolType.ACC_Captain,
    ACC_Headphone = PoolType.ACC_Headphone,
    ACC_Shield = PoolType.ACC_Shield,
}

public enum PantType
{
    Pant_1,
    Pant_2,
    Pant_3,
    Pant_4,
    Pant_5,
    Pant_6,
    Pant_7,
    Pant_8,
    Pant_9,
}

public enum ParticleType
{
    Hit_1,
    Hit_2,
    Hit_3,

    LevelUp_1,
    LevelUp_2,
    LevelUp_3,
}

public enum PoolType
{
    None,

    Enemy,

    W_Hammer_1,
    W_Hammer_2,
    W_Hammer_3,
    W_Candy_1,
    W_Candy_2,
    W_Candy_3,
    W_Boomerang_1,
    W_Boomerang_2,
    W_Boomerang_3,

    B_Hammer_1,
    B_Hammer_2,
    B_Hammer_3,
    B_Candy_1,
    B_Candy_2,
    B_Candy_3,
    B_Boomerang_1,
    B_Boomerang_2,
    B_Boomerang_3,

    SKIN_Normal,
    SKIN_Devil,
    SKIN_Angle,
    SKIN_Witch,
    SKIN_Deadpool,
    SKIN_Thor,

    HAT_Arrow,
    HAT_Cap,
    HAT_Cowboy,
    HAT_Crown,
    HAT_Ear,
    HAT_StrawHat,
    HAT_Headphone,
    HAT_Horn,
    HAT_Police,

    ACC_Book,
    ACC_Captain,
    ACC_Headphone,
    ACC_Shield,

    TargetIndicator,
}
