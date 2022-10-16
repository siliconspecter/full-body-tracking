using System;
using System.Numerics;

namespace SiliconSpecter.FullBodyTracking.Common
{
  /// <summary>
  /// Helper methods which are not associated with any particular aspect of the
  /// system.
  /// </summary>
  public static class Miscellaneous
  {
    /// <summary>
    /// Calculates an interior angle of a triangle, in radians.
    /// </summary>
    /// <param name="lengthOfNearSideA">
    /// The length of the first side touching the interior angle.
    /// </param>
    /// <param name="lengthOfNearSideB">
    /// The length of the second side touching the interior angle.
    /// </param>
    /// <param name="lengthOfFarSide">
    /// The length of the side which does not touch the interior angle.
    /// </param>
    /// <returns>The interior angle of the described triangle.</returns>
    public static double SolveGammaForLawOfCosines(double lengthOfNearSideA, double lengthOfNearSideB, double lengthOfFarSide)
    {
      return Math.Acos(Math.Max(-1, Math.Min(1, (lengthOfNearSideA * lengthOfNearSideA + lengthOfNearSideB * lengthOfNearSideB - lengthOfFarSide * lengthOfFarSide) / (2 * lengthOfNearSideA * lengthOfNearSideB))));
    }

    /// <summary>
    /// Generates a <see cref="Quaternion"/> which looks (along Z+) in a
    /// specified direction, with up (along Y+) in a specified direction.
    /// </summary>
    /// <param name="forward">The forward direction.</param>
    /// <param name="up">The upward direction.</param>
    /// <returns>The generated <see cref="Quaternion"/>.</returns>
    public static Quaternion LookAt(Vector3 forward, Vector3 up)
    {
      var forwardNormal = Vector3.Normalize(forward);
      var sideNormal = Vector3.Normalize(Vector3.Cross(up, forwardNormal));
      var upNormal = Vector3.Normalize(Vector3.Cross(forward, sideNormal));

      return Quaternion.CreateFromRotationMatrix(new Matrix4x4(
          sideNormal.X, sideNormal.Y, sideNormal.Z, 0,
          upNormal.X, upNormal.Y, upNormal.Z, 0,
          forwardNormal.X, forwardNormal.Y, forwardNormal.Z, 0,
          0, 0, 0, 1
      ));
    }
  }
}
