using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enitites.Interfaces
{
    public interface IDamagable
    {
        public event Action<GameObject> EventGetDamage;
        public void GetDamage();
    }
}
