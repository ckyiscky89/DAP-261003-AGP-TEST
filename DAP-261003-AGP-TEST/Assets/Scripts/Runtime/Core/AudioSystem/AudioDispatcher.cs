using DAP.Runtime.Data;
using System;

namespace DAP.Runtime.Core
{
    public static class AudioDispatcher
    {
        public static Action<SFXType> OnPlaySFX;
        public static void PlaySFX(SFXType type) => OnPlaySFX?.Invoke(type);
    }
}