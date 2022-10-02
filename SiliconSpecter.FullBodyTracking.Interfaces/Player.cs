﻿using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Interfaces
{
    /// <summary>
    /// The most recent information regarding a <see cref="Player"/> being tracked.
    /// </summary>
    public struct Player
    {
        /// <summary>
        /// The approximate position of the <see cref="Player"/>, in world space.
        /// </summary>
        public Vector3 ApproximatePosition;

        /// <summary>
        /// When known, details regarding the <see cref="Player"/>, otherwise, <see langword="null"/>.
        /// </summary>
        public PlayerDetails? Details;
    }
}
