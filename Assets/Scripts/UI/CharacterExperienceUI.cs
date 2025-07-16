using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterExperienceUI : MonoBehaviour
{
    [SerializeField] private Slider experienceSlider;
    [SerializeField] private TextMeshProUGUI amountOfExp;
    [SerializeField] private TextMeshProUGUI currentLvl;

    [SerializeField] private EXPData experienceData;

    public void UpdateExpInfo(float currentExp, float requireExp, int currentLvl)
    {
        experienceSlider.value = currentExp;
        experienceSlider.maxValue = requireExp;
        this.currentLvl.text = currentLvl.ToString();
        amountOfExp.text = currentExp.ToString() + "/" + requireExp.ToString();
    }
}
