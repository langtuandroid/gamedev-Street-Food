using _Project.Scripts.Managers;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class AustraliaManagerInstaller : MonoInstaller
    {
        [SerializeField] private AustraliaController _australiaManager;
        public override void InstallBindings()
        {
            Container.Bind<AustraliaController>().FromInstance(_australiaManager).AsSingle();
        }
    }
}