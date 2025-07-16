using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Character Data", menuName ="Experience")]
public class EXPData : ScriptableObject
{
    public Level[] levels;

    public float GetAmountExpByLvl(int lvl)
    {
        return levels[lvl].amountOfExperience;
    }
}

[Serializable]
public class Level
{
    public int level;
    public float amountOfExperience;
}
