using UnityEngine;

namespace JD.Utility.Saves
{
    public static class PlayerPrefsUtility
    {
        public static T GetSavedData<T>(string id, T defaultValue = default(T))
        {
            string key = GetKey(id);

            if (PlayerPrefs.HasKey(key))
            {
                string json = PlayerPrefs.GetString(key);
                return JsonUtility.FromJson<T>(json);
            }

            return defaultValue;
        }

        public static void SaveData<T>(string id, T data)
        {
            string key = GetKey(id);
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }

        public static int GetSavedInt(string id, int defaultValue = 0)
        {
            string key = GetKey(id);

            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetInt(key);
            }

            return defaultValue;
        }

        public static void SaveInt(string id, int value)
        {
            string key = GetKey(id);
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }

        private static string GetKey(string id)
        {
            return "SavedData_" + id;
        }

        public static void DeleteSave()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
