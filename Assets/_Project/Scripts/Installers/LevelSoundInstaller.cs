using _Project.Scripts.Managers;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class LevelSoundInstaller : MonoInstaller
    {
        [SerializeField] private SoundsAll _levelSoundManager;
        public override void InstallBindings()
        {
            Container.Bind<SoundsAll>().FromInstance(_levelSoundManager).AsSingle();
        }
    }
}