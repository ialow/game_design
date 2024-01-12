namespace Ddd.Infrastructure
{
    public interface IDamagable
    {
        public void GetDamage(float damage);
        public void OnDeath();
        public void OnRevival();
    }
}