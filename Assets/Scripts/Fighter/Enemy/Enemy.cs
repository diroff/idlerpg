public class Enemy : Fighter
{
    protected override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
    }
}