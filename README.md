# Transformation Cube Shader (Unity HDRP)
## Overview
Transformation Cube Shader is a Unity HDRP project that applies transformation effects—scaling, rotation, and colour manipulation—to a 3D grid of cubes in real-time. It showcases dynamic transformations controlled programmatically and through user input, creating a visually striking effect that leverages HDRP's high-quality rendering features.

https://github.com/user-attachments/assets/70fd8f95-6055-423b-a99c-014519bdbe06

[Video Link](https://youtu.be/PaRDc6INBPk)

## Features
### High Definition Render Pipeline (HDRP) Compatibility
* Designed specifically for Unity's HDRP, enabling advanced lighting, shadows, and post-processing effects for high-fidelity visuals.

### Dynamic Grid Setup
* Generates a grid of cubes at runtime based on the specified gridResolution parameter.
* Each cube has unique properties and transformations, offering a high degree of visual customisation.

### Real-Time Transformation
* Scaling: The entire grid and individual cubes scale dynamically over time, creating a pulsating effect.
* Rotation: Continuous rotation is applied to the grid with individual cube adjustments.
* Colour Shifts: Dynamic colour changes across the RGB spectrum, creating a vibrant animation.

### Interactive Controls
* Mouse Control: Enables rotation along with mouse input on the screen.
* Touch Control: Supports single-touch rotation and multi-touch (pinch) scaling on touch-enabled devices. (This only works for non HDRP projects)

## Scripts
* `TransformationGrid.cs`

  This script handles:

  * Grid Creation: Instantiates cubes in a 3D grid with gridResolution settings.
  * Transformation Pipeline: Manages scaling, rotation, and position transformations across the entire grid using Transformation components.
  * Colour Updates: Adjusts the colour of each cube based on its position and time to create a shifting visual effect.

* `MouseInput.cs`
  This script enables user interaction:
  
  * Rotation Control: Rotates the cube with mouse movement along X and Y axes.
  * Touch Scaling: Allows for pinch-to-scale gestures on touch-enabled devices. (Only works for non HDRP project)
  
## Installation and Usage
* Clone the Repository: Download or clone this repository to your local machine.
* Open in Unity HDRP: Open the project in Unity Editor with HDRP enabled (Unity version compatible with HDRP).
* Configure parameters such as gridResolution, rotationSpeed, scaleSpeed and assign prefab in the TransformationGrid component to spawn your desired gameobject 
* Run: Play the scene in Unity to see the grid transformation in action.
    * Control the cube rotation using mouse input in the game view at run time

## Dependencies
* Unity Engine with HDRP enabled (Unity version compatible with HDRP)
* No additional plugins are required.
## License
This project is licensed under the MIT License.
