using System;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "Game Data/Enemy Data")]
public class Eneemy_ScritableObj : ScriptableObject
{
    public EnemyDataShow[] enemyDataShows;
}

[Serializable]
public class EnemyDataShow
{
    public string enemyName;
    public float radius;
    public float attackDistance;
    public float shootForce;
}