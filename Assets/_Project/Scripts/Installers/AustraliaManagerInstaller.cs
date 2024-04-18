using _Project.Scripts.Managers;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class AustraliaManagerInstaller : MonoInstaller
    {
        [SerializeField] private Australia_Manager _australiaManager;
        public override void InstallBindings()
        {
            Container.Bind<Australia_Manager>().FromInstance(_australiaManager).AsSingle();
        }
    }
}