using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Interfaces
{
    /// <summary>
    /// Details regarding the extension of a <see cref="Player"/>'s <see cref="Limb"/>.
    /// </summary>
    public struct LimbExtension
    {
        /// <summary>
        /// The position of the arm's elbow joint/leg's knee joint, in world space, if known, otherwise, <see langword="null"/>.
        /// </summary>
        public Vector3? IntermediatePosition;

        /// <summary>
        /// The position of the arm's wrist joint/leg's ankle joint, in world space.
        /// </summary>
        public Vector3 DistalPosition;

        /// <summary>
        /// The position of the arm's middle fingertip/foot's middle toetip, in world space.
        /// </summary>
        public Vector3 TipPosition;
    }
}
