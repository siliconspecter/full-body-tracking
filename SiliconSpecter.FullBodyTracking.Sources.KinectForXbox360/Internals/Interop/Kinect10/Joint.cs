namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Interop.Kinect10;


/// <summary>
/// The index of a joint within a <see cref="Skeleton"/>.
/// </summary>
public enum Joint
{
    /// <summary>
    /// The center of the shoulders; closest to the neck.
    /// </summary>
    Neck = 2,

    /// <summary>
    /// The top of the head.
    /// </summary>
    TopOfHead = 3,

    /// <summary>
    /// The left shoulder.
    /// </summary>
    LeftShoulder = 4,

    /// <summary>
    /// The left elbow.
    /// </summary>
    LeftElbow = 5,

    /// <summary>
    /// The left wrist.
    /// </summary>
    LeftWrist = 6,

    /// <summary>
    /// The tip of the left middle finger.
    /// </summary>
    LeftMiddleFingertip = 7,

    /// <summary>
    /// The right shoulder.
    /// </summary>
    RightShoulder = 8,

    /// <summary>
    /// The right elbow.
    /// </summary>
    RightElbow = 9,

    /// <summary>
    /// The right wrist.
    /// </summary>
    RightWrist = 10,

    /// <summary>
    /// The tip of the right middle finger.
    /// </summary>
    RightMiddleFingertip = 11,

    /// <summary>
    /// The left hip.
    /// </summary>
    LeftHip = 12,

    /// <summary>
    /// The left knee.
    /// </summary>
    LeftKnee = 13,

    /// <summary>
    /// The left ankle.
    /// </summary>
    LeftAnkle = 14,

    /// <summary>
    /// The left foot.
    /// </summary>
    LeftFoot = 15,

    /// <summary>
    /// The right hip.
    /// </summary>
    RightHip = 16,

    /// <summary>
    /// The right knee.
    /// </summary>
    RightKnee = 17,

    /// <summary>
    /// The right ankle.
    /// </summary>
    RightAnkle = 18,

    /// <summary>
    /// The right foot.
    /// </summary>
    RightFoot = 19,

    /// <summary>
    /// The number of joints in total (not all are documented here).
    /// </summary>
    Count = 20,
}
