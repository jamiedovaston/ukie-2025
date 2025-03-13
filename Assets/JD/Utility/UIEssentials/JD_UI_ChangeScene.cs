using UnityEngine;
using JD.SceneManagement;

namespace JD.Utility.UIEssentials
{
    public class JD_UI_ChangeScene : MonoBehaviour
    {
        public void Trigger(string _id) => JD_SceneManager.Instance.LoadScene(_id);
    }
}
