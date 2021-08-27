using System.Threading.Tasks;
using Common.Scripts.Planet;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.Background
{
    public class BgScroll : IMovableEnvironment
    {
        private readonly Material _material;
        private Vector2 _offset;
        private float _xVelocity = 0;
        private float _yVelocity;
        private float _moveSmoothness;

        public BgScroll(Renderer renderer,float moveSmoothness)
        {
            _material = renderer.material;
            _offset = new Vector2(_xVelocity, _yVelocity);
            _moveSmoothness = moveSmoothness;
        }
        
        private void ReinitializeOffset()
        {
            _offset = new Vector2(_xVelocity, _yVelocity).normalized * RocketSpeed.CurrentSpeed/_moveSmoothness;
        }

        private void ScrollFromRocketDir()
        {
            _xVelocity = RocketSpeed.GetRocketDirection().x;
            _yVelocity = RocketSpeed.GetRocketDirection().y;
            Task.Delay(1).ContinueWith(task => { if(task.IsCompleted) ReinitializeOffset();});
        }

        public void Move()
        {
            ScrollFromRocketDir();
            _material.mainTextureOffset += _offset * Time.deltaTime;
        }
        
    }
}