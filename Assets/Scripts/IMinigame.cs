using System;
using Classes;
using UnityEngine;

namespace DefaultNamespace
{
    public interface IMinigame
    {
        public Task Task { get; set; }
    }
}