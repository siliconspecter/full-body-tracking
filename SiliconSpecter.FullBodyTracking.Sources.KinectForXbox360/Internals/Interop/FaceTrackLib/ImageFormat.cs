namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.FaceTrackLib;

/// <summary>
/// The format of an image sent to the face tracking library.
/// </summary>
public enum ImageFormat
{
  /// <summary>
  /// An 8-bits-per-pixel grayscale image.
  /// </summary>
  Luminance8 = 1,

  /// <summary>
  /// A 24-bits-per-pixel red-green-blue image.
  /// </summary>
  Red8Green8Blue8 = 2,

  /// <summary>
  /// A 32-bits-per-pixel red-green-blue image.
  /// </summary>
  Reserved8Red8Green8Blue8 = 3,

  /// <summary>
  /// A 32-bits-per-pixel alpha-red-green-blue image.
  /// </summary>
  Alpha8Red8Green8Blue8 = 4,

  /// <summary>
  /// A 32-bits-per-pixel blue-green-red image.
  /// </summary>
  Blue8Green8Red8Reserved8 = 5,

  /// <summary>
  /// A 32-bits-per-pixel blue-green-red-alpha image.
  /// </summary>
  Blue8Green8Red8Alpha8 = 6,

  /// <summary>
  /// A 16-bits-per-pixel depth image.
  /// </summary>
  Depth16 = 7,

  /// <summary>
  /// A 16-bits-per-pixel image, where 13 bits are depth and 3 bits are a player identifier.
  /// </summary>
  Depth13UserIdentifier3 = 8,
}
