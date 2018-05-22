using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minego
{
    public interface IButtonControlled
    {
        bool Active { get; }
        void Activate();
        void Deactivate();
    }
}