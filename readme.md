# Full Body Tracking

This is a crude full-body tracking library written in C#.  It's primarily
intended for people on the outside to join me here on the inside while the full
immersive tech is invented in this world too (I hope, some day!).

## License

Portions of this codebase were provided by third parties, such as Microsoft's
face tracking library for the Kinect for Xbox 360.  These have been clearly
identified by placing them in directories named `ThirdParty`.

Otherwise, this codebase is MIT licensed.

The license file can be found [here](./license).

## Projects

- [SiliconSpecter.FullBodyTracking.Common](./SiliconSpecter.FullBodyTracking.Common) - Common data types, interfaces and implementations shared between the other projects of this library.  Unit tests can be found in [SiliconSpecter.FullBodyTracking.Common.UnitTests](./SiliconSpecter.FullBodyTracking.Common.UnitTests).
- [SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360](./SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360) - Implements the source interface for the Kinect for Xbox 360.  You will need the Kinect SDK v1.8 installed to use this; the [face tracking library is included](./SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360/Interop/FaceTrackLib/ThirdParty) and must be loaded in some manner before attempting to use this project (how this done varies by operating environment).  Unit tests can be found in [SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.UnitTests](./SiliconSpecter.FullBodyTracking.Sources.KinectForXbox360.UnitTests).

## How to use it

### Installation

#### Within a Unity project

You're not supposed to do this and Unity will rightly tell you off for trying
it, but you _can_ just add this repository as a submodule to your own and
add symlinks to the projects you want to include.  Any non-test projects should
work under Unity on Windows at least.

#### Kinect for Xbox 360

You will need to install the [Kinect for Windows Runtime v1.8](https://www.microsoft.com/en-gb/download/details.aspx?id=40277)
to use this project.  Version 2 and up are incompatible as they only work with
the Kinect for Xbox One.

### Flow

First, use an [ISource](./SiliconSpecter.FullBodyTracking.Common/ISource.cs),
which represents a motion capture device, to get one or more
[Player](./SiliconSpecter.FullBodyTracking.Common/Player.cs)s; it will raise
events when it sees new players.

Initially, you will only get a rough position for each player, so inform the
source which players you wish to track in detail.  Most motion capture devices
have hard limits on how many players you can track in detail, and can sometimes
misidentify background objects as players, so don't just say all of them!

Use [IPlayerToMetricsConverter](./SiliconSpecter.FullBodyTracking.Common/IPlayerToMetricsConverter.cs)
to measure the performer, and pass those measurements and the most recent player
details to [IPlayerToKeyframeConverter](./SiliconSpecter.FullBodyTracking.Common/IPlayerToKeyframeConverter.cs).

When polling the source for updates to player data, one of three things can
happen:

- If the motion capture device has lost tracking, you will receive null.
- If the motion capture device does not yet have an update, you will receive the
  exact same player data again.
- Otherwise, you will receive player data with a different frame number to the
  previous update for that player.

[IInterpolator](./SiliconSpecter.FullBodyTracking.Common/IInterpolator.cs) can be
used to generate data for frames between updates from the motion capture device.

Once interpolated, pass the resulting keyframe data to
[IInverseKinematicsCalculator](./SiliconSpecter.FullBodyTracking.Common/IInverseKinematicsCalculator.cs)
to get a set of [InverseKinematics](./SiliconSpecter.FullBodyTracking.Common/InverseKinematics.cs).
This describes the final world-space coordinates which are to be applied to the
character model.

You will need to generate a [BindPose](./SiliconSpecter.FullBodyTracking.Common/BindPose.cs)
from the character model you will be animating to calculate inverse kinematics.

## How it works

The Kinect can pass through the positions of various joints; shoulders, elbows,
etc. regarding up to two players at a time.  The original idea was to just take
this "skeleton" data, and put it into rigged skeletal meshes.

### Missing rotational data

The Kinect gives us joint positions, but not rotations.  Depending upon the
bone, we can infer rotation in different ways.

#### Limbs

Arms, hands, legs and feet can be oriented using one another; for example, the
upper arm is positioned at the shoulder and points at the elbow, while the hand
uses the elbow to generate roll.

#### Head

The head is a bit more of a challenge; the Kinect only gives us a "neck" and
a "head" joint, basically the top and bottom of the skull, so we're unable to
detect yaw with just the skeleton data (and the pitch/roll is somewhat vague).

To work around this, we are able to use some facial recognition technology with
the Kinect's color camera to determine head rotation (the "neck" and "head"
joints are only used to find a "region of interest" where a face is likely to
be).

### Kinect/avatar mismatches

While the Kinect is very impressive for its age and low cost (especially now,
given a glut of supply and not much demand) the accuracy of this joint data can
be a little vague (drift of a couple of inches is common) and players are very
rarely a perfect 1:1 match with their avatars.

We therefore do some reprojection; rather than taking the skeleton from the
Kinect and directly applying it to the skeletal mesh, we convert it into a sort
of abstract, gesture-based representation.  Arms, for example, are a pointing
direction and extension (0 = folded up, 1 = locked straight).  We are then able
to come up with an equivalent sequence of rotations for the individual bones of
the arm which best approximate that.

### Permanence

Not all information is always available (for example, elbows are hard to detect
for straight arms, and arms could be behind you!).  We therefore have to keep
track of some things from the previous update we received, relative to the
player's vague position and facing direction, and reproject it onto their new
position/facing direction.

### Kinect limitation workarounds

When pointing an arm directly towards the Kinect, your hand quite often blocks
the Kinect's view of your wrist.  The Kinect does a reasonable job of filling in
these joints, but it seems that the fingertip joint becomes quite jittery.

We therefore detect the forearm being pointed directly towards the camera and
discard the fingertip joint entirely; the hand instead just follows the
orientation of the forearm.

Adios, René.
