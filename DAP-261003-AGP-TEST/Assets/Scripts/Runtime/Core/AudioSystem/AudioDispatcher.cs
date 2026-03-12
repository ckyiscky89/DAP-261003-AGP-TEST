using DAP.Runtime.Data;
using System;

namespace DAP.Runtime.Core
{
    public static class AudioDispatcher
    {
        public static Action<SFXType> onPlaySFX;
        public static void PlaySFX(SFXType type) => onPlaySFX?.Invoke(type);
    }
}