using UnityEngine;

public class Health : MonoBehaviour
{
    private int HP;

    public int GetHp()
    {
        return this.HP;
    }

    public void GetDamage(int damage)
    {
        this.HP -= damage;
    }
}
