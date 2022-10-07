using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Details regarding a posed character model.
  /// </summary>
  public struct InverseKinematics
  {
    /// <summary>
    /// The position of the hips bone on the Y axis, in meters, in local space.
    /// </summary>
    public float HipsY;

    /// <summary>
    /// The rotation of the hips bone, in world space.
    /// </summary>
    public Quaternion HipsRotation;

    /// <summary>
    /// The rotation of the shoulder bone, relative to the <see cref="HipsRotation"/>.
    /// </summary>
    public Quaternion ShoulderBone;

    /// <summary>
    /// The character's left arm.
    /// </summary>
    public InverseKinematicsLimb LeftArm;

    /// <summary>
    /// The character's right arm.
    /// </summary>
    public InverseKinematicsLimb RightArm;

    /// <summary>
    /// The character's left leg.
    /// </summary>
    public InverseKinematicsLimb LeftLeg;

    /// <summary>
    /// The character's right leg.
    /// </summary>
    public InverseKinematicsLimb RightLeg;
  }
}
