using _Project.Scripts.Managers;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class ChinaManagerInstaller : MonoInstaller
    {
        [SerializeField] private China_Manager _chinaManager;
        public override void InstallBindings()
        {
            Container.Bind<China_Manager>().FromInstance(_chinaManager).AsSingle();
        }
    }
}