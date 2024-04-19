using _Project.Scripts.Managers;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class ChinaManagerInstaller : MonoInstaller
    {
        [SerializeField] private ChinaController _chinaManager;
        public override void InstallBindings()
        {
            Container.Bind<ChinaController>().FromInstance(_chinaManager).AsSingle();
        }
    }
}