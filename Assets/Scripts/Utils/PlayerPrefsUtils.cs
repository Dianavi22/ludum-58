using UnityEngine;
using static UnityEngine.PlayerPrefs;

namespace Rewards.Utils
{
    public static class PlayerPrefsUtils
    {
        public static int TryGetInt(string key, int defaultValue = 0)
        {
            if (HasKey(key))
            {
                return GetInt(key);
            }

            SetInt(key, defaultValue);
            return GetInt(key);
        }

        public static float TryGetFloat(string key, float defaultValue = 0)
        {
            if (HasKey(key)) return GetFloat(key);

            SetFloat(key, defaultValue);
            return GetFloat(key);
        }

        public static string TryGetString(string key, string defaultValue = "")
        {
            if (HasKey(key)) return GetString(key);

            SetString(key, defaultValue);
            return GetString(key);
        }

        public static void SetBool(string key, bool boolVar)
        {
            var intVar = 0;
            if (!boolVar) intVar = 1;
            SetInt(key, intVar);
        }

        public static bool TryGetBool(string key, bool defaultValue = true)
        {
            var boolVar = defaultValue;
            if (HasKey(key))
            {
                var a = GetInt(key);
                boolVar = a == 0;
                return boolVar;
            }

            SetInt(key, 0);
            return boolVar;
        }
    }
}