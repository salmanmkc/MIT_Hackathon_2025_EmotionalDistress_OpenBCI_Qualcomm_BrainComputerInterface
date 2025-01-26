Asset Creator - Vladyslav Horobets (Hovl).
-----------------------------------------------------

Using:

1) Shaders
1.1)The "Use depth" on the material from the custom shaders is the Soft Particle Factor.
1.2)Use "Center glow"[MaterialToggle] only with particle system. This option is used to darken the main texture with a white texture (white is visible, black is invisible).
    If you turn on this feature, you need to use "Custom vertex stream" (Uv0.Custom.xy) in tab "Render". And don't forget to use "Custom data" parameters in your PS.
1.3)The distortion shader only works with standard rendering. Delete (if exist) distortion particles from effects if you use LWRP or HDRP!
1.4)You can change the cutoff in all shaders (except Add_CenterGlow and Blend_CenterGlow ) using (Uv0.Custom.xy) in particle system.

2)Quality
2.1) For better sparks quality enable "Anisotropic textures: Forced On" in quality settings.

BiRP, URP or HDRP support is here --> Tools > RP changer for Hovl Studio Assets

Contact me if you have any questions.
My email: hovlstudio1@gmail.com