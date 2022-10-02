# Full Body Tracking

This is a crude full-body tracking library written in C#.

## License

Portions of this codebase were provided by third parties, such as Microsoft's face tracking library for the Kinect for Xbox 360.  These have been clearly identified by placing them in directories named `ThirdParty`.

Otherwise, I place no restrictions on what you use this codebase for (commercial or otherwise), but do not offer any support for it nor do I accept any liability for the consequences of its existence.

## Projects

- [SiliconSpecter.FullBodyTracking.Interfaces](./SiliconSpecter.FullBodyTracking.Interfaces) - Common interfaces between the components of this library.
- [SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360](./SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360) - Implements the source interface for the Kinect for Xbox 360.  You will need the Kinect SDK v1.8 installed to use this; the [face tracking library is included](./SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360/Interop/FaceTrackLib/ThirdParty) and must be loaded in some manner before attempting to use this project (how this done varies by operating environment).  Unit tests can be found in [SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.UnitTests](./SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.UnitTests).
