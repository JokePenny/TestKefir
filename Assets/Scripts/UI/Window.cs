using UnityEngine;

namespace Assets.Scripts.UI
{
    public abstract class Window : MonoBehaviour
    {
        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
