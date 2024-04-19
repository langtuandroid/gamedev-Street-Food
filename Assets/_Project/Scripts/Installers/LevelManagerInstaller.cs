using _Project.Scripts.Managers;
using _Project.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class LevelManagerInstaller : MonoInstaller
    {
        [SerializeField] private LevelManager _levelManager;
        public override void InstallBindings()
        {
            Container.Bind<LevelManager>().FromInstance(_levelManager).AsSingle();
        }
    }
}