# Kosmonaut - A Soyuz MS-16 Simulator

Kosmonaut is a C# based Unity 2019.4.12f1 project that aims at simulating a virtual training environment for astronauts abroad the Soyuz MS-16.
The compiled project can be downloaded from the official itch.io page:  

[Kosmonaut itch.io page](https://marsnotbruno.itch.io/kosmonaut)

## Installation

To run the project, simply download the compiled source code at the link:
[Kosmonaut itch.io page](https://marsnotbruno.itch.io/kosmonaut)

If you want to download all the source files and tinker with the Unity editor by yourself, simply clone this repository.

## Usage

Use your mouse to look around and the scrol wheel to zoom. You can interact with green-highlighted objects. Some interactable objects automatically adjust your viewing position when you click on them, to return to free view simply click again with the mouse.

The Soyuz's movemets along the 6 degrees of freedom are performed with a Joystick Controller, either XBOX or PlayStation: be sure to check the right profile in the options from the main menu. The manual controls are:

- Roll: move the left joystick horizontally.  
- Pitch: move the left joystick vertically.  
- Yaw: press either the left or right bumper button.  
- Horizontal Translation: move the right joystick horizontally.  
- Vertical Translation: move the right joystick vertically.  
- Forward Translation: press the right trigger.  
- Backward Translation; press the left trigger.  
- Stabilizer: keep pressed the two bumper buttons together.  

The simulator features many possible failures occourring aboard the Soyuz, which can be solved according to these rules:  
- Main Engine Failure: press the "Restart Engines" button.  
- Camera Failure: it fixes by itself after 20 seconds.  
- Computer failure (absence of UI): click twice on the UI button.  
- Communications failure (no numbers on screen): press the "Reset Comms" button.  
- Vital Support Failure: press the "Reset VS" button four times  within 30 seconds of the failure occourring, otherwise the simulation will end.  
 
The other interactions include:
- More/Less power: increase or decrease the engine thrust power.  
- TPV: switch to third person view.  
- UI (while not during failure): switch on and off the main screen UI.  
- LANG: switch the UI and buttons language from Engilsh to Russian and viceversa (fun fact: the real Soyuz is Russian only!)  
- Secondary screen.  
- Side window.  
- ESC button: while in game, toggle the pause menu.  

Your mission is to succesfully dock with the ISS, as shown during the tutorial and in real footage. Be sure to perform contact with an approach velocity lower than 0.05 m/s, otherwise the mission could fail.

## Contributing
Contribution to the project is welcome, although the project won't be maintained in the future by the development team.

## Authors and Acknowledgement

The project has been developed by Alessio Cacciatore, Gianluca Garganese and Marzio Vallero as part of the [Computer Engineering Masters Degree](https://didattica.polito.it/pls/portal30/sviluppo.offerta_formativa.corsi?p_sdu_cds=37:18&p_lang=EN) exam [Virtual Reality](https://didattica.polito.it/pls/portal30/gap.pkg_guide.viewGap?p_cod_ins=02KQEPO&p_a_acc=2020&p_header=S&p_lang=en), taught by professors A. G. Bottino and F. Strada, during the academic year 2020/2021 at the [Politecnico di Torino University](https://www.polito.it/).

## License
For right to use, copyright and warranty of this software, refer to this project's [License](License.md).

## Resources Credits
This application has been made possible thanks to:

*   [Unity Game Engine](https://unity3d.com/) - Application Development
*   [Blender](https://www.blender.org/) - Rigging and model management
*   [Rhinoceros 3D](https://www.rhino3d.com/it/) - Modeling and texturing
*   [Topaz's Gigapixel AI](https://topazlabs.com/gigapixel-ai/?gclid=Cj0KCQiAvvKBBhCXARIsACTePW_hZO8J_2rlMo6Q94LXoWCbKAT-XZ12ck_9W3hbHIS9f27kG_dgChEaAlo7EALw_wcB) - Texture upscaling
*   [Adobe's Substance Painter](https://www.substance3d.com/products/substance-painter/?gclid=Cj0KCQiAvvKBBhCXARIsACTePW9OsK3oEpeGEev1PRWxjNFWQfyd1F_0dNbjyQ3j_HiAdEZEQ7GUFikaAp_vEALw_wcB) - Texture enhancing
*   [XInputDotNet](https://github.com/speps/XInputDotNet) - Vibration management for the joystick
*   [Audacity](https://www.audacityteam.org/) - Audio mixing and mastering
*   [Futura Mission docking](https://www.youtube.com/watch?v=TbBIg0co1sU&ab_channel=EuropeanSpaceAgency%2CESA) - Background audio
*   [All Sounds Youtube channel](https://www.youtube.com/watch?v=B18DEXPeFoU&ab_channel=AllSounds) - Buttons' SFX
*   [NASA's Visible Earth](https://visibleearth.nasa.gov/) - Topography and other Earth high resolution maps, Milky Way's Skybox map
*   [NASA's website](https://www.nasa.gov/) - 3D models for the Sun, Moon and ISS
*   [CSA's website](https://www.asc-csa.gc.ca/eng/vehicles/soyuz/default.asp) - Informations on the Soyuz program
*   [ESA's YouTube channel](https://www.youtube.com/channel/UCIBaDdAbGlFDeS33shmlD0A) - Documentaries on the Docking procedure
*   [NASA's YouTube channel](https://www.youtube.com/channel/UCLA_DiR1FfKNvjuUpBHmylQ) - Original Soyuz footage
*   [NASA's Spaceflight Forum](https://forum.nasaspaceflight.com/index.php?topic=46594.0) - Information for the screens and controls management
*   [Information Display Systems for Russian Spacecraft](https://authors.library.caltech.edu/5456/1/hrst.mit.edu/hrs/apollo/soviet/essays/essay-tiapchenko3.htm) - Additional information
*   [Soyuz - User's Manual](https://www.arianespace.com/wp-content/uploads/2015/09/Soyuz-Users-Manual-March-2012.pdf) - Additional information
*   [NASA Johnson](https://www.flickr.com/photos/nasa2explore/44479705321/) - This page's background image, under <span class="cc-license-identifier" property="dc:identifier dct:identifier">CC BY-NC-ND 2.0</span>
