using UnityEngine;
using System.Collections;

namespace Cry.Common
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T instance { get; set; }
        protected virtual void Awake()
        {
            instance = (T)this;
        }
    }
}