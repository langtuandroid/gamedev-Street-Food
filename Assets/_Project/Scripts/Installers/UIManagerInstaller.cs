using _Project.Scripts.UI_Scripts;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class UIManagerInstaller : MonoInstaller
    {
        [SerializeField] private UIManager _uiManager;
        public override void InstallBindings()
        {
            Container.Bind<UIManager>().FromInstance(_uiManager).AsSingle();
        }
    }
}