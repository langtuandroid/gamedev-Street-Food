using _Project.Scripts.Managers;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class LevelSoundInstaller : MonoInstaller
    {
        [SerializeField] private LevelSoundManager _levelSoundManager;
        public override void InstallBindings()
        {
            Container.Bind<LevelSoundManager>().FromInstance(_levelSoundManager).AsSingle();
        }
    }
}