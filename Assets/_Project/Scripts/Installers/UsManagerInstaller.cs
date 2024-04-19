using _Project.Scripts.Managers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class UsManagerInstaller : MonoInstaller
    {
        [SerializeField] private USController _usManager;
       
        public override void InstallBindings()
        {
            Container.Bind<USController>().FromInstance(_usManager).AsSingle();
        }
    }
}