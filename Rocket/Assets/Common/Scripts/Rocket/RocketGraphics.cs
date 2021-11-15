using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketGraphics : MonoBehaviour
    {
        private List<Material> _materials;
        private List<MeshRenderer> _renderers;

        private void Awake()
        {
            _renderers = new List<MeshRenderer>(GetComponentsInChildren<MeshRenderer>().ToArray());
            _materials = new List<Material>();
            foreach (var renderer in _renderers)
            {
                foreach (var material in renderer.materials)
                {
                    _materials.Add(material);
                }
            }
        }

        private void Start()
        {
            SetShadersDefault();
        }

        public void SetShadersDefault()
        {
            SetShader(Shader.Find("Universal Render Pipeline/Lit"));
        }

        public void SetShader(Shader shader)
        {
            foreach (var material in _materials)
            {
                material.shader = shader;
            }
        }
    }
}