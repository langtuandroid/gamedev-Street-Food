using _Project.Scripts.Entities.Customers;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class CustomerManagerInstaller : MonoInstaller
    {
        [SerializeField] private WisitorHandler _customerHandler;
       
        public override void InstallBindings()
        {
            Container.Bind<WisitorHandler>().FromInstance(_customerHandler).AsSingle();
        }
    }
}