using UnityEngine;

namespace JD.Utility.Saves
{
    [CreateAssetMenu(menuName = "Jamie Dovaston/Utility/SavePreferenceController", fileName = "SavePreferenceController")]
    public class SavePreferenceController : ScriptableObject
    {
        [ContextMenu("Delete Save")]
        public void DeleteSave()
        {
            PlayerPrefsUtility.DeleteSave();
        }
    }
}
