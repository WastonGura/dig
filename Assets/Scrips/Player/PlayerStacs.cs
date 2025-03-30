using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Settings")]
    public float maxOxygen = 200f;
    public float maxEnergy = 100f;
    public float oxygenDepletionRate = 0.5f;
    public float fallDamageRate = 10f;
    public float fallHeightThreshold = 5f;
    private float lastYPosition;


    public float currentOxygen;
    public float currentEnergy;
    private float depthMultiplier;
    public UIManager uiManager;

    void Awake()
    {
        currentOxygen = maxOxygen;
        currentEnergy = maxEnergy;
        lastYPosition = transform.position.y;
    }

    void Update()
    {
        float depth = Mathf.Abs(transform.position.y);
        depthMultiplier = 1 + depth * 0.1f;
        currentOxygen -= Time.deltaTime * oxygenDepletionRate * depthMultiplier;
        currentOxygen = Mathf.Clamp(currentOxygen, 0, maxOxygen);

        if (currentOxygen <= 0)
        {
            currentEnergy -= Time.deltaTime * 5f;
        }

        float fallDistance = lastYPosition - transform.position.y;
        if (fallDistance > fallHeightThreshold){
            currentEnergy -= fallDistance * fallDamageRate;
            currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        }

        lastYPosition = transform.position.y;

        uiManager.UpdateUI(currentOxygen, maxOxygen, currentEnergy, maxEnergy);
    }

    public void RestoreOxygen(float amount){
        currentOxygen = Mathf.Min(maxOxygen, currentOxygen + amount);
    }

}