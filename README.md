# કૈવલકૃપા સ્ટુડિયો લાઈવ - 4 Camera Live Event Mixer

Windows WPF starter project for a simple live-event mixer.

## Camera inputs
- Supports **4 camera input slots**: CAM 1, CAM 2, CAM 3, CAM 4.
- Default device indexes are 0, 1, 2, 3. USB webcams/capture devices may appear under different indexes on a particular PC.
- Each camera has a live preview and can be sent to PROGRAM or B.

## Included controls
- 4 camera previews
- A/B source buttons for all 4 cameras
- Image and video source selection
- Program preview
- CUT / FADE / WIPE controls
- MP4 recording
- RTMP streaming bridge
- Text overlay control
- Audio mute/level UI
- Scene buttons

## Build
Open `SpotLiveV3.csproj` in Visual Studio 2022 with the .NET desktop workload installed, restore NuGet packages, then Build.

Note: This is a development starter. Production-grade capture cards, synchronized multi-camera capture, hardware encoding, audio routing, real transitions, playlists and advanced effects still need implementation/testing.


### Logo
The supplied Kaival Krupa Studio Live logo is included as `KaivalKrupaLogo.png` and displayed in the application header.
