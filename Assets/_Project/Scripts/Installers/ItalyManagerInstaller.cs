using _Project.Scripts.Managers;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class ItalyManagerInstaller : MonoInstaller
    {
        [SerializeField] private Italy_Manager _italyManager;
        public override void InstallBindings()
        {
            Container.Bind<Italy_Manager>().FromInstance(_italyManager).AsSingle();
        }
    }
}