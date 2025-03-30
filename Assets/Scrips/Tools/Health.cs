using UnityEngine;

public class Health : MonoBehaviour
{
    public float oxygenAmount = 20f;
    public float energyAmount = 30f;

    // 氧气胶囊
    public void UseOxygenCapsule(GameObject target) {
        if (target.TryGetComponent<PlayerStats>(out var player)) {
            player.currentOxygen += oxygenAmount;
            player.currentOxygen = Mathf.Clamp(player.currentOxygen, 0, player.maxOxygen);
            Destroy(gameObject);
        }
    }

    // 急救箱
    public void UseFirstAidKit(GameObject target) {
        if (target.TryGetComponent<PlayerStats>(out var player)) {
            player.currentEnergy += energyAmount;
            player.currentEnergy = Mathf.Clamp(player.currentEnergy, 0, player.maxEnergy);
            Destroy(gameObject);
        }
    }
}
