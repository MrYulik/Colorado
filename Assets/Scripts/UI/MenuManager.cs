using Colorado.Core;
using Colorado.Systems;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Colorado.UI
{
    public sealed class MenuManager : BaseSingeltonSystem<MenuManager>
    {
        [SerializeField] private Menu[] _menus;
        [SerializeField] private Toggle _audioMute;

        public override void StartGame()
        {
            foreach (var menu in _menus)
                menu.Init();

            OpenMenu("main");

            _audioMute.onValueChanged.AddListener(MuteSettingChanged);
        }

        private void OnDestroy()
        {
            _audioMute.onValueChanged.RemoveAllListeners();
        }

        private void MuteSettingChanged(bool active) => AudioManager.Instance.IsMute = active;

        public void OpenMenu(string name)
        {
            foreach(var menu in _menus)
            {
                if(menu.IsOpened)
                {
                    menu.Close();
                }

                if(menu.Name == name)
                {
                    menu.Open();
                }
            }
        }

        public void CloseAllMenus() => _menus.Where(x => x.IsOpened).ToList().ForEach(x => x.Close());

        public void OpenURL(string url) => Application.OpenURL(url);
    }
}
