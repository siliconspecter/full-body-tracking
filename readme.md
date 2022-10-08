# Full Body Tracking

This is a crude full-body tracking library written in C#.

## License

Portions of this codebase were provided by third parties, such as Microsoft's
face tracking library for the Kinect for Xbox 360.  These have been clearly
identified by placing them in directories named `ThirdParty`.

Otherwise, this codebase is MIT licensed.

The license file can be found [here](./license).

## Projects

- [SiliconSpecter.FullBodyTracking.Common](./SiliconSpecter.FullBodyTracking.Common) - Common data types, interfaces and implementations shared between the other projects of this library.  Unit tests can be found in [SiliconSpecter.FullBodyTracking.Common.UnitTests](./SiliconSpecter.FullBodyTracking.Common.UnitTests).
- [SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360](./SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360) - Implements the source interface for the Kinect for Xbox 360.  You will need the Kinect SDK v1.8 installed to use this; the [face tracking library is included](./SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360/Interop/FaceTrackLib/ThirdParty) and must be loaded in some manner before attempting to use this project (how this done varies by operating environment).  Unit tests can be found in [SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.UnitTests](./SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.UnitTests).
