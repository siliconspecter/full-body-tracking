using System.Runtime.InteropServices;

namespace SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.Internals.Interop.FaceTrackLib;

/// <summary>
/// An image the face tracking library is able to work with.
/// </summary>
[ComImport, Guid("1A00A7BC-C217-11E0-AC90-0024811441FD"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IImage
{
  /// <summary>
  /// Allocates a backing buffer for the image.
  /// </summary>
  /// <param name="width">The width of the image, in pixel columns.</param>
  /// <param name="height">The height of the image, in pixel rows.</param>
  /// <param name="format">The format of the image.</param>
  void Allocate(uint width, uint height, ImageFormat format);

  /// <summary>
  /// Unused.
  /// </summary>
  /// <param name="width">Unused.</param>
  /// <param name="height">Unused.</param>
  /// <param name="data">Unused.</param>
  /// <param name="format">Unused.</param>
  /// <param name="stride">Unused.</param>
  [Obsolete]
  void Attach(uint width, uint height, IntPtr data, ImageFormat format, uint stride);

  /// <summary>
  /// Unused.
  /// </summary>
  [Obsolete]
  void Reset();

  /// <summary>
  /// Unused.
  /// </summary>
  /// <returns>Unused.</returns>
  [Obsolete]
  [PreserveSig]
  uint GetWidth();

  /// <summary>
  /// Unused.
  /// </summary>
  /// <returns>Unused.</returns>
  [Obsolete]
  [PreserveSig]
  uint GetHeight();

  /// <summary>
  /// Retrieves the number of <see cref="byte"/>s per pixel row.
  /// </summary>
  /// <returns>The number of <see cref="byte"/>s per pixel row.</returns>
  [PreserveSig]
  uint GetStride();

  /// <summary>
  /// Unused.
  /// </summary>
  /// <returns>Unused.</returns>
  [Obsolete]
  [PreserveSig]
  uint GetBytesPerPixel();

  /// <summary>
  /// Unused.
  /// </summary>
  /// <returns>Unused.</returns>
  [Obsolete]
  [PreserveSig]
  uint GetBufferSize();

  /// <summary>
  /// Unused.
  /// </summary>
  /// <returns>Unused.</returns>
  [Obsolete]
  ImageFormat GetFormat();

  /// <summary>
  /// Gets an <see cref="IntPtr"/> to the backing buffer.
  /// </summary>
  /// <returns>An <see cref="IntPtr"/> to the backing buffer.</returns>
  [PreserveSig]
  IntPtr GetBuffer();

  /// <summary>
  /// Unused.
  /// </summary>
  /// <returns>Unused.</returns>
  [Obsolete]
  [PreserveSig]
  bool IsAttached();

  /// <summary>
  /// Unused.
  /// </summary>
  /// <param name="destinationImage">Unused.</param>
  /// <param name="sourceRectangle">Unused.</param>
  /// <param name="destinationRow">Unused.</param>
  /// <param name="destinationColumn">Unused.</param>
  [Obsolete]
  void CopyTo(IImage destinationImage, IntPtr sourceRectangle, uint destinationRow, uint destinationColumn);

  /// <summary>
  /// Unused.
  /// </summary>
  /// <param name="startPoint">Unused.</param>
  /// <param name="endPoint">Unused.</param>
  /// <param name="color">Unused.</param>
  /// <param name="lineWidthPx">Unused.</param>
  [Obsolete]
  void DrawLine(Point startPoint, Point endPoint, uint color, uint lineWidthPx);
}
