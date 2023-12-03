using Colorado.Systems;
using System;
using UnityEngine;

namespace Colorado.UI
{
    [Serializable]
    public sealed class Menu
    {
        private Animator _animator;

        public string Name;
        public GameObject Parent;

        public bool IsOpened { get; private set; } = false;

        public void Init()
        {
            _animator = Parent.GetComponent<Animator>();
        }

        public void Open()
        {
            IsOpened = true;
            Parent.SetActive(true);
            _animator.Play("Show");
            AudioManager.Instance.PlaySound(0);
        }

        public void Close()
        {
            IsOpened = false;
            Parent.SetActive(false);
        }
    }
}