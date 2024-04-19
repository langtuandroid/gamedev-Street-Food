using _Project.Scripts.Managers;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class ItalyManagerInstaller : MonoInstaller
    {
        [SerializeField] private ItalyController _italyManager;
        public override void InstallBindings()
        {
            Container.Bind<ItalyController>().FromInstance(_italyManager).AsSingle();
        }
    }
}