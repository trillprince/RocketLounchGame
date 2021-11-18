using System;
using Common.Scripts.Audio;
using Common.Scripts.Cargo;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketBoosterController: IUpdatable
    {
        private readonly RocketController _controller;
        private Func<GameObject,Transform,GameObject> _instantiate;
        private readonly Action<GameObject> _destroyGo;
        public event Action OnEffectDiscard;
        private RocketEffect _rocketEffect;
        private GameObject _currentEffectObject;
        private IAudioManager _audioManager;

        public RocketBoosterController(RocketController controller,
            Func<GameObject, Transform, GameObject> instantiate, Action<GameObject> destroyGo)
        {
            _controller = controller;
            _audioManager = _controller.Audio.GetAudioManager();
            _instantiate = instantiate;
            _destroyGo = destroyGo;
        }

        public void ApplyHealthBooster(RocketEffect rocketEffect)
        {
            if(rocketEffect == _rocketEffect) return;
            _rocketEffect = rocketEffect;
            _rocketEffect.Boost(DiscardEffect);
            InstantiateEffect(_rocketEffect.GetEffectGameObject());
        }

        public void ApplyBooster(RocketEffect rocketEffect)
        {
            if(rocketEffect == _rocketEffect) return;
            _audioManager.FxAudioClipSetActive("Booster Pick Up",true);
            rocketEffect.AudioActive(true);
            _rocketEffect = rocketEffect;
            _rocketEffect.Boost(DiscardEffect);
        }

        public bool ContainsBooster()
        {
            if (_rocketEffect != null)
            {
                return true;
            }

            return false;
        }

        private void InstantiateEffect(GameObject gameObject)
        {
            _currentEffectObject = _instantiate?.Invoke(gameObject,_controller.Movement.GetTransform());
        }

        private void DiscardEffect()
        {
            _rocketEffect.AudioActive(false);
            _rocketEffect = null;
            _destroyGo?.Invoke(_currentEffectObject);
            OnEffectDiscard?.Invoke();
        }


        public void Execute()
        {
            if(_rocketEffect == null) return;
            _rocketEffect.Execute();
        }
    }
}