using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item/Other")]
public class ItemOtherData : ScriptableObject
{
    // refactoring
    [field: SerializeField] public Sprite Sprite { get; private set; }

    [field: SerializeField, Space] public float TimeAnimationTake { get; private set; }
    [field: SerializeField] public AnimationCurve AnimationTake { get; private set; }
}