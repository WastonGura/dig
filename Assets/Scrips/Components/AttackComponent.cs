using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    public float attackDamage = 20f;

    public float GetAttackDamage(){
        return attackDamage;
    }

    public void SetAttackDamage(float newAttack){
        attackDamage = newAttack; 
    }
}
