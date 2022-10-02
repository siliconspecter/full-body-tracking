namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Interop.FaceTrackLib;

/// <summary>
/// The index of an animation unit.
/// </summary>
public enum AnimationUnit
{
    /// <summary>
    /// Positive values up to 1 indicate that the upper lip has drawn back to expose the upper teeth.
    /// Zero indicates that the upper lip is covering the upper teeth.
    /// Negative values down to -1 indicate that the upper lip is stretched further than the upper teeth.
    /// </summary>
    LipRaiser,

    /// <summary>
    /// Positive values up to 1 indicate that the jaw has been lowered.
    /// Zero to -1 indicate that the jaw is closed.
    /// </summary>
    JawLower,

    /// <summary>
    /// Positive values up to 1 indicate that the mouth is stretched wide.
    /// Negative values down to -1 indicate that the mouth is puckering.
    /// </summary>
    LipStretcher,

    /// <summary>
    /// Positive values up to 1 indicate that the inner brow has been lowered; suspicion or anger.
    /// Negative values down to -1 indicate that the inner brow has been raised; surprise.
    /// </summary>
    BrowLower,

    /// <summary>
    /// Positive values up to 1 indicate that the edges of the mouth have been lowered; a frown.
    /// Negative values down to -1 indicate that the edges of the mouth have been raised; a smile.
    /// </summary>
    LipCornerDepressor,

    /// <summary>
    /// Positive values up to 1 indicate that the outer brow has been lowered; surprise.
    /// Negative values down to -1 indicate that the outer brow has been raised; sadness.
    /// </summary>
    BrowRaiser,

    /// <summary>
    /// The number of animation units.
    /// </summary>
    Count,
}
