using _Project.Scripts.Managers;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class UsManagerInstaller : MonoInstaller
    {
        [SerializeField] private US_Manager _usManager;
       
        public override void InstallBindings()
        {
            Container.Bind<US_Manager>().FromInstance(_usManager).AsSingle();
        }
    }
}