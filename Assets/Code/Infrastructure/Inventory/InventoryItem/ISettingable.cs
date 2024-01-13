using UnityEngine;

namespace Ddd.Infrastructure
{
    public interface ISettingable
    {
        public void SetParant(Transform parant);
        public void SetActive(bool activeted);
        public void SetActiveCollider(bool activeted);
        public void SetLocalPosition(Vector3 position);
        public void SetLocalRotation(Quaternion rotation);
        public void SetInvisibleColiderForSeconds(float second);

        public void BreakDependency();
    }
}