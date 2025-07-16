using UnityEngine;

public class CharacterExperience : MonoBehaviour
{
    [SerializeField] private EXPData expData;
    [SerializeField] private CharacterExperienceUI characterExp;

    private int currentLevel;
    private float currentExp;

    private void Start()
    {
        EnemySpawner.OnRemove += ExperienceHandler;

        currentLevel = 0;
        currentExp = 0;
        UpdateExp(0);
        
        PlayerManager.instance.LVL = currentLevel;
    }

    // increase and omptimize that logic for different enemy types(bosses, enemies)
    private void ExperienceHandler(int level)
    {
        float result = 100 * (level - currentLevel + 1);

        UpdateExp(result);
    }

    private void UpdateExp(float exp)
    {
        currentExp += exp;

        while (currentExp >= expData.GetAmountExpByLvl(currentLevel))
        {
            currentExp -= expData.GetAmountExpByLvl(currentLevel);
            currentLevel++;

            if (currentLevel >= expData.levels.Length)
            {
                currentLevel = expData.levels.Length;
                currentExp = 0;
                break;
            }
        }

        PlayerManager.instance.LVL = currentLevel;
        characterExp.UpdateExpInfo(currentExp, expData.GetAmountExpByLvl(currentLevel), currentLevel);
    }

    private void OnDisable()
    {
        EnemySpawner.OnRemove -= ExperienceHandler;
    }
}
