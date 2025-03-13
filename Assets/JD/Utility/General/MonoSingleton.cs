using UnityEngine;

namespace JD.Utility.General
{
    /// <summary>
    /// Singelton.
    /// </summary>
    /// <typeparam name="T"> Class reference</typeparam>
    public class MonoSingleton<T> : Singleton<T> where T : MonoSingleton<T>
    {
        protected override void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
