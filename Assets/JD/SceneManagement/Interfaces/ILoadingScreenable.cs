namespace JD.SceneManagement
{
    public interface ILoadingScreenable
    {
        public void SetActive(bool isActive);
        public void SetTitle(string title);
        public void SetProgress(float progress);
    }
}

