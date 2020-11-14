using UnityEngine;

namespace Attributes
{
    [CreateAssetMenu(fileName = "ColorSchema", menuName = "Game/Add Color Schema", order = 1)]
    public class ColorSchema : ScriptableObject
    {
        [InspectorName("Health Color")]
        public Color32 healthColor = Color.white;
        [InspectorName("Quest Icon Color")]
        public Color32 questWarningColor = Color.white;
        public Color32 questInProgressColor = Color.white;
        public Color32 questCompleteColor = Color.white;
    }
}
