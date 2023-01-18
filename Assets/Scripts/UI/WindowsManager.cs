using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public sealed class WindowsManager : MonoBehaviour
    {
        [SerializeField] private Transform _rootWindows;
        [SerializeField] private Window[] _windowPrefabs;
        private List<Window> _listWindows;

        public void Initialize()
        {
            _listWindows = new List<Window>();
            foreach (var window in _windowPrefabs)
            {
                var newWindow = GameObject.Instantiate(window, _rootWindows);
                newWindow.gameObject.SetActive(false);
                _listWindows.Add(newWindow);
            }
        }

        public void Open<T>() where T : Window
        {
            var window = GetWindow<T>();
            window?.Open();
        }

        public void Close<T>() where T : Window
        {
            var window = GetWindow<T>();
            window?.Close();
        }

        public Window GetWindow<T>() where T : Window
        {
            int indexWindow = _listWindows.FindIndex(window => typeof(T) == window.GetType());

            if (indexWindow != -1)
            {
                return _listWindows[indexWindow];
            }

            return null;
        }
    }
}
