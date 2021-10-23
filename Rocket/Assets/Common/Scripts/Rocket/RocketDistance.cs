using System.Collections;
using Common.Scripts.Cargo;
using Common.Scripts.Infrastructure;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketDistance: IGameStateSubscriber
    {
        private readonly RocketSpeed _rocketSpeed;
        private readonly ICoroutineRunner _coroutineRunner;
        private float _coveredDistance = 0;
        private bool _launched;
        private float _lerpDuration = 0.02f;
        private float _timeElapsed;
        private Coroutine _coroutine;

        public RocketDistance(RocketSpeed rocketSpeed,ICoroutineRunner coroutineRunner)
        {
            _rocketSpeed = rocketSpeed;
            _coroutineRunner = coroutineRunner;
        }

        public void OnGameStateChange(GameState gameState)
        {
            if (gameState == GameState.CargoDrop)
            {
                _coroutine = _coroutineRunner.StartCoroutine(CalculateCoveredDistance());
            }
        }
        private IEnumerator CalculateCoveredDistance()
        {
            var coveredDistance = _coveredDistance;
            while (_timeElapsed < _lerpDuration)
            {
                
                _coveredDistance = Mathf.Lerp(_coveredDistance, coveredDistance + _rocketSpeed.GetCurrentSpeed()/50, _timeElapsed / _lerpDuration);
                _timeElapsed += Time.deltaTime;
                yield return null;
            }
            if (_timeElapsed >= _lerpDuration)
            {
                _timeElapsed = 0;
                _coroutine = null;
                _coroutine = _coroutineRunner.StartCoroutine(CalculateCoveredDistance());
            }

        }

        public int GetCoveredDistance()
        {
            return (int)_coveredDistance;
        }

    }
}