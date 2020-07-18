# AudioVisualizerWinFramework

This is a flexible audio visualizer program with customizable colors and easily created visualization modes.

## Input Modes:
#### Speaker Out
Visualizes the output from the default speaker device
#### Microphone In
Visualizes the audio input from the default microphone device
#### From File
The only mode that doesn't have a slight delay in visualization. This mode also allows you to export a video of the visualization.
## Render modes
There are 3 types of render modes, with a few variations of all but the waveform.
#### Waveform
Renders the samples in a linear fashion. Literally represents the movement of the speaker membrane.
#### Frequency
Renders the frequencies played in a linear fashion. The larger the amplitude of frequency, the taller the spikes.
#### Circle
Renders the frequencies played in a circular fashion, starting at Pi/2 at the lowest frequencies, and running around the circumference of the circle clockwise.
## Settings
#### X Scale
Range: 1 - 100   
Controls the horizontal stretch
#### Y Scale
Range: 0 - 500   
Controls the scale of the amplitude of the frequencies.
#### Sample Size Exponent
Range: 1 - 16   
The sample size must be a power of 2, so the user may only control the integer power of the samples size. A larger sample size will result in more accurate rendering.
#### Smoothing
Range: 1 - 10,000   
Controls the number of elements to apply a center-weighted average to in order to get the spike value. values 1 - 3 all act as one, due to the minimum length needed to create a window function.
#### Colors
All customizable colors will appear in the right hand pair of lists. Double-click on either the color preview or the color name to edit the color.
