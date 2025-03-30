using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image oxygenSlider;
    public Image energySlider;
    public void UpdateUI(float currentOxygen, float maxOxygen, float currentEnergy, float maxEnergy)
    {
        oxygenSlider.fillAmount = currentOxygen / maxOxygen;
        energySlider.fillAmount = currentEnergy / maxEnergy;
    }
}
