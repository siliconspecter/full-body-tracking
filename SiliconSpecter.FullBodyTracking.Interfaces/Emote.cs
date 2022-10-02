using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Interfaces
{
    /// <summary>
    /// The emote currently being displayed by a <see cref="Player"/>.
    /// </summary>
    public enum Emote
    {
        /// <summary>
        /// The <see cref="Player"/> is not currently making any particular emote.
        /// </summary>
        Neutral,

        /// <summary>
        /// The <see cref="Player"/> is currently making an angry emote.
        /// </summary>
        Angry,

        /// <summary>
        /// The <see cref="Player"/> is currently making a surprised emote.
        /// </summary>
        Surprised,

        /// <summary>
        /// The <see cref="Player"/> is currently making a joy (smiling) emote.
        /// </summary>
        //
        Joy,
    }
}
