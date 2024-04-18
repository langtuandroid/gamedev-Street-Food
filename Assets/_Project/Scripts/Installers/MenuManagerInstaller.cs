using _Project.Scripts.UI_Scripts;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class MenuManagerInstaller : MonoInstaller
    {
        [SerializeField] private MenuManager _menuManager;
        public override void InstallBindings()
        {
            Container.Bind<MenuManager>().FromInstance(_menuManager).AsSingle();
        }
    }
}